using System;
using UnityEngine;

namespace Tameru.Application
{
    [Serializable]
    public struct PhaseData
    {
        public int phaseTime;
        public float enemyInstanceInterval;
        public InstanceData[] instanceData;
    }

    [Serializable]
    public struct InstanceData
    {
        public EnemyType type;
        public int instanceRate;
    }
}