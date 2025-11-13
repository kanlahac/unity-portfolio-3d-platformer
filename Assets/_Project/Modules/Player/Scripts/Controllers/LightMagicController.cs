
namespace Project.Player
{
    using System.Collections;
    using DG.Tweening;
    using Project.Core;
    using UnityEngine;

    sealed class LightMagicController : Controller, ILateUpdate, IAwake, IDisable
    {
        [InjectField] private PlayerData _playerData;
        [InjectField] private Transform _characterModel;
        [InjectField] private Transform _lightMagicTransform;
        [InjectField] private Transform _staffTransform;
        [InjectField] private Light _lightMagic;
        [InjectField] private GameObject _root;
        [InjectField] private MonoBehaviour _host;
        private Transform _lightMagicEffectTransform;
        private Coroutine _floatingMoveCorutine;
        private Coroutine _handleEffectCorroutine;
        private float _initialIntensity;
        private Sequence _effectSequence;
        private bool _isActive;


        public void Awake()
        {
            _lightMagicEffectTransform = _lightMagicTransform.GetChild(0);
            _initialIntensity = _lightMagic.intensity;

            HideEffect();
        }


        public void OnDisable()
        {
            HideEffect();
            StopAll();
        }


        public void LateUpdate(float deltaTime)
        {
            FollowingMove(deltaTime);

            if (_playerData.CanUseAbility)
                HandleEffect();
        }


        private IEnumerator FloatingMove()
        {
            while (true)
            {
                Vector3 randomLocalTarget = Random.insideUnitSphere * 2f;
                float duration = Random.Range(0.5f, 1f);

                _lightMagicEffectTransform.DOKill();

                yield return _lightMagicEffectTransform
                    .DOLocalMove(randomLocalTarget, duration)
                    .SetTarget(_root)
                    .SetSpeedBased(true)
                    .SetEase(Ease.InOutSine)
                    .WaitForCompletion();
            }
        }


        private void FollowingMove(float deltaTime)
        {
            if (_isActive)
            {
                Vector3 targetPosition = _characterModel.position + (Vector3.up * 6f);

                _lightMagicTransform.position = Vector3.Lerp(
                    _lightMagicTransform.position,
                    targetPosition,
                    4.5f * deltaTime
                );
            }
            else
            {
                _lightMagicTransform.position = _staffTransform.position;
            }
        }


        private void HandleEffect()
        {
            if (_effectSequence != null) _effectSequence.Kill();

            _lightMagicTransform.DOKill();
            HideEffect();
            
            _effectSequence = DOTween.Sequence();

            _effectSequence.Append(
                DOVirtual.Float(0f, 1f, 1f, (value) =>
                {
                    _lightMagic.intensity = _initialIntensity * value;
                    _lightMagicEffectTransform.localScale = Vector3.one * value;
                })
                .OnComplete(() => {
                    _isActive = true;
                    _floatingMoveCorutine = _host.StartCoroutine(FloatingMove());
                })
                .SetEase(Ease.InExpo)
            )
            .SetDelay(1f);

            _effectSequence.Append(
                DOVirtual.Float(1f, 0f, _playerData.AbilityDurationValue, (value) =>
                {
                    _lightMagic.intensity = _initialIntensity * value;
                    _lightMagicEffectTransform.localScale = Vector3.one * value;
                })
                .OnComplete(() => HideEffect())
                .SetEase(Ease.InExpo)
            );
        }


        private void HideEffect()
        {
            _lightMagic.intensity = 0;
            _lightMagicEffectTransform.localScale = Vector3.zero;
            _lightMagicTransform.position = _staffTransform.position;
            _isActive = false;

            if (_floatingMoveCorutine != null)
                _host.StopCoroutine(_floatingMoveCorutine);
                
            _lightMagicEffectTransform.localPosition = Vector3.zero;
        }


        private void StopAll()
        {
            _lightMagicEffectTransform.DOKill();
            _lightMagicTransform.DOKill();

            if (_floatingMoveCorutine != null)
                _host.StopCoroutine(_floatingMoveCorutine);

            if (_handleEffectCorroutine != null)
                _host.StopCoroutine(_handleEffectCorroutine);
        }
    }
}
