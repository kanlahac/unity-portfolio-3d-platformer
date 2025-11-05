namespace Project.Core
{
    using System;
    using UnityEngine;

    public abstract class ModuleData : ScriptableObject
    {
        public string ModuleName { get; private set; }


        protected virtual void OnEnable()
        {
            Type type = GetType();
            ModuleName = type.Name;
        }
    }
}

