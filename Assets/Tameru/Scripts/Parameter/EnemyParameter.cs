using Tameru.View;
using UniRx;
using UnityEngine;

namespace Tameru.Parameter
{
    [CreateAssetMenu(fileName = "EnemyParameter", menuName = "ScriptableObjects/EnemyParameter", order = 3)]
    public class EnemyParameter:ScriptableObject
    {
        public EnemyType EnemyType => enemyType;
        public BaseEnemyView Prefab => prefab;
        public float Hp => hp;
        public float AttackimgValue=>attackValue; 
        
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private BaseEnemyView prefab;
        [SerializeField] private int hp;
        [SerializeField] private int attackValue;
        
        

    }
}