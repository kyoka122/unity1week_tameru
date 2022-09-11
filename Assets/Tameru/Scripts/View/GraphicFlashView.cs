using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Tameru.View
{
    public sealed class GraphicFlashView : MonoBehaviour
    {
        [SerializeField] private Graphic graphic = default;
        [SerializeField] private float duration = default;

        public void Init()
        {
            graphic
                .DOFade(0.0f, duration)
                .SetEase(Ease.InExpo)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(graphic.gameObject);
        }
    }
}