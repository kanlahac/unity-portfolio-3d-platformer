namespace Project.Player
{
    using UnityEngine;

    sealed partial class PlayerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ActionBase[] _playerActions;


        private void OnEnable()
        {
            foreach (ActionBase action in _playerActions)
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
        }


        private void FixedUpdate()
        {
            foreach (ActionBase action in _playerActions)
            {
                action.UpdateAction(Time.deltaTime);
            }
        }  
    }
}