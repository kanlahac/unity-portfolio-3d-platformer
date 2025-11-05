namespace Project.Player
{
    using Project.Core;

    [ChildStateOf(typeof(AirState))]
    [StateOf(typeof(PlayerStateController))]
    sealed class FallState : ChildState
    {
        [InjectField] private ModuleData _playerData; 


        public override void EnterState()
        {
            _playerData.CanApplyGravity = true;
            _playerData.CanMove = true;
        }


        public override void ExitState()
        {
            _playerData.CanApplyGravity = false;
            _playerData.CanMove = false;
        }
    }
}