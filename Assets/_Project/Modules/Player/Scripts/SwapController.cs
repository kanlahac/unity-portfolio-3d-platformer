namespace Project.Player
{
    using Project.Core;
    using Unity.Cinemachine;
    using UnityEngine;
    using UnityEngine.AI;

    sealed class SwapController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ForceController _forceController;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private AiController _aiController;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private CinemachineCamera _camera;

        [Header("Status")]
        [SerializeField] private BooleanVariable _isControlledByPlayer;
        [SerializeField] private Vector3Variable _velocity;
        [SerializeField] private BooleanVariable _isGroundedStatus;

        public bool isGrounded => _isGroundedStatus.runtimeValue;


        public void SwapControl(bool isControlledByPlayer)
        {
            if (isControlledByPlayer)
            {
                _aiController.enabled = false;
                _navMeshAgent.enabled = false;
                _characterController.enabled = true;
                _playerController.enabled = true;
                _camera.Priority = 10;
            }
            else
            {
                _playerController.enabled = false;

                _forceController.ResetForces();

                _velocity.runtimeValue = Vector3.zero;
                _characterController.enabled = false;
                _navMeshAgent.enabled = true;
                _aiController.enabled = true;

                _navMeshAgent.Warp(transform.position);
                
                _camera.Priority = 0;
            }

            _isControlledByPlayer.runtimeValue = isControlledByPlayer;
        }
    }
}