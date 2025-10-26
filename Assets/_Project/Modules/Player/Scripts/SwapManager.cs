namespace Project.Player
{
    using Project.Core;
    using UnityEngine;
    using UnityEngine.InputSystem;

    sealed class SwapManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InputActionReference _swapInput;
        [SerializeField] private SwapController _magePlayerController;
        [SerializeField] private SwapController _archerPlayerController;

        [Header("Events")]
        [SerializeField] private StandardEvent _onSwap;

        private SwapController _activePlayer;


        private void OnEnable()
        {
            _swapInput.action.Enable();

            _swapInput.action.performed += HandleInput;
        }


        private void OnDisable()
        {
            _swapInput.action.performed -= HandleInput;

            _swapInput.action.Disable();
        }


        private void Start()
        {
            _magePlayerController.SwapControl(false);
            _archerPlayerController.SwapControl(false);

            _activePlayer = _magePlayerController;
            _activePlayer.SwapControl(true);
        }


        private void HandleInput(InputAction.CallbackContext context)
        {
            if (_activePlayer == null) return;
            if (_activePlayer.isGrounded == false) return;
           
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
            _onSwap.Raise();
        }
    }
}