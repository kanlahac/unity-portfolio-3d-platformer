namespace Project.Player
{
    using DG.Tweening;
    using Project.Core;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.InputSystem;

    sealed class DashAction : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InputActionReference _input;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private ForceController _forceController;
        [SerializeField] private float _dashCooldown = 1.5f;

        [Header("Status")]
        [SerializeField] private Vector3Variable _velocityStatus;

        [Header("Values")]
        [SerializeField] private FloatVariable _dashValue;

        [Header("Unity Events"), Space]
        public UnityEvent onDash;
        public UnityEvent onDashReset;

        private bool _isDashing = false;


        private void OnEnable()
        {
            _input.action.Enable();
            _input.action.performed += HandleDash;
        }


        private void OnDisable()
        {
            _input.action.performed -= HandleDash;
            _input.action.Disable();
        }


        private void HandleDash(InputAction.CallbackContext context)
        {
            if (_characterController.enabled == false) return;
            if (_characterController.isGrounded == false) return;
            if (_isDashing == true) return;

            _isDashing = true;
            Vector3 dashForce = _velocityStatus.runtimeValue.normalized * _dashValue.runtimeValue;
            dashForce.y = 0f;

            _forceController.AddExternalForce(dashForce);

            onDash?.Invoke();

            DOVirtual.DelayedCall(_dashCooldown, () =>
            {
                _isDashing = false;
                onDashReset?.Invoke();
            });
        }
        
    }
}
