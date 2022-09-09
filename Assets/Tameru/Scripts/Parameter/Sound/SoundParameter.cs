using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tameru.Parameter
{
    [CreateAssetMenu(fileName = nameof(SoundParameter), menuName = "DataTable/" + nameof(SoundParameter))]
    public sealed class SoundParameter : ScriptableObject
    {
        [SerializeField] private List<BgmParameter> bgmDataList = default;
        [SerializeField] private List<SeParameter> seDataList = default;

        public AudioClip Find(BgmType type)
        {
            var data = bgmDataList.Find(x => x.type == type);
            if (data == null || data.clip == null)
            {
                throw new Exception($"bgm data is nothing: {type}");
            }

            return data.clip;
        }

        public AudioClip Find(SeType type)
        {
            var data = seDataList.Find(x => x.type == type);
            if (data == null || data.clip == null)
            {
                throw new Exception($"se data is nothing: {type}");
            }

            return data.clip;
        }
    }
}