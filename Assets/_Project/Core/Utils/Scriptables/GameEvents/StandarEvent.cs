namespace Project.Core
{
    using System;
    using UnityEngine;


    [CreateAssetMenu(fileName = "StandardEvent", menuName = "Scriptable Objects/Event/Standard")]
    public class StandardEvent : ScriptableObject
    {
        private event Action OnEvent;


        public void AddListener(Action listener) => OnEvent += listener;
        public void RemoveListener(Action listener) => OnEvent -= listener;
        public void Raise() => OnEvent?.Invoke();
    }
}