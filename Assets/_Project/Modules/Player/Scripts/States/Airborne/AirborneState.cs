namespace Project.Player
{
    using System;
    using Project.Core;

    [StateOf(typeof(PlayerStateController))]
    sealed class AirborneState : ParentState
    {
        [InjectField] private BooleanVariable _isGroundedStatus;


        public override void EnterState()
        {
            SetChildState(typeof(FallState));
        }


         public override Type CheckTransitions()
        {
            if (_isGroundedStatus.runtimeValue == true)
                return typeof(GroundedState);

            return null;
        }
    }
}