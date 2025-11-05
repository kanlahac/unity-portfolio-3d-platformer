namespace Project.Player
{
    using System;
    using Project.Core;
    using Unity.Cinemachine;
    using UnityEngine;

    [Serializable]
    sealed class DependencyContainer : DependencyProvider
    {
        [field: SerializeField, Header("Module")] public InputReader InputReader { get; private set; }
        [field: SerializeField] public ModuleData PlayerData { get; private set; }

        [field: SerializeField, Header("Objects")] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Transform CharacterModel { get; private set; }
        [field: SerializeField] public Transform LightMagicTransform { get; private set; }
        [field: SerializeField] public Transform LightMagicEffectTransform { get; private set; }
    }
}