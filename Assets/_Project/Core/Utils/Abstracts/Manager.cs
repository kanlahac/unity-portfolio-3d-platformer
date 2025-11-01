namespace Project.Core
{
    using System.Collections.Generic;
    using UnityEngine;
    using System.Reflection;

    public abstract class Manager<T> : MonoBehaviour
        where T : DependencyProvider, new()
    {
        [SerializeField] private T _dependencyProvider = new T();
        protected List<Controller> _controllers = new();


        protected virtual void Awake()
        {
            Assembly assembly = GetType().Assembly;
            _controllers = FactoryService.ControllerFactory(assembly, _dependencyProvider);

            foreach (var controller in _controllers)
            {
                if (controller is IAwake item)
                    item.Awake();
            }
        }


        protected virtual void Start()
        {
            foreach (var controller in _controllers)
            {
                if (controller is IStart item)
                    item.Start();
            }
        }


        protected virtual void OnEnable()
        {
            foreach (var controller in _controllers)
            {
                if (controller is IEnable item)
                    item.OnEnable();
            }
        }


        protected virtual void OnDisable()
        {
            foreach (var controller in _controllers)
            {
                if (controller is IDisable item)
                    item.OnDisable();
            }
        }


        protected void Update()
        {
            float deltaTime = Time.deltaTime;

            foreach (var controller in _controllers)
            {
                if (controller is IUpdate item)
                    item.Update(deltaTime);
            }
        }


        protected virtual void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;

            foreach (var controller in _controllers)
            {
                if (controller is IFixedUpdate item)
                    item.FixedUpdate(deltaTime);
            }
        }


        protected virtual void LateUpdate()
        {
            float deltaTime = Time.deltaTime;

            foreach (var controller in _controllers)
            {
                if (controller is ILateUpdate item)
                    item.LateUpdate(deltaTime);
            }
        }


        protected virtual void OnDestroy()
        {
            OnDisable();
            _controllers.Clear();
        }
    } 
}