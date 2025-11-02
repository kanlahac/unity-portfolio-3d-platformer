namespace Project.Player
{
    using Project.Core;
    using UnityEngine;

    sealed class ForceController : Controller, IEnable, IDisable, IUpdate
    {
        [InjectField] private CharacterController _characterController;
        [InjectField] private Vector3Event _onAddExternalForce;
        [InjectField] private BooleanVariable _isGroundedStatus;
        [InjectField] private Vector3Variable _velocityStatus;
        [InjectField] private Vector3Variable _horizontalMoveStatus;
        [InjectField] private FloatVariable _gravityValue;
        private Vector3 _gravityForce;
        private Vector3 _externalForce;


        public void OnEnable()
        {
            _onAddExternalForce.AddListener(AddExternalForce);
        }


        public void OnDisable()
        {
            _onAddExternalForce.RemoveListener(AddExternalForce);
        }


        public void Update(float deltaTime)
        {
            if (_characterController.enabled == false) return;

            AddGravity(deltaTime);

            Vector3 allForces = (
                _horizontalMoveStatus.runtimeValue +
                _gravityForce +
                _externalForce
            ) * deltaTime;

            _characterController.Move(allForces);

            _externalForce = Vector3.Lerp(
                _externalForce,
                Vector3.zero,
                5f * deltaTime
            );

            _isGroundedStatus.runtimeValue = _characterController.isGrounded;
            _velocityStatus.runtimeValue = _characterController.velocity;
        }


        private void AddGravity(float deltaTime)
        {
            if (_isGroundedStatus.runtimeValue == false)
            {
                _gravityForce.y += _gravityValue.runtimeValue * deltaTime;
                _gravityForce.y = Mathf.Max(_gravityForce.y, -50f);
            }
            else
            {
                _gravityForce.y = -0.5f;
            }
        }


        private void AddExternalForce(Vector3 forceVector)
        {
            _externalForce += forceVector;
        }
    }
}