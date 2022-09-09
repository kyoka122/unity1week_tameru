using UnityEngine;

namespace Tameru.Parameter
{
    [CreateAssetMenu(fileName = nameof(SeParameter), menuName = "DataTable/" + nameof(SeParameter))]
    public sealed class SeParameter : ScriptableObject
    {
        [SerializeField] private SeType seType = default;
        [SerializeField] private AudioClip audioClip = default;

        public SeType type => seType;
        public AudioClip clip => audioClip;
    }
}