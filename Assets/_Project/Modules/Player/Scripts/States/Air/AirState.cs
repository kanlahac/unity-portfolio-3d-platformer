namespace Project.Player
{
    using Project.Core;
    using UnityEngine;

    [StateOf(typeof(PlayerStateController))]
    sealed class AirState : ParentState
    {
        [InjectField] private InputReader _inputReader;


        public override void EnterState()
        {
            ActivateChildState(typeof(FallState));
        }


        protected override void CheckTransitions()
        {
            
        }
    }
}