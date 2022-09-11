using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Tameru.View
{
    public abstract class BaseMagicBallView : BaseMagicView
    {
        [SerializeField] private float lifeTime = 30;
        public override void Shot(Vector2 direction)
        {
            base.Shot(direction);

            this.OnTriggerEnter2DAsObservable()
                .Subscribe(x =>
                {
                    if (x.TryGetComponent<BaseEnemyView>(out var enemy))
                    {
                        Destroy(gameObject,lifeTime);
                    }
                })
                .AddTo(this);
        }
    }
}