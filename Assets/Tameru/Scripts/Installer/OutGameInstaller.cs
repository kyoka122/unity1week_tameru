using Tameru.Logic;
using Tameru.View;
using UnityEngine;

namespace Tameru.Installer
{
    public sealed class OutGameInstaller : MonoBehaviour
    {
        [SerializeField] private VolumeView volumeView = default;

        private void Start()
        {
            var sceneEntity = CommonInstaller.Instance.sceneEntity;
            var soundEntity = CommonInstaller.Instance.soundEntity;

            var topLogic = new TopLogic(sceneEntity, soundEntity, volumeView);

            soundEntity.SetUpPlayBgm(BgmType.Top);
        }
    }
}