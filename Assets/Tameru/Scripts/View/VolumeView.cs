using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Tameru.View
{
    public sealed class VolumeView : MonoBehaviour
    {
        [SerializeField] private Slider bgmVolumeSlider = default;
        [SerializeField] private Slider seVolumeSlider = default;

        public void Init(float bgmVolume, float seVolume)
        {
            bgmVolumeSlider.value = bgmVolume;
            seVolumeSlider.value = seVolume;
        }

        public IObservable<float> UpdateBgmVolume()
        {
            return bgmVolumeSlider
                .OnValueChangedAsObservable();
        }

        public IObservable<float> UpdateSeVolume()
        {
            return seVolumeSlider
                .OnValueChangedAsObservable();
        }

        public IObservable<SeType> OnPointerUpBgmSlider()
        {
            return bgmVolumeSlider
                .OnPointerUpAsObservable()
                .Select(_ => SeType.Decision);
        }

        public IObservable<SeType> OnPointerUpSeSlider()
        {
            return seVolumeSlider
                .OnPointerUpAsObservable()
                .Select(_ => SeType.Decision);
        }
    }
}