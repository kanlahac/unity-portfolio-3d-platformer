namespace Project.Core
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    [CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
    public class InputReader : ScriptableObject, GameInput.IPlayerActions
    {
        [Header("Player events")]
        [SerializeField] private BooleanEvent _inputAbilityEvent;
        [SerializeField] private BooleanEvent _inputAttackEvent;
        [SerializeField] private BooleanEvent _inputDashEvent;
        [SerializeField] private BooleanEvent _inputJumpEvent;
        [SerializeField] private Vector2Event _inputLookEvent;
        [SerializeField] private Vector2Event _inputMoveEvent;
        [SerializeField] private BooleanEvent _inputPauseEvent;
        [SerializeField] private BooleanVariable _inputCheckMove;
        [SerializeField] private BooleanVariable _inputCheckLook;

        private GameInput gameInput;


        private void OnEnable()
        {
            if (gameInput == null)
            {
                gameInput = new GameInput();
                gameInput.Player.SetCallbacks(this);
            }

            EnablePlayerInput();
        }


        private void OnDisable()
        {
            DisableAllInput();
        }


        public void EnablePlayerInput()
        {
            gameInput.Player.Enable();
        }


        public void DisableAllInput()
        {
            gameInput.Player.Disable();
        }


        public void OnAbility(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                _inputAbilityEvent.Raise(true);


            if (context.phase == InputActionPhase.Canceled)
                _inputAbilityEvent.Raise(false);
        }


        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                _inputAttackEvent.Raise(true);

            if (context.phase == InputActionPhase.Canceled)
                _inputAttackEvent.Raise(false);
        }


        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                _inputDashEvent.Raise(true);

            if (context.phase == InputActionPhase.Canceled)
                _inputDashEvent.Raise(false);
        }


        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                _inputJumpEvent.Raise(true);
            
            if (context.phase == InputActionPhase.Canceled)
                _inputJumpEvent.Raise(false);
        }


        public void OnLook(InputAction.CallbackContext context)
        {
            _inputLookEvent.Raise(context.ReadValue<Vector2>());

            if (context.phase == InputActionPhase.Performed)
                _inputCheckLook.runtimeValue = true;

            if (context.phase == InputActionPhase.Canceled)
                _inputCheckLook.runtimeValue = false;
        }
        

        public void OnMove(InputAction.CallbackContext context)
        {
            _inputMoveEvent.Raise(context.ReadValue<Vector2>());

            if (context.phase == InputActionPhase.Performed)
                _inputCheckMove.runtimeValue = true;

            if (context.phase == InputActionPhase.Canceled)
                _inputCheckMove.runtimeValue = false;
        }


        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                _inputPauseEvent.Raise(true);

            if (context.phase == InputActionPhase.Canceled)
                _inputPauseEvent.Raise(false);
        }
    }
}