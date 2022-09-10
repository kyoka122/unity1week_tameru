using Tameru.Entity;
using UnityEngine;

namespace Tameru.View
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class BaseMagicView : MonoBehaviour
    {
        private float speed = 5.0f;

        public abstract MagicType magicType { get; }

        public virtual void Shot(Vector2 direction)
        {
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }
}