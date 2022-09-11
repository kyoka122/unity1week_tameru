using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Tameru.View
{
    public abstract class BaseMagicBallView : BaseMagicView
    {
        public override void Shot(Vector2 direction)
        {
            base.Shot(direction);

            this.OnTriggerEnter2DAsObservable()
                .Subscribe(x =>
                {
                    if (x.TryGetComponent<BaseEnemyView>(out var enemy))
                    {
                        Destroy(gameObject);
                    }
                })
                .AddTo(this);

            Destroy(gameObject, 30.0f);
        }
    }
}