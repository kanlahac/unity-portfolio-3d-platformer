namespace Project.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class FactoryService
    {
        public static FactoryStateResponse StateFactory(Assembly assembly, Type stateMachineType, DependencyProvider dependencyProvider)
        {
            Type stateType = typeof(State);

            List<Type> handlerTypes = assembly.GetTypes()
                .Where(
                    item =>
                        item.IsClass &&
                        !item.IsAbstract &&
                        stateType.IsAssignableFrom(item) &&
                        item.IsDefined(typeof(StateOfAttribute), false) &&
                        item.GetCustomAttribute<StateOfAttribute>()?.stateMachineType == stateMachineType
                ).ToList();

            Dictionary<Type, ParentState> parentStates = new();
            Dictionary<Type, ChildState> childStates = new();
            List<State> allStates = new();

            handlerTypes.ForEach(
                type =>
                {
                    State instance = Activator.CreateInstance(type) as State;

                    instance.AwakeState(dependencyProvider);
                    allStates.Add(instance);

                    if (instance is ParentState parentInstance)
                    {
                        parentStates.Add(type, parentInstance);
                    }
                    else if (instance is ChildState childInstance)
                    {
                        childStates.Add(type, childInstance);
                    }
                }
            );

            foreach (var childPair in childStates)
            {
                Type childType = childPair.Key;
                ChildState childState = childPair.Value;

                ChildStateOfAttribute attribute = (ChildStateOfAttribute)Attribute.GetCustomAttribute(
                    childType,
                    typeof(ChildStateOfAttribute)
                );

                if (attribute != null)
                {
                    Type parentType = attribute.parentStateType;

                    if (parentStates.TryGetValue(parentType, out ParentState parentInstance))
                    {
                        parentInstance.AddChildState(childType, childState);
                    }
                }
            }

            return new FactoryStateResponse(allStates, parentStates);
        }


        public static List<Controller> ControllerFactory(Assembly assembly, DependencyProvider dependencyProvider)
        {
            Type controllerType = typeof(Controller);

            List<Type> controllerTypes = assembly.GetTypes()
                .Where(
                    item =>
                        item.IsClass &&
                        !item.IsAbstract &&
                        controllerType.IsAssignableFrom(item)
                ).ToList();

            List<Controller> instances = new();

            controllerTypes.ForEach(
                type =>
                {
                    Controller instance = Activator.CreateInstance(type) as Controller;

                    instance.AwakeController(dependencyProvider);
                    instances.Add(instance);
                }
            );

            return instances;
        }
    }
}