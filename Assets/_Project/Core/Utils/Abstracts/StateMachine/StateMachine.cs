namespace Project.Core
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public abstract class StateMachine : Controller, IUpdate, IDisable
    {
        private List<State> _allStates = new();
        private Dictionary<Type, ParentState> _parentStates = new();
        protected ParentState _currentState;


        public override void AwakeController(DependencyProvider dependencyContainer)
        {
            Type stateMachineType = GetType();
            Assembly assembly = stateMachineType.Assembly;
            FactoryStateResponse response = FactoryService.StateFactory(assembly, stateMachineType, dependencyContainer);

            _allStates = response.allStates;
            _parentStates = response.parentStates;

            Awake();
        }


        protected virtual void Awake() { return; }


        public void OnDisable()
        {
            _allStates.ForEach(
                state => state.ExitState()
            );
        }


        public void Update(float deltaTime)
        {
            _currentState?.UpdateState(deltaTime);

            Type stateType = _currentState?.CheckTransitions();

            if (stateType != null)
                SetParentState(stateType);
        }


        protected void SetParentState(Type stateType)
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