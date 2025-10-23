namespace Project.Player
{
    using Project.Core;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.InputSystem;

    sealed class MoveAction : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InputActionReference _input;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private ForceController _forceController;
        [SerializeField] private Transform _characterModel;

        [Header("Status")]
        [SerializeField] private BooleanVariable _isMovingStatus;

        [Header("Values")]
        [SerializeField] private FloatVariable _moveValue;

        [Header("Unity events"), Space]
        public UnityEvent OnMoveStart;
        public UnityEvent OnMoveEnd;

        private bool _isMovementActive = false;


        private void OnEnable() => _input.action.Enable();


        private void OnDisable()
        {
            _input.action.Disable();

            _isMovementActive = false;
            _isMovingStatus.runtimeValue = false;
        }


        private void FixedUpdate()
        {
            if (_characterController.enabled == false) return;

            Vector2 inputValue = _input.action.ReadValue<Vector2>();
            Vector3 moveForce = new Vector3(inputValue.x, 0f, inputValue.y);

            bool inputActive = inputValue != Vector2.zero;
            bool isGrounded = _characterController.isGrounded;

            _forceController.AddMoveForce(moveForce * _moveValue.runtimeValue);

            if (inputActive)
            {
                RotateCharacterModel(moveForce);
            }

            if (_isMovementActive == true && (inputActive == false || isGrounded == false))
            {
                OnMoveEnd?.Invoke();
                _isMovementActive = false;
            }

            if (_isMovementActive == false && inputActive == true && isGrounded == true)
            {
                OnMoveStart?.Invoke();
                _isMovementActive = true;
            }

            _isMovingStatus.runtimeValue = _isMovementActive;
        }


        private void RotateCharacterModel(Vector3 moveForce)
        {
            Quaternion lookTarget = Quaternion.LookRotation(moveForce);

            _characterModel.rotation = Quaternion.Slerp(
                _characterModel.rotation,
                lookTarget,
                15f * Time.deltaTime
            );
        }
    }
}