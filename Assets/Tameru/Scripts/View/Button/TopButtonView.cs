using DG.Tweening;
using EFUK;
using UnityEngine;

namespace Tameru.View
{
    /// <summary>
    /// DoozyUI が機能しないため、似たような機能を実装している
    /// </summary>
    public sealed class TopButtonView : BaseButtonView
    {
        [SerializeField] private bool isInitHide = default;
        [SerializeField] private CanvasGroup canvasGroup = default;
        private CanvasGroup _parentGroup;
        private const float _animationTime = 0.3f;

        public void Init()
        {
            _parentGroup = transform.parent.GetComponent<CanvasGroup>();
            _parentGroup.transform.ConvertRectTransform().anchoredPosition = Vector2.zero;
            canvasGroup.transform.ConvertRectTransform().anchoredPosition = Vector2.zero;

            if (isInitHide)
            {
                HideParent(0.0f);
            }

            push += () =>
            {
                Show();
                HideParent();
            };
        }

        private void Show(float animationTime = _animationTime)
        {
            canvasGroup.transform.ConvertRectTransform().anchoredPosition = Vector2.zero;
            canvasGroup.transform.ConvertRectTransform().localScale = Vector3.one * 0.8f;

            DOTween.Sequence()
                .Append(canvasGroup
                    .DOFade(1.0f, animationTime)
                    .SetEase(Ease.OutBack))
                .Join(canvasGroup.transform.ConvertRectTransform()
                    .DOScale(Vector3.one, animationTime)
                    .SetEase(Ease.OutBack))
                .OnComplete(() => canvasGroup.blocksRaycasts = true);
        }

        private void HideParent(float animationTime = _animationTime)
        {
            _parentGroup.blocksRaycasts = false;

            DOTween.Sequence()
                .Append(_parentGroup
                    .DOFade(0.0f, animationTime)
                    .SetEase(Ease.OutQuart))
                .Join(_parentGroup.transform.ConvertRectTransform()
                    .DOAnchorPosY(-100.0f, animationTime)
                    .SetEase(Ease.OutQuart));
        }
    }
}