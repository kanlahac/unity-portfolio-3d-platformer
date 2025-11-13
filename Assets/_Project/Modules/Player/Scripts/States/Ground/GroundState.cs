namespace Project.Player
{
    using Project.Core;
    using UnityEngine; 

    [StateOf(typeof(PlayerStateController))]
    sealed class GroundState : ParentState
    {
        [InjectField] private InputReader _inputReader;
        [InjectField] private PlayerData _playerData;


        public override void EnterState()
        {
            ActivateChildState(typeof(IdleState));
        }


        protected override void CheckTransitions()
        {
            if (_inputReader.IsMoving)
            {
                ActivateChildState(typeof(MoveState));
                DeactivateChildState(typeof(IdleState));
            }

            if (!_inputReader.IsMoving)
            {
                ActivateChildState(typeof(IdleState));
                DeactivateChildState(typeof(MoveState));
            }

            if (_inputReader.IsJumping)
            { 
                ActivateChildState(typeof(JumpState));
            }

            if (_inputReader.IsDashing && _playerData.DashCooldownValue <= 0f)
            {
                ActivateChildState(typeof(DashState));
            }

            if (!_inputReader.IsDashing)
            {
                DeactivateChildState(typeof(DashState));
            }

            if (_inputReader.IsUsingAbility)
            {
                ActivateChildState(typeof(AbilityState));
            }

            if (!_inputReader.IsUsingAbility)
            {
                DeactivateChildState(typeof(AbilityState));
            }
        }
    }
}