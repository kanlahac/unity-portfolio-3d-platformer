namespace Project.Player
{
    using UnityEngine;
    using UnityEngine.AI;
    using UnityEngine.InputSystem;

    sealed class InputController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InputActionAsset _inputAsset;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private CharacterController _characterController;

        void Awake()
        {
            
        }
    }
}