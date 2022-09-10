using UniRx;
using UnityEngine;

namespace Tameru.Entity
{
    public class PhaseEntity
    {
        public ReactiveProperty<int> phaseProperty => _phase;
        public int phase => _phase.Value;
        public IReadOnlyReactiveProperty<float> elapsedTime => _elapsedTime;

        public float currentPhasePassedTime { get; private set; } = 0;

        private ReactiveProperty<int> _phase;
        private readonly ReactiveProperty<float> _elapsedTime;

        public PhaseEntity()
        {
            _phase = new ReactiveProperty<int>(1);
            _elapsedTime = new ReactiveProperty<float>(0.0f);
        }

        public void UpdatePhase()
        {
            _phase.Value++;
            Debug.Log($"Phase:{_phase.Value}");
        }

        public void AddPassedTime(float time)
        {
            currentPhasePassedTime += time;
            _elapsedTime.Value += time;
        }

        public void ResetTime()
        {
            currentPhasePassedTime = 0;
        }
    }
}