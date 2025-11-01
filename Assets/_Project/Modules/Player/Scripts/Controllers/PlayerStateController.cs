namespace Project.Player
{
    using Project.Core;

    sealed class PlayerStateController : StateMachine
    {
        protected override void Awake()
        {
            SetParentState(typeof(GroundedState));
        }
    }
}