namespace Project.Player
{
    using DG.Tweening;
    using Project.Core;
    using UnityEngine;

    sealed class LightMagicController : Controller
    {
        [InjectField] private Transform _characterModel;
        [InjectField] private Transform _lightMagicTransform;
        [InjectField] private Transform _lightMagicEffectTransform;
        [InjectField] private GameObject _root;


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

            _lightMagicEffectTransform.DOKill();

            _lightMagicEffectTransform
                .DOLocalMove(randomLocalTarget, Random.Range(2f, 4f))
                .SetTarget(_root)
                .SetEase(Ease.InOutSine)
                .OnComplete(FloatingMove);
        }
        

        private void FollowingMove()
        {
            Vector3 targetPosition = _characterModel.position + (Vector3.up * 6f);

            _lightMagicTransform.DOKill();

            _lightMagicTransform
                .DOMove(targetPosition, 4.6f)
                .SetTarget(_root)
                .SetSpeedBased(true)
                .SetEase(Ease.OutSine);
        }
    }
}
