using UnityEngine;

namespace Tameru.View
{
    public class EnemySpawnView:MonoBehaviour
    {
        public BaseEnemyView InstanceEnemy(BaseEnemyView enemy,Vector2 instancePositions)
        {
            BaseEnemyView enemyView=Instantiate(enemy, instancePositions, Quaternion.identity);
            Debug.Log($"Instantiate!");
            return enemyView;
        }
    }
}