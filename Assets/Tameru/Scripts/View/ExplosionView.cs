using UnityEngine;

namespace Tameru.View
{
    public sealed class ExplosionView : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 0.5f);
        }
    }
}