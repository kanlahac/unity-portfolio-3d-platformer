namespace Project.Player
{
    using Project.Core;

    [ChildStateOf(typeof(GroundState))]
    [StateOf(typeof(PlayerStateController))]
    sealed class DashState : ChildState
    {
        [InjectField] private ModuleData _playerData;


        public override void EnterState()
        {
            _playerData.CanDash = true;
        }


        public override void ExitState()
        {
            _playerData.CanDash = false;
        }
    }
}