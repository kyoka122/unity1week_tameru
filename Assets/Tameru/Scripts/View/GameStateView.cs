using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Tameru.View
{
    public sealed class GameStateView : MonoBehaviour
    {
        public async UniTask ShowClearAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public async UniTask ShowOverAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }
    }
}