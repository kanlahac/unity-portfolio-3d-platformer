namespace Project.Player
{
    using Project.Core;
    using UnityEngine;
    using UnityEngine.InputSystem;

    sealed class ForceController : Controller, IEnable, IDisable, IUpdate
    {
        [InjectField] private CharacterController _characterController;
        [InjectField] private Vector3Event _onAddExternalForce; 
        [InjectField] private BooleanVariable _isGroundedStatus;
        [InjectField] private Vector3Variable _velocityStatus;
        [InjectField] private FloatVariable _gravityValue;
        [InjectField] private Transform _characterTransform;
        [InjectField] private InputActionReference _moveInput;
        [InjectField] private StandardEvent _onMoveStart;
        [InjectField] private StandardEvent _onMoveEnd;
        [InjectField] private BooleanVariable _isMovingStatus;
        [InjectField] private FloatVariable _moveValue;

        private Vector3 _gravityForce;
        private Vector3 _moveForce;
        private Vector3 _externalForce;


        public void OnEnable()
        {
            _onAddExternalForce.AddListener(AddExternalForce);

            _moveInput.action.Enable();

            _moveInput.action.performed += HandleInput;
            _moveInput.action.canceled += HandleInput;
        }


        public void OnDisable()
        {
            _onAddExternalForce.RemoveListener(AddExternalForce);

            _moveInput.action.performed -= HandleInput;
            _moveInput.action.canceled -= HandleInput;

            _moveInput.action.Disable();
        }


        public void Update(float deltaTime)
        {
            if (_characterController.enabled == false) return;

            AddMoveForce();
            RotateTowardsMoveDirection(deltaTime);
            AddGravityForce(deltaTime);

            Vector3 allForces = (
                _gravityForce +
                _moveForce +
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


        private void RotateTowardsMoveDirection(float deltaTime)
        {
            Vector3 flatMovement = new Vector3(_moveForce.x, 0, _moveForce.z);

            if (flatMovement.sqrMagnitude >= 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(flatMovement);

                _characterTransform.rotation = Quaternion.Slerp(
                    _characterTransform.rotation,
                    targetRotation,
                    15f * deltaTime
                );
            }
        }


        private void AddGravityForce(float deltaTime)
        {
            if (_characterController.isGrounded == false)
            {
                _gravityForce.y += _gravityValue.runtimeValue * deltaTime;
                _gravityForce.y = Mathf.Max(_gravityForce.y, -50f);
            }
            else
            {
                _gravityForce.y = -0.5f;
            }
        }


        private void AddMoveForce()
        {
            Vector2 inputValue = _moveInput.action.ReadValue<Vector2>();
            Vector3 moveForce = new Vector3(inputValue.x, 0f, inputValue.y);

            bool inputActive = inputValue != Vector2.zero;

            _moveForce = moveForce * _moveValue.runtimeValue;

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


        private void AddExternalForce(Vector3 forceVector) => _externalForce += forceVector;
    }
}