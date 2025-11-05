namespace Project.Core
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class ChildStateOfAttribute : Attribute
    {
        public Type ParentStateType { get; }
        

        public ChildStateOfAttribute(Type parentStateType)
        {
            if (!typeof(State).IsAssignableFrom(parentStateType))
            {
                throw new ArgumentException($"The type {parentStateType.Name} must be State.");
            }

            ParentStateType = parentStateType;
        }
    }
}