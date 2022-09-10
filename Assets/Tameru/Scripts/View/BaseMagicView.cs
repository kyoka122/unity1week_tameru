using Tameru.Entity;
using UnityEngine;

namespace Tameru.View
{
    public abstract class BaseMagicView:MonoBehaviour
    {
        public abstract MagicType magicType { get; }
    }
}