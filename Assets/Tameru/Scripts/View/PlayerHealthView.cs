using UnityEngine;
using UnityEngine.UI;

namespace Tameru.View
{
    public class PlayerHealthView:MonoBehaviour
    {
        [SerializeField] private Slider hp;

        public void SetMaxHp(int newMaxHp)
        {
            hp.maxValue = newMaxHp;
        }

        public void SetHp(int newHp)
        {
            hp.value = newHp;
        }
    }
}