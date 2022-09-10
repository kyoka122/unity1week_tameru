using System;
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
        public EnemyData[] data;
        
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