namespace Project.Player
{
    using System.Collections;
    using Project.Core;
    using UnityEngine;

    [ChildStateOf(typeof(AirState))]
    [StateOf(typeof(PlayerStateController))]
    sealed class AirDashState : ChildState
    {
        [InjectField] private PlayerData _playerData;
        [InjectField] private GameObject _root;
        private PlayerManager _playerManager;


        public override void EnterState()
        {
            if (_playerManager == null)
            {
                _playerManager = _root.GetComponent<PlayerManager>();
            }

            _playerManager.StartCoroutine(SetDash());
        }


        public override void ExitState()
        {
            _playerData.CanDash = false;
        }


        private IEnumerator SetDash()
        {
            _playerData.CanDash = true;

            yield return null;

            _playerData.CanDash = false;
        }
    }
}