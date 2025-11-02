namespace Project.Core
{
    using UnityEngine;

    public abstract class BaseVariable<T> : ScriptableObject
    {
        [field: SerializeField] public T baseValue { get; private set; }
        public T runtimeValue;


        private void OnEnable() => ResetValue();
        public void ResetValue() => runtimeValue = baseValue;
    }
}