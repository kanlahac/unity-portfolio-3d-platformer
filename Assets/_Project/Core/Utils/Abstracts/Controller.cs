namespace Project.Core
{
    public abstract class Controller 
    {
        public virtual void AwakeController(DependencyProvider dependencyContainer)
        {
            DependencyInjectorService.AutoInject(this, dependencyContainer);
        }
    } 
}