namespace Project.Player
{
    using Project.Core;

    [ChildStateOf(typeof(GroundState))]
    [StateOf(typeof(PlayerStateController))]
    sealed class IdleState : ChildState
    {
        public override void EnterState()
        {
            
        }


        public override void ExitState()
        {

        }
    }
}