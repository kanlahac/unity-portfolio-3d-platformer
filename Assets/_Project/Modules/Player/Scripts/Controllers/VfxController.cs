namespace Project.Player
{
    using Project.Core;
    using UnityEngine;

    sealed class VfxController : Controller, IEnable, IDisable
    {
        [InjectField] private ParticleSystem _jumpSmoke;
        [InjectField] private InputReader _inputReader;
        [InjectField] private BooleanVariable _isGroundedStatus;


        public void OnEnable()
        {
            _inputReader.jumpEvent += HandleJump;
        }


        public void OnDisable()
        {
            _inputReader.jumpEvent -= HandleJump;
        }


        private void HandleJump(float jumpValue)
        {
            if (_isGroundedStatus.runtimeValue == false)
                return;

            _jumpSmoke.Play();
        }
    }
}