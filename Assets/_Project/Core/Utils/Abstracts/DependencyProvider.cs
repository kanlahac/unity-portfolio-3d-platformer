namespace Project.Core
{
    using System;
    using UnityEngine;

    [Serializable]
    public abstract class DependencyProvider
    {
        public GameObject Root { get; private set; }


        public void AddManager(GameObject root)
        {
            Root = root;
        }
    }
}