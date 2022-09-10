using Tameru.Entity;
using Tameru.View;
using UniRx;

namespace Tameru.Logic
{
    public sealed class TimeLogic
    {
        private readonly PhaseEntity _phaseEntity;
        private readonly TimeView _timeView;

        public TimeLogic(PhaseEntity phaseEntity, TimeView timeView)
        {
            _phaseEntity = phaseEntity;
            _timeView = timeView;

            _phaseEntity.elapsedTime
                .Subscribe(_timeView.Render)
                .AddTo(_timeView);
        }
    }
}