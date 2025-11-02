namespace Project.Player
{
    using System;
    using Project.Core;

    [StateOf(typeof(PlayerStateController))]
    [ChildStateOf(typeof(AirborneState))]
    sealed class FallState : ChildState
    {
        public override void EnterState()
        {
            
        }

        public override void ExitState()
        {
            
        }

        public override void UpdateState(float deltaTime)
        {
            
        }
    }
}