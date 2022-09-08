using System;
using UniRx;

namespace Tameru.Entity
{
    public sealed class SceneEntity
    {
        private readonly Subject<SceneName> _load;

        public SceneEntity()
        {
            _load = new Subject<SceneName>();
        }

        /// <summary>
        /// シーン遷移のイベント発行
        /// </summary>
        /// <param name="sceneName">遷移先のシーン名</param>
        public void SetUpLoad(SceneName sceneName)
        {
            _load?.OnNext(sceneName);
        }

        public IObservable<SceneName> Load()
        {
            return _load
                .Where(x => x != SceneName.None);
        }
    }
}