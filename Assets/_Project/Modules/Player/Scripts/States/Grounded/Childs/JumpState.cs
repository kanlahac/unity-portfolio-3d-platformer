namespace Project.Player
{
    using System;
    using Project.Core;
    using UnityEngine;
    using UnityEngine.InputSystem;

    [StateOf(typeof(PlayerStateController))]
    [ChildStateOf(typeof(GroundedState))]
    sealed class JumpState : ChildState
    {
        [InjectField] private InputReader _inputReader;
        [InjectField] private StandardEvent _onJump;
        [InjectField] private Vector3Event _onExternalForce;
        [InjectField] private FloatVariable _jumpValue;
        private InputAction _moveInputAction;


        // public override void EnableState()
        // {
        //     _moveInputAction = _inputReader.GetAction("Jump");
        //     _moveInputAction.performed += HandleInput;
        // } 


        // public override void DisableState() => _moveInputAction.performed -= HandleInput;


        // private void HandleInput(InputAction.CallbackContext context)
        // {
        //     _onExternalForce.Raise(Vector3.up * _jumpValue.runtimeValue);
        //     _onJump.Raise();
        // }


        public override Type CheckTransitions()
        {
            return typeof(FallState);
        }
    }
}