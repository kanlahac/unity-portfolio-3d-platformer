namespace Project.Core
{
    using System;
    using System.Collections.Generic;

    public class FactoryStateResponse
    {
        public List<State> allStates;
        public Dictionary<Type, ParentState> parentStates;


        public FactoryStateResponse(List<State> allStates, Dictionary<Type, ParentState> parentStates)
        {
            this.allStates = allStates;
            this.parentStates = parentStates;
        }
    }
}