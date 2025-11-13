namespace Project.Core
{
    using System;
    using UnityEngine;

    [Serializable]
    public abstract class DependencyProvider
    {
        public GameObject Root { get; private set; }
        public MonoBehaviour Host { get; private set; }

        private readonly Lazy<IDependencyInjector> _injector = new Lazy<IDependencyInjector>(
            () => new DependencyInjectorService()
        );

        public IDependencyInjector Injector => _injector.Value;

        private readonly Lazy<IFactory> _factory = new Lazy<IFactory>(
            () => new FactoryService()
        );
        
        public IFactory Factory => _factory.Value;


        public void AddManager(GameObject rootObject)
        {
            Root = rootObject;
            MonoBehaviour[] components = rootObject.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour component in components)
            {
                if (component == null) continue;

                Type currentType = component.GetType();

                while (currentType != null)
                {
    
                    if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == typeof(Manager<>))
                    {
                        Host = component;
                        return; 
                    }

                    currentType = currentType.BaseType;
                }
            }
        }
    }
}