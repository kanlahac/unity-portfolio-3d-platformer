namespace Project.Core
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class StateOfAttribute : Attribute
    {
        public Type stateMachineType { get; }
        
        public StateOfAttribute(Type stateMachineType)
        {
            this.stateMachineType = stateMachineType;
        }
    }
}