namespace Project.Player
{
    using System;
    using Project.Core;
    using Unity.Cinemachine;
    using UnityEngine;
    using UnityEngine.AI;
    using UnityEngine.InputSystem;

    [Serializable]
    sealed class DependencyContainer : DependencyProvider
    {
        [field: SerializeField, Header("Inputs")] public InputActionReference moveInput { get; private set; }
        [field: SerializeField] public InputActionReference jumpInput { get; private set; }
        [field: SerializeField] public InputActionReference dashInput { get; private set; }
        [field: SerializeField] public InputActionReference swapInput { get; private set; }
        [field: SerializeField] public InputActionReference abilityInput { get; private set; }

        [field: SerializeField, Header("Objects")] public NavMeshAgent navMeshAgent { get; private set; }
        [field: SerializeField] public Animator animator { get; private set; }
        [field: SerializeField] public CharacterController characterController { get; private set; }
        [field: SerializeField] public CinemachineCamera characterCamera { get; private set; }
        [field: SerializeField] public Transform characterTransform { get; private set; }
        [field: SerializeField] public Transform lightMagicTransform { get; private set; }
        [field: SerializeField] public Transform lightMagicEffectTransform { get; private set; }

        [field: SerializeField, Header("Events")] public Vector3Event onAddExternalForce { get; private set; }
        [field: SerializeField] public StandardEvent onJump { get; private set; }
        [field: SerializeField] public StandardEvent onDash { get; private set; }
        [field: SerializeField] public StandardEvent onMoveStart { get; private set; }
        [field: SerializeField] public StandardEvent onMoveEnd { get; private set; }
        [field: SerializeField] public StandardEvent onSwap { get; private set; }

        [field: SerializeField, Header("Status")] public BooleanVariable isGroundedStatus { get; private set; }
        [field: SerializeField] public BooleanVariable isMovingStatus { get; private set; }
        [field: SerializeField] public BooleanVariable isControlledByPlayerStatus { get; private set; }
        [field: SerializeField] public Vector3Variable velocityStatus { get; private set; }

        [field: SerializeField, Header("Values")] public FloatVariable gravityValue { get; private set; }
        [field: SerializeField] public FloatVariable moveValue { get; private set; }
        [field: SerializeField] public FloatVariable jumpValue { get; private set; }
        [field: SerializeField] public FloatVariable dashValue { get; private set; }

        [field: SerializeField, Header("Vfx")] public ParticleSystem circleSelected { get; private set; }
        [field: SerializeField] public ParticleSystem jumpSmoke { get; private set; }
    }
}