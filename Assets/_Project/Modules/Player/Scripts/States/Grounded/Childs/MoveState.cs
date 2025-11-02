namespace Project.Player
{
    using System;
    using Project.Core;
    using UnityEngine;


    [StateOf(typeof(PlayerStateController))]
    [ChildStateOf(typeof(GroundedState))]
    sealed class MoveState : ChildState
    {
        [InjectField] private FloatVariable _moveValue;
        [InjectField] private Vector3Variable _horizontalMoveStatus;
        [InjectField] private Transform _characterTransform;
        [InjectField] private InputReader _inputReader;
        private Vector2 _inputValue;


        public override void EnterState()
        {
           _inputReader.moveEvent += HandleMove;
        } 


        public override void ExitState()
        {
            _inputReader.moveEvent -= HandleMove;
        } 


        private void HandleMove(Vector2 inputValue)
        {
            _inputValue = inputValue;
        }


        public override void UpdateState(float deltaTime)
        {
            Vector3 moveForce = new Vector3(_inputValue.x, 0f, _inputValue.y);
            Vector3 flatMovement = new Vector3(moveForce.x, 0, moveForce.z);

            _horizontalMoveStatus.runtimeValue = moveForce * _moveValue.runtimeValue;

            if (flatMovement.sqrMagnitude >= 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(flatMovement);

                _characterTransform.rotation = Quaternion.Slerp(
                    _characterTransform.rotation,
                    targetRotation,
                    15f * deltaTime
                );
            }
        }
    }
}