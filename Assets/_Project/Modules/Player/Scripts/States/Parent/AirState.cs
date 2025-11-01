namespace Project.Player
{
    using System;
    using Project.Core;
    using UnityEngine;

    [StateOf(typeof(PlayerStateController))]
    sealed class AirState : ParentState
    {
        [InjectField] private BooleanVariable _isGroundedStatus;


        public override void EnterState()
        {
            //SetChildState(typeof(IdleState));
            Debug.Log("Air");
        }


         public override Type CheckTransitions()
        {
            if (_isGroundedStatus.runtimeValue == true)
                return typeof(GroundedState);

            return null;
        }
    }
}