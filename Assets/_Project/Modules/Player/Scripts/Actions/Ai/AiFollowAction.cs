namespace Project.Player
{
    using Project.Core;
    using UnityEngine;
    using UnityEngine.AI;

    public class AiFollowAction : AiActionBase
    {
        private NavMeshAgent _navMeshAgent => _aiController.navMeshAgent;
        private Transform _otherCharacter => _aiController.otherCharacter;
        private BooleanVariable _isGroundedStatus => _aiController.isGroundedStatus;
        private FloatVariable _moveValue => _aiController.moveValue;
        private Transform transform => _aiController.transform;


        public override void EnableAction()
        {
            _navMeshAgent.ResetPath();

            _isGroundedStatus.runtimeValue = true;
            _navMeshAgent.speed = _moveValue.runtimeValue;
            _navMeshAgent.autoBraking = true;
        }


        public override void UpdateAction(float deltaTime)
        {
            float distance = Vector3.Distance(transform.position, _otherCharacter.position);

            if (distance < _navMeshAgent.stoppingDistance)
            {
                _navMeshAgent.isStopped = true;
            }
            else
            {
                _navMeshAgent.isStopped = false;
                _navMeshAgent.destination = _otherCharacter.position;
            }
        }
    }
}