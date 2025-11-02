namespace Project.Player
{
    using System;
    using Project.Core;
    using UnityEngine;

    [StateOf(typeof(PlayerStateController))]
    [ChildStateOf(typeof(GroundedState))]
    sealed class JumpState : ChildState
    {
        [InjectField] private Vector3Event _onAddExternalForce;
        [InjectField] private FloatVariable _jumpValue;
        [InjectField] private InputReader _inputReader;


        public override void EnterState()
        {
            _inputReader.jumpEvent += HandleJump;
        }


        public override void ExitState()
        {
            _inputReader.jumpEvent -= HandleJump;
        }


        private void HandleJump(float pressForce)
        {
            _onAddExternalForce.Raise(Vector3.up * _jumpValue.runtimeValue * pressForce);
        }


        public override void UpdateState(float deltaTime){}
    }
}