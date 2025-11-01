namespace Project.Player
{
    using DG.Tweening;
    using Project.Core;
    using UnityEngine;

    sealed class LightMagicController : Controller, IEnable, IDisable, ILateUpdate
    {
        [InjectField] private Transform _characterTransform;
        [InjectField] private Transform _lightMagicTransform;
        [InjectField] private Transform _lightMagicEffectTransform;


        public void OnEnable()
        {
            FloatingMove();
        }


        public void OnDisable()
        {
            _lightMagicEffectTransform.DOKill();
        }


        public void LateUpdate(float deltaTime)
        {
            FollowingMove();
        }


        private void FloatingMove()
        {
            Vector3 randomLocalTarget = Random.insideUnitSphere * 2f;

            _lightMagicEffectTransform
                .DOLocalMove(randomLocalTarget, Random.Range(2f, 4f))
                .SetEase(Ease.InOutSine)
                .OnComplete(FloatingMove);
        }
        

        private void FollowingMove()
        {
            Vector3 targetPosition = _characterTransform.position;
            targetPosition.y = 7f;

            _lightMagicTransform.DOKill();

            _lightMagicTransform
                .DOMove(targetPosition, 4.6f)
                .SetSpeedBased(true)
                .SetEase(Ease.OutSine);
        }
    }
}
