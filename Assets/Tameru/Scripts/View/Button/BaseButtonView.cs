using System;
using DG.Tweening;
using Doozy.Runtime.UIManager.Components;
using EFUK;
using MPUIKIT;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Tameru.View
{
    [RequireComponent(typeof(UIButton))]
    public abstract class BaseButtonView : MonoBehaviour
    {
        [SerializeField] private SeType pushSe = default;
        [SerializeField] private SeType cursorOverSe = default;

        protected Action push;
        private bool _isActive = true;

        private readonly float _animationTime = 0.1f;

        public void Init(Action<SeType> pushed, Action<SeType> cursorOver)
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

                pushed?.Invoke(pushSe);
            };

            var button = GetComponent<UIButton>();
            button
                .OnPointerDownAsObservable()
                .Where(_ => _isActive)
                .Subscribe(_ => push?.Invoke())
                .AddTo(this);

            var image = button.GetComponent<MPImage>();
            button
                .OnPointerEnterAsObservable()
                .Where(_ => _isActive)
                .Where(_ => cursorOverSe != SeType.None)
                .Subscribe(_ =>
                {
                    image.color = Color.yellow;
                    cursorOver?.Invoke(cursorOverSe);
                })
                .AddTo(this);

            var defaultColor = image.color;
            button
                .OnPointerExitAsObservable()
                .Where(_ => _isActive)
                .Where(_ => cursorOverSe != SeType.None)
                .Subscribe(_ =>
                {
                    image.color = defaultColor;
                })
                .AddTo(this);
        }

        public void Activate(bool value)
        {
            _isActive = value;
        }
    }
}