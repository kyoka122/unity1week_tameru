using System;
using System.Collections.Generic;
using System.Linq;
using Tameru.Application;
using Tameru.View;
using UniRx;
using UnityEngine;

namespace Tameru.Parameter
{
    [CreateAssetMenu(fileName = "EnemyParameter", menuName = "ScriptableObjects/EnemyParameter", order = 3)]
    public class EnemyParameter:ScriptableObject
    {
        public List<EnemyData> data;

        public EnemyData Find(EnemyType type)
        {
            var enemy = data.Find(x => x.type == type);
            if (enemy == null)
            {
                throw new Exception($"enemy data is nothing: {type}");
            }

            return enemy;
        }
        
        public BaseEnemyView FindPrefab(EnemyType type)
        {
            BaseEnemyView result = data.FirstOrDefault(enemy => enemy.type == type)?.prefab;
            if (result==null)
            {
                Debug.LogError("Not Found Data");
            }
            return result;
        }
        
        public int FindHp(EnemyType type)
        {
            int? result = data.FirstOrDefault(enemy => enemy.type == type)?.hp;
            if (result==null)
            {
                Debug.LogError("Not Found Data");
                return -1;
            }
            return result.Value;
        }
        
        public int FindAttack(EnemyType type)
        {
            int? result = data.FirstOrDefault(enemy => enemy.type == type)?.attack;
            if (result==null)
            {
                Debug.LogError("Not Found Data");
                return -1;
            }
            return result.Value;
        }
        
        public int FindSpeed(EnemyType type)
        {
            int? result = data.FirstOrDefault(enemy => enemy.type == type)?.speed;
            if (result==null)
            {
                Debug.LogError("Not Found Data");
                return -1;
            }
            return result.Value;
        }
        
    }
}