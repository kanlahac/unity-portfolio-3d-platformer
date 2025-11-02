namespace Project.Player
{
    using System;
    using Project.Core;

    [StateOf(typeof(PlayerStateController))]
    [ChildStateOf(typeof(AirborneState))]
    sealed class FallState : ChildState
    {
        public override Type CheckTransitions()
        {
            return null;
        }
    }
}