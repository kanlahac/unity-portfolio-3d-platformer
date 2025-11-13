namespace Project.Core
{
    using System;
    using System.Collections.Generic;

    public abstract class ParentState : State
    {
        protected Dictionary<Type, ChildState> _childStates = new();
        protected List<ChildState> _activeChildState = new();


        public override void UpdateState(float deltaTime)
        {
            for (int i = _activeChildState.Count - 1; i >= 0; i--)
            {
                _activeChildState[i].UpdateState(deltaTime);
            }

            CheckTransitions();
            CheckExitFlag();
        }


        public void AddChildState(Type childType, ChildState childState)
        {
            _childStates.Add(childType, childState);
        }


        public override void ExitState()
        {
            _activeChildState.ForEach(
                state => state.ExitState()
            );

            _activeChildState.Clear();
        }


        protected void ActivateChildState(Type stateType)
        {
            if (_childStates.TryGetValue(stateType, out ChildState state))
            {
                if (_activeChildState.Contains(state)) 
                    return;

                state.EnterState();
                _activeChildState.Add(state);
            }
        }


        protected void DeactivateChildState(Type stateType)
        {
            if (_childStates.TryGetValue(stateType, out ChildState state))
            {
                if (!_activeChildState.Contains(state))
                    return;
                    
                state.ExitState();
                _activeChildState.Remove(state);
            }
        }


        protected abstract void CheckTransitions();


        private void CheckExitFlag()
        {
            if (_activeChildState == null) return;

            for (int i = _activeChildState.Count - 1; i >= 0; i--)
            {
                ChildState state = _activeChildState[i];

                if (state.ExitFlag == false) continue;

                state.ExitState();
                _activeChildState.Remove(state);
            }
        }
    }
}