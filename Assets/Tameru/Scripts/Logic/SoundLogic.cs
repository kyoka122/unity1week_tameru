using Tameru.Entity;
using Tameru.Parameter;
using Tameru.View;
using UniRx;

namespace Tameru.Logic
{
    public sealed class SoundLogic
    {
        private readonly SoundParameter _soundParameter;
        private readonly SoundEntity _soundEntity;
        private readonly SoundView _soundView;

        public SoundLogic(SoundParameter soundParameter, SoundEntity soundEntity, SoundView soundView)
        {
            _soundParameter = soundParameter;
            _soundEntity = soundEntity;
            _soundView = soundView;

            _soundEntity.PlayBgm()
                .Subscribe(x =>
                {
                    var clip = _soundParameter.Find(x);
                    _soundView.PlayBgm(clip);
                })
                .AddTo(_soundView);

            _soundEntity.PlaySe()
                .Subscribe(x =>
                {
                    var clip = _soundParameter.Find(x);
                    _soundView.PlaySe(clip);
                })
                .AddTo(_soundView);
        }
    }
}