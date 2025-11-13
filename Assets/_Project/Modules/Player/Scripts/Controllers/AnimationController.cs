namespace Project.Player
{
    using Project.Core;
    using UnityEngine;

    sealed class AnimationController : Controller, ILateUpdate
    {
        [InjectField] private Animator _animator;
        [InjectField] private PlayerData _playerData;
        private static readonly int _velocityHash = Animator.StringToHash("Velocity");
        private static readonly int _isGroundedHash = Animator.StringToHash("IsGrounded");
        private static readonly int _CanDashHash = Animator.StringToHash("CanDash");
        private static readonly int _CanUseAbilityHash = Animator.StringToHash("CanUseAbility");


        public void LateUpdate(float deltaTime)
        {
            float velocity = _playerData.Velocity.magnitude;
            _animator.SetFloat(_velocityHash, velocity);

            _animator.SetBool(_isGroundedHash, _playerData.IsGrounded);

            _animator.SetBool(_CanDashHash, _playerData.CanDash);

            _animator.SetBool(_CanUseAbilityHash, _playerData.CanUseAbility);
        } 
    }
}