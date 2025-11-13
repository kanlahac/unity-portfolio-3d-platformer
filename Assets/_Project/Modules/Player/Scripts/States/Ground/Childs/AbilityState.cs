namespace Project.Player
{
    using System.Collections;
    using Project.Core;
    using UnityEngine;

    [ChildStateOf(typeof(GroundState))]
    [StateOf(typeof(PlayerStateController))]
    sealed class AbilityState : ChildState
    {
        [InjectField] private PlayerData _playerData;
        [InjectField] private MonoBehaviour _host;


        public override void EnterState()
        {
            _host.StartCoroutine(SetAbility());
        }


        public override void ExitState()
        {
            _playerData.CanUseAbility = false;
        }


        private IEnumerator SetAbility()
        {
            _playerData.CanUseAbility = true;

            yield return null;

            _playerData.CanUseAbility = false;
        }
    }
}