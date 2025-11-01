namespace Project.Core
{
    using System;
    using System.Collections.Generic;

    public abstract class ParentState : State
    {
        protected ChildState _currentChildState { get; private set; } = null;
        protected Dictionary<Type, ChildState> _childStates = new();


        public override void UpdateState(float deltaTime)
        {
            _currentChildState?.UpdateState(deltaTime);

            Type stateType = _currentChildState?.CheckTransitions();

            if (stateType != null)
                SetChildState(stateType);
        }


        public void AddChildState(Type childType, ChildState childState) => _childStates.Add(childType, childState);


        protected void SetChildState(Type stateType)
        {
            if (_childStates.TryGetValue(stateType, out ChildState newState))
            {
                if (_currentChildState != newState)
                {
                    _currentChildState?.ExitState();
                    _currentChildState = newState;
                    _currentChildState.EnterState();
                }
            }
        }
    }
}