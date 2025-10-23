namespace Project.Core
{
    using System;
    using UnityEngine;

    public abstract class BaseEvent<T> : ScriptableObject
    {
        private event Action<T> OnEvent;


        public void AddListener(Action<T> listener) => OnEvent += listener;
        public void RemoveListener(Action<T> listener) => OnEvent -= listener;
        public void Raise(T value) => OnEvent?.Invoke(value);
    }
}