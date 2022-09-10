using UniRx;
using UnityEngine;

namespace Tameru.Entity
{
    public class PhaseEntity
    {
        public ReactiveProperty<int> phaseProperty => _phase;
        public int phase =>_phase.Value;

        public float currentPhasePassedTime { get; private set; } = 0;
        
        private ReactiveProperty<int> _phase;
        
        public PhaseEntity()
        {
            _phase = new ReactiveProperty<int>(1);
        }
        
        public void UpdatePhase()
        {
            _phase.Value++;
            Debug.Log($"Phase:{_phase.Value}");
        }
        
        public void AddPassedTime(float time)
        {
            currentPhasePassedTime += time;
        }

        public void ResetTime()
        {
            currentPhasePassedTime = 0;
        }
    }
}