namespace Project.Core
{
    public abstract class Controller
    {
        public virtual void AwakeController(DependencyProvider dependencyProvider)
        {
            dependencyProvider.Injector.AutoInject(this, dependencyProvider);
        }
    } 
}