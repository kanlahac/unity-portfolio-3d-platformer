namespace Project.Core
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEngine;

    public abstract class StateMachine : Controller, IUpdate, IDisable
    {
        private List<State> _allStates = new();
        private Dictionary<Type, ParentState> _parentStates = new();
        protected ParentState _currentState;


        public override void AwakeController(DependencyProvider dependencyProvider)
        {
            base.AwakeController(dependencyProvider);

            Type stateMachineType = GetType();
            Assembly assembly = stateMachineType.Assembly;
            FactoryStateResponse response = dependencyProvider.Factory.CreateStates(assembly, stateMachineType, dependencyProvider);

            _allStates = response.allStates;
            _parentStates = response.parentStates;
        }


        public abstract void CheckParentTransition();


        public virtual void OnDisable()
        {
            _allStates.ForEach(
                state => state.ExitState()
            );
        }


        public virtual void Update(float deltaTime)
        {
            _currentState?.UpdateState(deltaTime);

            CheckParentTransition();
        }


        protected virtual void SetParentState(Type stateType)
        {
            if (_parentStates.TryGetValue(stateType, out ParentState newState))
            {
                if (_currentState != newState)
                {
                    _currentState?.ExitState();
                    _currentState = newState;
                    _currentState.EnterState();
                }
            }
        }
    }
}