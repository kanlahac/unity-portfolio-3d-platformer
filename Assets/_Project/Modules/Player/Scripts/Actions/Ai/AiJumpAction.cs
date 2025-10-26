namespace Project.Player
{
    using DG.Tweening;
    using Project.Core;
    using UnityEngine;
    using UnityEngine.AI;

    public class AiJumpAction : AiActionBase
    {
        private NavMeshAgent _navMeshAgent => _aiController.navMeshAgent;
        private BooleanVariable _isGroundedStatus => _aiController.isGroundedStatus;
        private FloatVariable _moveValue => _aiController.moveValue;
        private Transform transform => _aiController.transform;


        public override void UpdateAction(float deltaTime)
        {
            if (_navMeshAgent.isOnOffMeshLink == false) return;

            _isGroundedStatus.runtimeValue = false;

            transform.DOKill();
            transform.DOJump(
                endValue: _navMeshAgent.currentOffMeshLinkData.endPos,
                jumpPower: 4f,
                numJumps: 1,
                duration: 1.0f
            )
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                _navMeshAgent.CompleteOffMeshLink();

                _isGroundedStatus.runtimeValue = true;
            });
        }
    }
}