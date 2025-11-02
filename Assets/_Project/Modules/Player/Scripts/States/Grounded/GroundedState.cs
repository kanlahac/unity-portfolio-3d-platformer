namespace Project.Player
{
    using System;
    using Project.Core;
    using UnityEngine;

    [StateOf(typeof(PlayerStateController))]
    sealed class GroundedState : ParentState
    {
        [InjectField] private InputReader _inputReader;


        public override void EnterState()
        {
            ActivateChildState(typeof(IdleState));
        }


        public override void ExitState()
        {
            
        }


        protected override void CheckTransitions()
        {
            if (_inputReader.isJumping == true)
            {
                ActivateChildState(typeof(JumpState));
            }
                
            if (_inputReader.isMoving == true)
            {
                DesactivateChildState(typeof(IdleState));
                ActivateChildState(typeof(MoveState));
            }
            else
            {
                DesactivateChildState(typeof(MoveState));
                ActivateChildState(typeof(IdleState));
            }
        }  
    }
}