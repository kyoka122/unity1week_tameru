using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tameru.Entity;
using Tameru.View;
using UniRx;
using UnityEngine.SceneManagement;

namespace Tameru.Logic
{
    public sealed class SceneLogic : IDisposable
    {
        private readonly SceneEntity _sceneEntity;
        private readonly TransitionView _transitionView;
        private readonly CancellationTokenSource _tokenSource;

        public SceneLogic(SceneEntity sceneEntity, TransitionView transitionView)
        {
            _sceneEntity = sceneEntity;
            _transitionView = transitionView;
            _tokenSource = new CancellationTokenSource();

            _sceneEntity.Load()
                .Subscribe(x => FadeLoadAsync(x, _tokenSource.Token).Forget())
                .AddTo(_transitionView);
        }

        private async UniTask FadeLoadAsync(SceneName sceneName, CancellationToken token)
        {
            await _transitionView.ShowAsync(token);

            await SceneManager.LoadSceneAsync(sceneName.ToString());

            await _transitionView.HideAsync(token);
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}