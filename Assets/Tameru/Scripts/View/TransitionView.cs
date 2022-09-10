using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Tameru.View
{
    public sealed class TransitionView : MonoBehaviour
    {
        [SerializeField] private Image mask = default;

        private readonly float fadeTime = 0.5f;

        // TODO: 仮のフェード実装のため修正する
        public async UniTask ShowAsync(CancellationToken token)
        {
            Activate(true);

            await mask
                .DOFade(1.0f, fadeTime)
                .SetEase(Ease.Linear)
                .SetLink(gameObject)
                .WithCancellation(token);
        }

        // TODO: 仮のフェード実装のため修正する
        public async UniTask HideAsync(CancellationToken token)
        {
            await mask
                .DOFade(0.0f, fadeTime)
                .SetEase(Ease.Linear)
                .SetLink(gameObject)
                .WithCancellation(token);

            Activate(false);
        }

        private void Activate(bool value)
        {
            mask.raycastTarget = value;
        }
    }
}