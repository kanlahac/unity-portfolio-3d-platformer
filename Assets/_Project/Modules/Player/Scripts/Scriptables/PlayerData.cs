namespace Project.Player
{
    using System;
    using Project.Core;
    using UnityEngine;

    [CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/Module Data/Player")]
    public class ModuleData : Core.ModuleData
    {
        [field: SerializeField, Header("Base Values")] 
        public float BaseMoveValue { get; private set; }
        [field: SerializeField] public float BaseJumpValue { get; private set; }
        [field: SerializeField] public float BaseDashValue { get; private set; }
        [field: SerializeField] public float BaseDashCooldownValue { get; private set; }
        [field: SerializeField] public float BaseGravityValue { get; private set; }

        [Header("Runtime Values")]
        public float MoveValue;
        public float JumpValue;
        public float DashValue;
        public float DashCooldownValue;
        public float GravityValue;

        [Header("Info")]
         public Vector3 GlobalPosition;
        public Vector3 Velocity;
        public Vector3 LookingDirection;
        public bool IsGrounded;

        [Header("Forces")]
        public Vector3 HorizontalForce;
        public Vector3 VerticalForce;
        public Vector3 ExternalForce;

        [Header("States")]
        public bool CanJump;
        public bool CanDash;
        public bool CanMove;
        public bool CanApplyGravity;


        protected override void OnEnable()
        {
            base.OnEnable();

            ResetData();
            ResetRuntime();
        }


        public void ResetData()
        {
            MoveValue = BaseMoveValue;
            JumpValue = BaseJumpValue;
            DashValue = BaseDashValue;
            DashCooldownValue = BaseDashCooldownValue;
            GravityValue = BaseGravityValue;
        }


        public void ResetRuntime()
        {
            GlobalPosition = Vector3.zero;
            Velocity = Vector3.zero;
            HorizontalForce = Vector3.zero;
            VerticalForce = Vector3.zero;
            ExternalForce = Vector3.zero;
            LookingDirection = Vector3.zero;
            IsGrounded = false;
            CanJump = false;
            CanDash = false;
            CanMove = false;
            CanApplyGravity = false;
        }
    }
}
