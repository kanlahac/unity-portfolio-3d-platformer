namespace Project.Core
{
    using System;
    
    public abstract class State
    {
        public virtual void AwakeState(DependencyProvider dependencyProvider)
        {
            DependencyInjectorService.AutoInject(this, dependencyProvider);
        }


        public virtual void UpdateState(float deltaTime) { return; }
        public virtual void EnterState() { return; }
        public virtual void ExitState() { return; }
        public virtual Type CheckTransitions() { return null; }
    }
}