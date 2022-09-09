using System;
using UniRx;

namespace Tameru.Entity
{
    public sealed class SoundEntity : ISoundEntity
    {
        private readonly Subject<BgmType> _playBgm;
        private readonly Subject<SeType> _playSe;
        private readonly ReactiveProperty<float> _bgmVolume;
        private readonly ReactiveProperty<float> _seVolume;

        public SoundEntity()
        {
            _playBgm = new Subject<BgmType>();
            _playSe = new Subject<SeType>();
            _bgmVolume = new ReactiveProperty<float>(0.5f);
            _seVolume = new ReactiveProperty<float>(0.5f);
        }

        public void SetUpPlayBgm(BgmType type)
        {
            _playBgm?.OnNext(type);
        }

        public void SetUpPlaySe(SeType type)
        {
            _playSe?.OnNext(type);
        }

        public IObservable<BgmType> PlayBgm()
        {
            return _playBgm
                .Where(x => x != BgmType.None);
        }

        public IObservable<SeType> PlaySe()
        {
            return _playSe
                .Where(x => x != SeType.None);
        }

        public float bgmVolume => _bgmVolume.Value;

        public float seVolume => _seVolume.Value;

        public void SetBgmVolume(float value)
        {
            _bgmVolume.Value = value;
        }

        public void SetSeVolume(float value)
        {
            _seVolume.Value = value;
        }

        public IReadOnlyReactiveProperty<float> UpdateBgmVolume()
        {
            return _bgmVolume;
        }

        public IReadOnlyReactiveProperty<float> UpdateSeVolume()
        {
            return _seVolume;
        }
    }
}