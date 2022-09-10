using Tameru.Application;
using UnityEngine;

namespace Tameru.Parameter
{
    [CreateAssetMenu(fileName = "PhaseParameter", menuName = "ScriptableObjects/PhaseParameter", order = 5)]
    public class PhaseParameter:ScriptableObject
    {
        public PhaseData[] PhaseData => phaseData;
        
        [SerializeField] private PhaseData[] phaseData;

        public int GetIndex(int phase)
        {
            return phase - 1;
        }

        public int GetPhaseTime(int phase)
        {
            return phaseData[GetIndex(phase)].phaseTime;
        }

        public InstanceData[] GetInstance(int phase)
        {
            return phaseData[GetIndex(phase)].instanceData;
        }

        public float GetEnemyInstanceInterval(int phase)
        {
            return phaseData[GetIndex(phase)].enemyInstanceInterval;
        }

        public bool IsPlayingPhase(int phase)
        {
            return 0 < phase && phase <= PhaseData.Length;
        }
    }
}