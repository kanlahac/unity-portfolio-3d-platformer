namespace Project.Player
{
    using Project.Core;
    using UnityEngine;

    sealed class VfxController : Controller, IEnable, IDisable
    {
        [InjectField] private ParticleSystem _jumpSmoke;
        [InjectField] private StandardEvent _onJump;


        public void OnEnable()
        {
            _onJump.AddListener(HandleJump);
        }


        public void OnDisable()
        {
            _onJump.RemoveListener(HandleJump);
        }


        private void HandleJump() => _jumpSmoke.Play();
    }
}