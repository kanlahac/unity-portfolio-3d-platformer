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
        [SerializeField] private AiActionBase[] _aiActions;

        [Header("Status")]
        [SerializeField] private BooleanVariable _isGroundedStatus;
        [SerializeField] private Vector3Variable _velocityStatus;
        [SerializeField] private FloatVariable _moveValue;

        private List<AiActionBase> _actions = new();

        public NavMeshAgent navMeshAgent => _navMeshAgent;
        public Transform otherCharacter => _otherCharacter;
        public BooleanVariable isGroundedStatus => _isGroundedStatus;
        public FloatVariable moveValue => _moveValue;


        private void Awake() => ActionFactory();


        private void OnEnable()
        {
            foreach (AiActionBase action in _actions)
            {
                action.EnableAction();
            }
        }


        private void OnDisable()
        {
            foreach (AiActionBase action in _actions)
            {
                action.DisableAction();
            }
        }


        private void Update()
        {
            foreach (AiActionBase action in _actions)
            {
                action.UpdateAction(Time.deltaTime);
            }

            _velocityStatus.runtimeValue = _navMeshAgent.velocity;
        }


        private void ActionFactory()
        {
            foreach (AiActionBase action in _aiActions)
            {
                AiActionBase instance = Instantiate(action);
                instance.AwakeAction(this);
                _actions.Add(instance);
            }
        }


        private void OnDestroy()
        {
            foreach (AiActionBase action in _actions)
            {
                Destroy(action);
            }

            _actions.Clear();
        }
    }
}