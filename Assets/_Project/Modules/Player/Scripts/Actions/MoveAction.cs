namespace Project.Player
{
    using Project.Core;
    using UnityEngine;
    using UnityEngine.InputSystem;

    [CreateAssetMenu(fileName = "MoveAction", menuName = "Scriptable Objects/Action/Move")]
    public class MoveAction : ActionBase
    {
        [Header("References")]
        [SerializeField] private InputActionReference _input;

        [Header("Events")]
        [SerializeField] private StandardEvent _onMoveStart;
        [SerializeField] private Vector3Event _onMoveForce;
        [SerializeField] private StandardEvent _onMoveEnd;

        [Header("Status")]
        [SerializeField] private BooleanVariable _isMovingStatus;

        [Header("Values")]
        [SerializeField] private FloatVariable _moveValue;


        public override void EnableAction()
        {
            _input.action.Enable();

            _input.action.performed += HandleInput;
            _input.action.canceled += HandleInput;
        }


        public override void DisableAction()
        {
            _input.action.performed -= HandleInput;
            _input.action.canceled -= HandleInput;

            _input.action.Disable();
        }


        public override void UpdateAction(float deltaTime)
        {
            Vector2 inputValue = _input.action.ReadValue<Vector2>();
            Vector3 moveForce = new Vector3(inputValue.x, 0f, inputValue.y);

            bool inputActive = inputValue != Vector2.zero;

            _onMoveForce.Raise(moveForce * _moveValue.runtimeValue);

            _isMovingStatus.runtimeValue = inputActive;
        }


        private void HandleInput(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    _onMoveStart.Raise();
                    break;
                
                case InputActionPhase.Canceled:
                    _onMoveEnd.Raise();
                    break;
            }
        }
    }
}