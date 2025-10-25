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
        [SerializeField] private Vector3Event _onAddMoveForce;

        [Header("Status")]
        [SerializeField] private BooleanVariable _isGroundedStatus;
        [SerializeField] private Vector3Variable _velocityStatus;

        [Header("Values")]
        [SerializeField] private FloatVariable _gravityValue;

        private Vector3 _gravityForce;
        private Vector3 _moveForce;
        private Vector3 _externalForce;


        private void OnEnable()
        {
            _onAddExternalForce.AddListener(AddExternalForce);
            _onAddMoveForce.AddListener(AddMoveForce);
        }


        private void OnDisable()
        {
            _onAddExternalForce.RemoveListener(AddExternalForce);
            _onAddMoveForce.RemoveListener(AddMoveForce);
        }


        private void FixedUpdate()
        {
            if (_characterController.enabled == false) return;

            RotateTowardsMoveDirection();
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


        private void RotateTowardsMoveDirection()
        {
            Vector3 flatMovement = new Vector3(_moveForce.x, 0, _moveForce.z);

            if (flatMovement.sqrMagnitude >= 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(flatMovement);

                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    15f * Time.deltaTime
                );
            }
        }

        
        public void ResetForces()
        {
            _gravityForce = Vector3.zero;
            _moveForce = Vector3.zero;
            _externalForce = Vector3.zero;
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


        private void AddExternalForce(Vector3 forceVector) => _externalForce += forceVector;
        private void AddMoveForce(Vector3 forceVector) => _moveForce = forceVector;
    }
}