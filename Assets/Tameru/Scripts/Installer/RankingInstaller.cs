using Tameru.Logic;
using UnityEngine;

namespace Tameru.Installer
{
    public sealed class RankingInstaller : MonoBehaviour
    {
        private void Start()
        {
            var sceneEntity = CommonInstaller.Instance.sceneEntity;
            var soundEntity = CommonInstaller.Instance.soundEntity;

            var rankingLogic = new RankingLogic(sceneEntity, soundEntity);
        }
    }
}