namespace Project.Player
{
    using System;
    using Project.Core;

    [StateOf(typeof(PlayerStateController))]
    [ChildStateOf(typeof(GroundedState))]
    sealed class IdleState : ChildState
    {
        public override void EnterState(){}
        public override void ExitState(){}
        public override void UpdateState(float deltaTime){}
    }
}