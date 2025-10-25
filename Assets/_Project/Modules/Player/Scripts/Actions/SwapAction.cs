namespace Project.Player
{
    using Project.Core;
    using UnityEngine;
    using UnityEngine.InputSystem;

    [CreateAssetMenu(fileName = "SwapAction", menuName = "Scriptable Objects/Action/Swap")]
    public class SwapAction : ActionBase
    {
        [Header("References")]
        [SerializeField] private InputActionReference _input;

        [Header("Events")]
        [SerializeField] private StandardEvent _onSwap;

        [Header("Status")]
        [SerializeField] private BooleanVariable _isGroundedStatus;


        public override void EnableAction()
        {
            _input.action.Enable();

            _input.action.performed += HandleInput;
        }


        public override void DisableAction()
        {
            _input.action.performed -= HandleInput;

            _input.action.Disable();
        }


        private void HandleInput(InputAction.CallbackContext context)
        {
            if (_isGroundedStatus.runtimeValue == false) return;
            
            _onSwap.Raise();
        }
    }
}