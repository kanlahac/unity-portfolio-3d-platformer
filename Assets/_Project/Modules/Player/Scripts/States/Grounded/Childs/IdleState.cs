namespace Project.Player
{
    using System;
    using Project.Core;

    [StateOf(typeof(PlayerStateController))]
    [ChildStateOf(typeof(GroundedState))]
    sealed class IdleState : ChildState
    {
        [InjectField] private BooleanVariable _inputCheckMove;


        public override Type CheckTransitions()
        {
            if (_inputCheckMove.runtimeValue == true)
                return typeof(MoveState);

            return null;
        }
    }
}