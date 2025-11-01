namespace Project.Player
{
    using Project.Core;
    using UnityEngine;

    sealed class CircleSelectController : Controller, ILateUpdate, IAwake
    {
        [InjectField] private BooleanVariable _isControlledByPlayerStatus;
        [InjectField] private BooleanVariable _isGroundedStatus;
        [InjectField] private ParticleSystem _circleSelected;

        private ParticleSystemRenderer _particleRenderer;


        public void Awake()
        {
            _particleRenderer = _circleSelected.GetComponent<ParticleSystemRenderer>();
        }


        public void LateUpdate(float deltaTime) => _particleRenderer.enabled = CheckStaus();


        private bool CheckStaus()
        {
            return
                _isGroundedStatus.runtimeValue == true &&
                _isControlledByPlayerStatus.runtimeValue == true;
        }
    }
}