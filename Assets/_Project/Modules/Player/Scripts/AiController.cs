namespace Project.Player
{
    using System.Collections.Generic;
    using Project.Core;
    using UnityEngine;
    using UnityEngine.AI;

    public class AiController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Transform _otherCharacter;

        [Header("Status")]
        [SerializeField] private BooleanVariable _isGroundedStatus;
        [SerializeField] private Vector3Variable _velocityStatus;

        [Header("Values")]
        [SerializeField] private FloatVariable _moveValue;

        private List<AiActionBase> _aiActions = new();

        public NavMeshAgent navMeshAgent => _navMeshAgent;
        public Transform otherCharacter => _otherCharacter;
        public BooleanVariable isGroundedStatus => _isGroundedStatus;
        public FloatVariable moveValue => _moveValue;


        private void Awake() => ActionFactory();


        private void OnEnable()
        {
            foreach (AiActionBase action in _aiActions)
            {
                action.EnableAction();
            }
        }


        private void OnDisable()
        {
            foreach (AiActionBase action in _aiActions)
            {
                action.DisableAction();
            }
        }


        private void Update()
        {
            foreach (AiActionBase action in _aiActions)
            {
                action.UpdateAction(Time.deltaTime);
            }

            _velocityStatus.runtimeValue = _navMeshAgent.velocity;
        }


        private void ActionFactory()
        {
            AiActionBase followAction = new AiFollowAction();
            followAction.AwakeAction(this);
            _aiActions.Add(followAction);

            AiActionBase jumpAction = new AiJumpAction();
            jumpAction.AwakeAction(this);
            _aiActions.Add(jumpAction);
        }
    }
}