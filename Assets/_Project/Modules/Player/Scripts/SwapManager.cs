namespace Project.Player
{
    using Project.Core;
    using UnityEngine;

    sealed class SwapManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SwapController _magePlayerController;
        [SerializeField] private SwapController _archerPlayerController;

        [Header("Events")]
        [SerializeField] private StandardEvent _onSwap;

        private SwapController _activePlayer;


        private void OnEnable() => _onSwap.AddListener(HandleSwap);
        private void OnDisable() => _onSwap.RemoveListener(HandleSwap);


        private void Start()
        {
            _magePlayerController.SwapControl(false);
            _archerPlayerController.SwapControl(false);

            _activePlayer = _magePlayerController;
            _activePlayer.SwapControl(true);
        }


        private void HandleSwap()
        {
            if (_activePlayer == null) return;

            _activePlayer.SwapControl(false);

            if (_activePlayer == _magePlayerController)
            {
                _activePlayer = _archerPlayerController;
            }
            else
            {
                _activePlayer = _magePlayerController;
            }

            _activePlayer.SwapControl(true);
        }
    }
}