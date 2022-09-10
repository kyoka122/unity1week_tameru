using UnityEngine;
using UnityEngine.UI;

namespace Tameru.View
{
    public class PlayerChargeView : MonoBehaviour
    {
        [SerializeField] private Image chargeGauge = default;
        private float _maxValue;

        public void InitSliderMaxValue(float newMaxValue)
        {
            _maxValue = newMaxValue;
        }

        public void Render(float value)
        {
            chargeGauge.fillAmount = value / _maxValue;
        }
    }
}