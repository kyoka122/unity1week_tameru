using UnityEngine;

namespace Tameru.Entity
{
    public class PlayerHealthEntity
    {
        public void AddDamage(float damage)
        {
            Debug.Log($"AddDamage!:{damage}");
        }
    }
}