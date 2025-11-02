namespace Project.Player
{
    using Project.Core;

    sealed class PlayerStateController : StateMachine
    {
        [InjectField] private BooleanVariable _isGroundedStatus;


        public override void Awake()
        {
            SetParentState(typeof(GroundedState));
        }


        public override void CheckParentTransition()
        {
            if (_isGroundedStatus.runtimeValue == true )
                SetParentState(typeof(GroundedState));

            if (_isGroundedStatus.runtimeValue == false)
                SetParentState(typeof(AirborneState));
        }
    }
}