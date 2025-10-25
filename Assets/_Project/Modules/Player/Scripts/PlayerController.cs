namespace Project.Player
{
    using Project.Core;
    using UnityEngine;

    sealed partial class PlayerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ActionBase[] _playerActions;
        [SerializeField] private ActionBase[] _aiActions;

        [Header("Status")]
        [SerializeField] private BooleanVariable _isControlledByPlayer;


        private void OnEnable()
        {
            foreach (ActionBase action in _playerActions)
            {
                action.EnableAction();
            }

            foreach (ActionBase action in _aiActions)
            {
                action.EnableAction();
            }
        }


        private void OnDisable()
        {
            foreach (ActionBase action in _playerActions)
            {
                action.DisableAction();
            }

            foreach (ActionBase action in _aiActions)
            {
                action.DisableAction();
            }
        }


        private void FixedUpdate()
        {
            if (_isControlledByPlayer.runtimeValue == true)
            {
                foreach (ActionBase action in _playerActions)
                {
                    action.UpdateAction(Time.deltaTime);
                }
            }
            else
            {
                foreach (ActionBase action in _aiActions)
                {
                    action.UpdateAction(Time.deltaTime);
                }
            }
        }  
    }
}