namespace Project.Player
{
    using Project.Core;

    [ChildStateOf(typeof(GroundState))]
    [StateOf(typeof(PlayerStateController))]
    sealed class MoveState : ChildState
    {
        [InjectField] private ModuleData _playerData;


        public override void EnterState()
        {
            _playerData.CanMove = true;
        }


        public override void ExitState()
        {
            _playerData.CanMove = false;
        }
    }
}