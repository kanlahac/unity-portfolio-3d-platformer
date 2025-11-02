namespace Project.Player
{
    using Project.Core;
    using UnityEngine;

    sealed class VfxController : Controller, IEnable, IDisable
    {
        [InjectField] private ParticleSystem _jumpSmoke;
        [InjectField] private BooleanEvent _inputJumpEvent;


        public void OnEnable()
        {
            _inputJumpEvent.AddListener(HandleJump);
        }


        public void OnDisable()
        {
            _inputJumpEvent.RemoveListener(HandleJump);
        }


        private void HandleJump(bool isPerformed)
        {
            if (isPerformed == true)
            {
                _jumpSmoke.Play();
            }
        }
    }
}