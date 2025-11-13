namespace Project.Player
{
    using Project.Core;

    [ChildStateOf(typeof(AirState))]
    [StateOf(typeof(PlayerStateController))]
    sealed class AirMoveState : ChildState
    {
        [InjectField] private PlayerData _playerData; 


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