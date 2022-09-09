using System;
using DG.Tweening;
using Doozy.Runtime.UIManager.Components;
using EFUK;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Tameru.View
{
    [RequireComponent(typeof(UIButton))]
    public abstract class BaseButtonView : MonoBehaviour
    {
        [SerializeField] private SeType seType = default;

        protected Action push;

        private readonly float _animationTime = 0.1f;

        public void Init(Action<SeType> action)
        {
            var rectTransform = transform.ConvertRectTransform();
            var scale = rectTransform.localScale;

            push += () =>
            {
                // 押下時のアニメーション
                DOTween.Sequence()
                    .Append(rectTransform
                        .DOScale(scale * 0.8f, _animationTime))
                    .Append(rectTransform
                        .DOScale(scale, _animationTime))
                    .SetLink(gameObject);

                action?.Invoke(seType);
            };

            GetComponent<UIButton>()
                .OnPointerDownAsObservable()
                .Subscribe(_ => push?.Invoke())
                .AddTo(this);
        }
    }
}