namespace Project.Core
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public interface IFactory
    {
        public FactoryStateResponse CreateStates(Assembly assembly, Type stateMachineType, DependencyProvider dependencyProvider);
        public List<Controller> CreateControllers(Assembly assembly, DependencyProvider dependencyProvider);
    }
}