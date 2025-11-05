namespace Project.Player
{
    using Project.Core;

    [ChildStateOf(typeof(GroundState))]
    [StateOf(typeof(PlayerStateController))]
    sealed class JumpState : ChildState
    {
        [InjectField] private ModuleData _playerData;


        public override void EnterState()
        {
            _playerData.CanJump = true;
        }


        public override void ExitState()
        {
            _playerData.CanJump = false;
        }
    }
}