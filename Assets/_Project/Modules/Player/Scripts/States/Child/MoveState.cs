namespace Project.Player
{
    using System;
    using Project.Core;
    using UnityEngine;


    [StateOf(typeof(PlayerStateController))]
    [ChildStateOf(typeof(GroundedState))]
    sealed class MoveState : ChildState
    {
        [InjectField] private BooleanVariable _isMovingStatus;


        public override void EnterState()
        {
            Debug.Log("Move");
        }


        public override Type CheckTransitions()
        {
            if (_isMovingStatus.runtimeValue == false)
                return typeof(IdleState);

            return null;
        }
    }
}