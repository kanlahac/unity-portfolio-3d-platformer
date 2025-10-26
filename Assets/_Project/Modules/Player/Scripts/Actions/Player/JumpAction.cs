namespace Project.Player
{
    using Project.Core;
    using UnityEngine;
    using UnityEngine.InputSystem;

    [CreateAssetMenu(fileName = "JumpAction", menuName = "Scriptable Objects/Action/Jump")]
    public class JumpAction : ActionBase
    {
        [Header("References")]
        [SerializeField] private InputActionReference _input;

        [Header("Events")]
        [SerializeField] private StandardEvent _onJump;
        [SerializeField] private Vector3Event _onExternalForce;

        [Header("Status")]
        [SerializeField] private BooleanVariable _isGroundedStatus;

        [Header("Values")]
        [SerializeField] private FloatVariable _jumpValue;


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

            _onExternalForce.Raise(Vector3.up * _jumpValue.runtimeValue);
            _onJump.Raise();
        }
    }
}