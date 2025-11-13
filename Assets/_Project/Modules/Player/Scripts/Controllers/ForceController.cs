namespace Project.Player
{
    using DG.Tweening;
    using Project.Core;
    using UnityEngine;

    sealed class ForceController : Controller, ILateUpdate
    {
        [InjectField] private PlayerData _playerData;
        [InjectField] private InputReader _inputReader;
        [InjectField] private CharacterController _characterController;
        [InjectField] private Transform _characterModel;
        [InjectField] private GameObject _root;
        private Tween _decelerateExternalForcesTween = null;


        public void LateUpdate(float deltaTime)
        {
            if (_playerData.CanMove) ApplyMovement(deltaTime);
            if (!_playerData.CanMove) DecelerateMovement(deltaTime);
            if (_playerData.CanJump) ApplyJump();
            if (_playerData.CanDash) ApplyDash(deltaTime);
            if (_playerData.CanApplyGravity) ApplyGravity(deltaTime);

            _characterController.Move(
                (
                    (Vector3.up * _playerData.VerticalForce.y) +
                    _playerData.HorizontalForce +
                    _playerData.ExternalForce
                )
                * deltaTime
            );

            if (_playerData.IsGrounded)
            {
                _playerData.VerticalForce.y = -2.5f;
            }

            if (_playerData.DashCooldownValue >= 0f)
            {
                _playerData.DashCooldownValue -= deltaTime;
            }

            DecelerateExternalForce(deltaTime);

            _playerData.Velocity = _characterController.velocity;
            _playerData.IsGrounded = _characterController.isGrounded;
            _playerData.GlobalPosition = _characterController.transform.position;
        }


        private void ApplyMovement(float deltaTime)
        {
            Vector3 moveForce = new Vector3(_inputReader.MoveValue.x, 0f, _inputReader.MoveValue.y);
            Vector3 flatMovement = new Vector3(moveForce.x, 0, moveForce.z);

            _playerData.HorizontalForce = moveForce * _playerData.MoveValue;

            if (flatMovement.sqrMagnitude >= 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(flatMovement);

                _characterModel.rotation = Quaternion.Slerp(
                    _characterModel.rotation,
                    targetRotation,
                    15f * deltaTime
                );

                _playerData.LookingDirection = _characterModel.forward;
            }
        }


        private void ApplyJump()
        {
            _playerData.ExternalForce += Vector3.up * _playerData.JumpValue;
        }


        private void ApplyDash(float deltaTime)
        {
            _playerData.ExternalForce += _playerData.LookingDirection * _playerData.DashValue;
            _playerData.DashCooldownValue = _playerData.BaseDashCooldownValue;
        }


        private void ApplyGravity(float deltaTime)
        {
            _playerData.VerticalForce.y += _playerData.GravityValue * deltaTime;

            _playerData.VerticalForce.y = Mathf.Clamp(
                _playerData.VerticalForce.y,
                -25f,
                25f
            );
        }


        private void DecelerateMovement(float deltaTime)
        {
            if (_playerData.HorizontalForce.sqrMagnitude <= 0.01f) return;

            _playerData.HorizontalForce = Vector3.zero;
        }


        private void DecelerateExternalForce(float deltaTime)
        {
            if (_playerData.ExternalForce.sqrMagnitude <= 0.01f) return;

            _decelerateExternalForcesTween.Kill();

            _decelerateExternalForcesTween = DOTween.To(
                () => _playerData.ExternalForce,
                result => _playerData.ExternalForce = result,
                Vector3.zero,
                0.5f
            )
            .SetTarget(_root)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
                _playerData.ExternalForce = Vector3.zero
            );
        }
    }
}