namespace Project.Core
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class StateOfAttribute : Attribute
    {
        public Type StateMachineType { get; }
        
        public StateOfAttribute(Type stateMachineType)
        {
            StateMachineType = stateMachineType;
        }
    }
}