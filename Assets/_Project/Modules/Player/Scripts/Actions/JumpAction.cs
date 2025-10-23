namespace Project.Player
{
    using Project.Core;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.InputSystem;

    sealed class JumpAction : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InputActionReference _input;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private ForceController _forceController;

        [Header("Values")]
        [SerializeField] private FloatVariable _jumpValue;

        [Header("Unity Events"), Space]
        public UnityEvent OnJump;


        private void OnEnable()
        {
            _input.action.Enable();
            _input.action.performed += HandleJump;
        }


        private void OnDisable()
        {
            _input.action.performed -= HandleJump;
            _input.action.Disable();
        }


        private void HandleJump(InputAction.CallbackContext context)
        {
            if (_characterController.enabled == false) return;
            if (_characterController.isGrounded == false) return;

            _forceController.AddExternalForce(Vector3.up * _jumpValue.runtimeValue);

            OnJump?.Invoke();
        }
    }
}