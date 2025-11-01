namespace Project.Player
{
    using System;
    using Project.Core;
    using UnityEngine;

    [StateOf(typeof(PlayerStateController))]
    sealed class GroundedState : ParentState
    {
        [InjectField] private BooleanVariable _isGroundedStatus;


        public override void EnterState()
        {
            SetChildState(typeof(IdleState));
            Debug.Log("Grounded");
        }


        public override Type CheckTransitions()
        {
            if (_isGroundedStatus.runtimeValue == false)
                return typeof(AirState);

            return null;
        }
    }
}