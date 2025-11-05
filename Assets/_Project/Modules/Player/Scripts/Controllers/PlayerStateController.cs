namespace Project.Player
{
    using Project.Core;

    sealed class PlayerStateController : StateMachine
    {
        [InjectField] private ModuleData _playerData;


        public override void CheckParentTransition()
        {
            if (_playerData.IsGrounded)
            {
                SetParentState(typeof(GroundState));
                return;
            }
            
            if (!_playerData.IsGrounded)
            {
                SetParentState(typeof(AirState));
                return;
            }
        }
    }
}