namespace Project.Core
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;

    [CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
    public class InputReader : ScriptableObject, GameInput.IPlayerActions
    {
        [field: SerializeField] public Vector2 MoveValue { get; private set; }
        [field: SerializeField] public Vector2 LookValue { get; private set; }
        [field: SerializeField] public bool IsMoving { get; private set; }
        [field: SerializeField] public bool IsLooking { get; private set; }
        [field: SerializeField] public bool IsUsingAbility { get; private set; }
        [field: SerializeField] public bool IsAttacking { get; private set; }
        [field: SerializeField] public bool IsDashing { get; private set; }
        [field: SerializeField] public bool IsJumping { get; private set; }
        [field: SerializeField] public bool IsPaused { get; private set; }


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
            {
                IsUsingAbility = true;
            }
                
            if (context.phase == InputActionPhase.Canceled)
            {
                IsUsingAbility = false;
            }
        }


        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                IsAttacking = true;
            }
            
            if (context.phase == InputActionPhase.Canceled)
            {
                IsAttacking = false;
            }
        }


        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                IsDashing = true;
            }
                
            if (context.phase == InputActionPhase.Canceled)
            {
                IsDashing = false;
            }
        }


        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                IsJumping = true;
            }

            if (context.phase == InputActionPhase.Canceled)
            {
                IsJumping = false;
            } 
        }


        public void OnLook(InputAction.CallbackContext context)
        {
            Vector2 Value = context.ReadValue<Vector2>();
            LookValue = Value;
            IsLooking = Value.sqrMagnitude > 0.01f;
        }
        

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 Value = context.ReadValue<Vector2>();
            MoveValue = Value;
            IsMoving = Value.sqrMagnitude > 0.01f;
        }


        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                IsPaused = true;
            }
                
            if (context.phase == InputActionPhase.Canceled)
            {
                IsPaused = false;
            }
        }
    }
}