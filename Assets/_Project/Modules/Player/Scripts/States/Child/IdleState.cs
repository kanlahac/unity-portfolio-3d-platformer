namespace Project.Player
{
    using System;
    using Project.Core;
    using UnityEngine;

    [StateOf(typeof(PlayerStateController))]
    [ChildStateOf(typeof(GroundedState))]
    sealed class IdleState : ChildState
    {
        [InjectField] private BooleanVariable _isMovingStatus;


        public override void EnterState()
        {
            Debug.Log("Idle");
        }


        public override Type CheckTransitions()
        {
            if (_isMovingStatus.runtimeValue == true)
                return typeof(MoveState);

            return null;
        }
    }
}