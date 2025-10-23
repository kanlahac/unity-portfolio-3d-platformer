namespace Project.Player
{
    using Project.Core;
    using UnityEngine;

    sealed class ForceController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CharacterController _characterController;

        [Header("Events")]
        [SerializeField] private Vector3Event _onAddExternalForce;

        [Header("Status")]
        [SerializeField] private BooleanVariable _isGroundedStatus;
        [SerializeField] private Vector3Variable _velocityStatus;

        [Header("Values")]
        [SerializeField] private FloatVariable _gravityValue;

        private Vector3 _gravityForce;
        private Vector3 _moveForce;
        private Vector3 _externalForce;


        private void OnEnable() => _onAddExternalForce.AddListener(AddExternalForce);
        private void OnDisable() => _onAddExternalForce.RemoveListener(AddExternalForce);


        private void FixedUpdate()
        {
            if (_characterController.enabled == false) return;

            AddGravityForce();

            Vector3 allForces = (
                _gravityForce +
                _moveForce +
                _externalForce
            ) * Time.deltaTime;

            _characterController.Move(allForces);

            _externalForce = Vector3.Lerp(
                _externalForce,
                Vector3.zero,
                5f * Time.deltaTime
            );

            _isGroundedStatus.runtimeValue = _characterController.isGrounded;
            _velocityStatus.runtimeValue = _characterController.velocity;
        }


        private void AddGravityForce()
        {
            if (_characterController.isGrounded == false)
            {
                _gravityForce.y += _gravityValue.runtimeValue * Time.deltaTime;
                _gravityForce.y = Mathf.Max(_gravityForce.y, -50f);
            }
            else
            {
                _gravityForce.y = -0.5f;
            }
        }


        public void AddExternalForce(Vector3 forceVector) => _externalForce += forceVector;
        public void AddMoveForce(Vector3 forceVector) => _moveForce = forceVector;
    }
}