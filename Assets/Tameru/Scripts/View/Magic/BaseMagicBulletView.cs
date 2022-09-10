using EFUK;
using UnityEngine;

namespace Tameru.View
{
    public abstract class BaseMagicBulletView : BaseMagicView
    {
        [SerializeField] private float lifeTime = default;

        public override void Shot(Vector2 direction)
        {
            base.Shot(direction);
            this.Delay(lifeTime, () => Destroy(gameObject));
        }
    }
}