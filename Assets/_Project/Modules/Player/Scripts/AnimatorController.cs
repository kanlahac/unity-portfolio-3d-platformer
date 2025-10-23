namespace Project.Player
{
    using Project.Core;
    using UnityEngine;

    sealed class AnimatorController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Animator _animator;

        [Header("Status")]
        [SerializeField] private BooleanVariable _isGroundedStatus;
        [SerializeField] private Vector3Variable _velocityStatus;


        private void LateUpdate()
        {
            _animator.SetBool("IsGrounded", _isGroundedStatus.runtimeValue);

            Vector3 velocity = new Vector3(_velocityStatus.runtimeValue.x, 0f, _velocityStatus.runtimeValue.z);
            _animator.SetFloat("Velocity", velocity.magnitude);
        }


        public void HandleDash() => _animator.SetTrigger("Dash");
    }
}
