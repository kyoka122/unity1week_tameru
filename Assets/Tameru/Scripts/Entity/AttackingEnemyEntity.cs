using System.Collections.Generic;
using Tameru.View;
using UnityEngine;

namespace Tameru.Entity
{
    public class AttackingEnemyEntity
    {
        public IReadOnlyDictionary<BaseEnemyView, float> attackingTime=>_attackingTime;
        
        private readonly Dictionary<BaseEnemyView, float> _attackingTime;

        public AttackingEnemyEntity()
        {
            _attackingTime=new Dictionary<BaseEnemyView,float>();
        }

        public void RegisterAttackingEnemy(BaseEnemyView enemyView)
        {
            if (!_attackingTime.ContainsKey(enemyView))
            {
                _attackingTime.Add(enemyView,0);
            }
        }

        public void UnRegisterAttackingEnemy(BaseEnemyView enemyView)
        {
            if (_attackingTime.ContainsKey(enemyView))
            {
                _attackingTime.Remove(enemyView);
            }
        }

        public void AddTime(BaseEnemyView enemyView,float time)
        {
            _attackingTime[enemyView] += time;
        }

        public void ResetTime(BaseEnemyView enemyView)
        {
            _attackingTime[enemyView] = 0;
        }

    }
}