namespace Project.Core
{
    public abstract class ChildState : State
    {
        public bool ExitFlag { get; protected set; } = false;
    }
}