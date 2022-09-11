using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Tameru.View
{
    public sealed class GameStateView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI stateText = default;

        public async UniTask ShowReadyAsync(CancellationToken token)
        {
            var time = UiConfig.READY_TIME / 3.0f;
            await stateText
                .DOText("Ready...?", time)
                .SetEase(Ease.Linear)
                .WithCancellation(token);

            // 一時待機
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);

            stateText.text = $"Start!!";
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);

            stateText.text = $"";
        }

        public async UniTask ShowClearAsync(CancellationToken token)
        {
            await stateText
                .DOText("GAME CLEAR", UiConfig.FINISH_TIME)
                .SetEase(Ease.Linear)
                .WithCancellation(token);

            await UniTask.Delay(TimeSpan.FromSeconds(1.5f), cancellationToken: token);
        }

        public async UniTask ShowOverAsync(CancellationToken token)
        {
            await stateText
                .DOText("GAME OVER", UiConfig.FINISH_TIME)
                .SetEase(Ease.Linear)
                .WithCancellation(token);

            await UniTask.Delay(TimeSpan.FromSeconds(1.5f), cancellationToken: token);
        }
    }
}