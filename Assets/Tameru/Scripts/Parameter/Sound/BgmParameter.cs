using UnityEngine;

namespace Tameru.Parameter
{
    [CreateAssetMenu(fileName = nameof(BgmParameter), menuName = "DataTable/" + nameof(BgmParameter))]
    public sealed class BgmParameter : ScriptableObject
    {
        [SerializeField] private BgmType bgmType = default;
        [SerializeField] private AudioClip audioClip = default;

        public BgmType type => bgmType;
        public AudioClip clip => audioClip;
    }
}