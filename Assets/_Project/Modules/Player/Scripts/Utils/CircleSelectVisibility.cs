namespace Project.Player
{
    using Project.Core;
    using UnityEngine;

    [RequireComponent(typeof(ParticleSystem))]
    sealed class CircleSelectVisibility : MonoBehaviour
    {
        [Header("Status")]
        [SerializeField] private BooleanVariable _isControlledByPlayer;
        [SerializeField] private BooleanVariable _isGroundedStatus;

        private ParticleSystemRenderer _particleRenderer;


        private void Awake() => _particleRenderer = GetComponent<ParticleSystemRenderer>();
        private void LateUpdate() => _particleRenderer.enabled = CheckStaus();


        private bool CheckStaus()
        {
            return
                _isGroundedStatus.runtimeValue == true &&
                _isControlledByPlayer.runtimeValue == true;
        }
    }
}