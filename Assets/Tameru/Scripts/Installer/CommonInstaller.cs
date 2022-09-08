using Tameru.Entity;
using Tameru.Logic;
using Tameru.View;
using UnityEngine;

namespace Tameru.Installer
{
    public sealed class CommonInstaller : MonoBehaviour
    {
        // Singleton用のinstance
        // 他installerからのみアクセス可能とする
        private static CommonInstaller _instance;
        public static CommonInstaller Instance => _instance;

        // 他Logicで利用するためにEntityのみ公開する
        public SceneEntity sceneEntity { get; } = new SceneEntity();

        [SerializeField] private TransitionView transitionView = default;

        private void Awake()
        {
            // Singleton
            {
                if (_instance != null)
                {
                    Destroy(gameObject);
                    return;
                }

                _instance = this;
                DontDestroyOnLoad(gameObject);
            }

            var sceneLogic = new SceneLogic(sceneEntity, transitionView);
        }
    }
}