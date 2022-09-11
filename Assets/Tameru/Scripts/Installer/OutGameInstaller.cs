using Tameru.Logic;
using Tameru.View;
using UnityEngine;

namespace Tameru.Installer
{
    public sealed class OutGameInstaller : MonoBehaviour
    {
        [SerializeField] private GraphicFlashView flashView = default;
        [SerializeField] private VolumeView volumeView = default;

        private void Start()
        {
            var sceneEntity = CommonInstaller.Instance.sceneEntity;
            var soundEntity = CommonInstaller.Instance.soundEntity;

            var topLogic = new TopLogic(sceneEntity, soundEntity, flashView, volumeView);
        }
    }
}