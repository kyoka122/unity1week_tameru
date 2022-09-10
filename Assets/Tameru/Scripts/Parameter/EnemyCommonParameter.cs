using System.Collections;
using System.Linq;
using UnityEngine;

namespace Tameru.Parameter
{
    [CreateAssetMenu(fileName = "CommonEnemyParameter", menuName = "ScriptableObjects/CommonEnemyParameter", order = 4)]
    public class EnemyCommonParameter:ScriptableObject
    {
         public float DamageInterval => damageInterval;
         
         //MEMO: カメラ内に収まっているかを判定するためにVector3
         public Vector3[] SpawnerPos => enemySpawnerTransform
             .Select(transform => transform.position)
             .ToArray();
         
         [SerializeField] private float damageInterval;
         [SerializeField] private Transform[] enemySpawnerTransform;
         
    }
}