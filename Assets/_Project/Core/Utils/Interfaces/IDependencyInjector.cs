namespace Project.Core
{
    public interface IDependencyInjector
    {
        public void AutoInject(object target, DependencyProvider provider);
    }
}