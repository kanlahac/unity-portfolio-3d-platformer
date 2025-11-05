namespace Project.Core
{
    using System;
    
    public abstract class State
    {
        public virtual void AwakeState(DependencyProvider dependencyProvider)
        {
            DependencyInjectorService.AutoInject(this, dependencyProvider);
        }


        public abstract void EnterState();
        public abstract void ExitState();
        public virtual void UpdateState(float deltaTime) { }
    }
}