using System;
using Tameru.Entity;
using Tameru.View;
using UnityEngine;

namespace Tameru.Application
{
    [Serializable]
    public class PhaseData
    {
        public int phaseTime;
        public float enemyInstanceInterval;
        public InstanceData[] instanceData;
    }

    [Serializable]
    public class InstanceData
    {
        public EnemyType type;
        public int instanceRate;
    }

    [Serializable]
    public class MagicData
    {
        public MagicType type;
        public BaseMagicView prefab;
        public string name;
        public int chargeValue;
        public int damage;
    }

    [Serializable]
    public class EnemyData
    {
        public EnemyType type ;
        public BaseEnemyView prefab;
        public int hp;
        public int attack; 
    }
}