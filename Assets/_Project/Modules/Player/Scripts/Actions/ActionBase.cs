namespace Project.Player
{
    using UnityEngine;

    public abstract class ActionBase : ScriptableObject
    {
        public virtual void EnableAction() { }
        public virtual void UpdateAction(float deltaTime) { }
        public virtual void DisableAction() { }
    }
}