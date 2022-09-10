using UnityEngine;
using UnityEngine.UI;

namespace Tameru.View
{
    public class PlayerHealthView : MonoBehaviour
    {
        [SerializeField] private Image hpGauge = default;
        private float _maxValue;

        public void SetMaxHp(int newMaxHp)
        {
            _maxValue = newMaxHp;
        }

        public void SetHp(int newHp)
        {
            hpGauge.fillAmount = newHp / _maxValue;
        }
    }
}