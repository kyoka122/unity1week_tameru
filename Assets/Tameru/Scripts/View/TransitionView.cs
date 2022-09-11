using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MPUIKIT;
using UnityEngine;

namespace Tameru.View
{
    public sealed class TransitionView : MonoBehaviour
    {
        [SerializeField] private MPImage mask = default;
        private float minStrokeWidth = 0.01f;
        private float maxStrokeWidth = 275.0f;
        private static readonly int _strokeWidth = Shader.PropertyToID("_StrokeWidth");

        public async UniTask ShowAsync(CancellationToken token)
        {
            Activate(true);

            await DOTween.To(
                () => mask.material.GetFloat(_strokeWidth),
                x => mask.material.SetFloat(_strokeWidth, x),
                maxStrokeWidth,
                UiConfig.FADE_TIME)
                .SetEase(Ease.OutQuart)
                .WithCancellation(token);
        }

        public async UniTask HideAsync(CancellationToken token)
        {
            await DOTween.To(
                    () => mask.material.GetFloat(_strokeWidth),
                    x => mask.material.SetFloat(_strokeWidth, x),
                    minStrokeWidth,
                    UiConfig.FADE_TIME)
                .SetEase(Ease.InQuart)
                .WithCancellation(token);

            Activate(false);
        }

        private void Activate(bool value)
        {
            mask.raycastTarget = value;
        }
    }
}