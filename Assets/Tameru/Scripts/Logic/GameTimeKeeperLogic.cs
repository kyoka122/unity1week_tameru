using Tameru.Entity;
using Tameru.Parameter;
using UniRx;
using UnityEngine;

namespace Tameru.Logic
{
    public class GameTimeKeeperLogic
    {
        private readonly PhaseEntity _phaseEntity;
        private readonly PhaseParameter _phaseParameter;

        public GameTimeKeeperLogic(PhaseEntity phaseEntity,PhaseParameter phaseParameter)
        {
            _phaseEntity = phaseEntity;
            _phaseParameter = phaseParameter;
        }

        public void UpdateGameTime()
        {
            if (!_phaseParameter.IsPlayingPhase(_phaseEntity.phase))
            {
                return;
            }
            _phaseEntity.AddPassedTime(Time.deltaTime);
            if (_phaseEntity.currentPhasePassedTime>=_phaseParameter.GetPhaseTime(_phaseEntity.phase))
            {
                _phaseEntity.UpdatePhase();
                _phaseEntity.ResetTime();
            }
        }

    }
}