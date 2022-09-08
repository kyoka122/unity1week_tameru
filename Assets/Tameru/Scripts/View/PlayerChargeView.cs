using UnityEngine;
using UnityEngine.UI;

namespace Tameru.View
{
    public class PlayerChargeView:MonoBehaviour
    {
        [SerializeField] private Text chargeText;
        [SerializeField] private Slider chargeValueSlider;

        public void InitSliderParam(float newMaxValue)
        {
            chargeValueSlider.maxValue = newMaxValue;
        }

        public void Render(float value)
        {
            chargeValueSlider.value = value;
            chargeText.text = value.ToString();
        }
    }
}