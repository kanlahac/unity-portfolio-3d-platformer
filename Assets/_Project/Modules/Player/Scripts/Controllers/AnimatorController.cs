namespace Project.Player
{
    using Project.Core;
    using UnityEngine;

    sealed class AnimatorController : Controller, ILateUpdate
    {
        [InjectField] private Animator _animator;
        [InjectField] private BooleanVariable _isGroundedStatus;
        [InjectField] private Vector3Variable _velocityStatus;


        public void LateUpdate(float deltaTime)
        {
            _animator.SetBool("IsGrounded", _isGroundedStatus.runtimeValue);

            Vector3 velocity = new Vector3(_velocityStatus.runtimeValue.x, 0f, _velocityStatus.runtimeValue.z);
            _animator.SetFloat("Velocity", velocity.magnitude);
        }
    }
}
