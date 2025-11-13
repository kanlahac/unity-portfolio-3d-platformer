namespace Project.Player
{
    using Project.Core;
    using UnityEngine;

    [StateOf(typeof(PlayerStateController))]
    sealed class AirState : ParentState
    {
        [InjectField] private InputReader _inputReader;
        [InjectField] private PlayerData _playerData;


        public override void EnterState()
        {
            ActivateChildState(typeof(FallState));
        }


        protected override void CheckTransitions()
        {
            if (_inputReader.IsMoving)
            {
                ActivateChildState(typeof(AirMoveState));
            }

            if (!_inputReader.IsMoving)
            {
                DeactivateChildState(typeof(AirMoveState));
            }

            if (_inputReader.IsDashing && _playerData.DashCooldownValue <= 0f)
            {
                ActivateChildState(typeof(AirDashState));
            }

            if (!_inputReader.IsDashing)
            {
                DeactivateChildState(typeof(AirDashState));
            }
        }
    }
}