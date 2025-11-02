namespace Project.Player
{
    using Project.Core;
    using UnityEngine;

    sealed class AnimatorController : Controller, ILateUpdate
    {
        [InjectField] private Animator _animator;
        [InjectField] private BooleanVariable _isGroundedStatus;
        [InjectField] private Vector3Variable _velocityStatus;

        private static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");
        private static readonly int VelocityHash = Animator.StringToHash("Velocity");


        public void LateUpdate(float deltaTime)
        {
            _animator.SetBool(IsGroundedHash, _isGroundedStatus.runtimeValue);

            Vector3 velocity = new Vector3(_velocityStatus.runtimeValue.x, 0f, _velocityStatus.runtimeValue.z);
            _animator.SetFloat(VelocityHash, velocity.magnitude);
        }
    }
}
