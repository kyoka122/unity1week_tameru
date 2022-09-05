using UnityEngine;
using UnityEngine.UI;

namespace Tameru.View
{
    public class ChargeView:MonoBehaviour
    {
        [SerializeField] private Text chargeText;
        
        public void Render(int value)
        {
            chargeText.text = value.ToString();
        }
    }
}