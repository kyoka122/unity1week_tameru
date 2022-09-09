using System;
using UniRx;

namespace Tameru.Entity
{
    public sealed class SoundEntity
    {
        private readonly Subject<BgmType> _playBgm;
        private readonly Subject<SeType> _playSe;

        public SoundEntity()
        {
            _playBgm = new Subject<BgmType>();
            _playSe = new Subject<SeType>();
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
    }
}