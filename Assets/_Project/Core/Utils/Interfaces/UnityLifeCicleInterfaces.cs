namespace Project.Core
{
    public interface IUpdate
    {
        public void Update(float deltaTime);
    }


    public interface IFixedUpdate
    {
        public void FixedUpdate(float deltaTime);
    }


    public interface ILateUpdate
    {
        public void LateUpdate(float deltaTime);
    }


    public interface IStart
    {
        public void Start();
    }


    public interface IEnable
    {
        public void OnEnable();
    }


    public interface IDisable
    {
        public void OnDisable();
    }


    public interface IAwake
    {
        public void Awake();
    }
}