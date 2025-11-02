namespace Project.Core
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;

    [CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
    public class InputReader : ScriptableObject, GameInput.IPlayerActions
    {
        public event Action abilityEvent;
        public event Action attackEvent;
        public event Action dashEvent;
        public event Action<float> jumpEvent;
        public event Action<Vector2> lookEvent;
        public event Action<Vector2> moveEvent;
        public event Action pauseEvent;

        public bool isMoving { get; private set; }
        public bool isLooking { get; private set; }
        public bool isUsingAbility { get; private set; }
        public bool isAttacking { get; private set; }
        public bool isDashing { get; private set; }
        public bool isJumping { get; set; }
        public bool isUsePause { get; private set; }


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
                isUsingAbility = true;
                abilityEvent?.Invoke();
            }
                
            if (context.phase == InputActionPhase.Canceled)
            {
                isUsingAbility = false;
            }
        }


        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                isAttacking = true;
                attackEvent?.Invoke();
            }
            
            if (context.phase == InputActionPhase.Canceled)
            {
                isAttacking = false;
            }
        }


        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                isDashing = true;
                dashEvent?.Invoke();
            }
                
            if (context.phase == InputActionPhase.Canceled)
            {
                isDashing = false;
            }
        }


        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                isJumping = true;
            } 

            if (context.phase == InputActionPhase.Performed)
            {
                float normalizedValue = Mathf.Clamp((float)context.duration, 0.5f, 1f);

                jumpEvent?.Invoke(normalizedValue);
            }

            if (context.phase == InputActionPhase.Canceled)
            {
                isJumping = false;
            } 
        }


        public void OnLook(InputAction.CallbackContext context)
        {
            Vector2 Value = context.ReadValue<Vector2>();

            isLooking = Value != Vector2.zero;
            lookEvent?.Invoke(Value);
        }
        

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 Value = context.ReadValue<Vector2>();

            isMoving = Value != Vector2.zero;
            moveEvent?.Invoke(Value);
        }


        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                pauseEvent?.Invoke();
            }
                
            if (context.phase == InputActionPhase.Canceled)
            {
                isUsePause = false;
            }
        }
    }
}