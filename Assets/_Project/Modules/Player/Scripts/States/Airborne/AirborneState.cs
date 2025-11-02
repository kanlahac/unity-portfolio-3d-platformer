namespace Project.Player
{
    using System;
    using Project.Core;

    [StateOf(typeof(PlayerStateController))]
    sealed class AirborneState : ParentState
    {
        [InjectField] private InputReader _inputReader;


        public override void EnterState()
        {
            ActivateChildState(typeof(FallState));
        }


        public override void ExitState()
        {
            
        }


        protected override void CheckTransitions()
        {

        }
    }
}