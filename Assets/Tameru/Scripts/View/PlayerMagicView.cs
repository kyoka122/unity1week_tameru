using Tameru.Entity;
using TMPro;
using UnityEngine;

namespace Tameru.View
{
    public class PlayerMagicView:MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI useAbleMagicNameText;
        
        public void UpdateUseAbleMagicText(string magicName)
        {
            useAbleMagicNameText.text = magicName;
        }
        
        public void UseMagic(MagicType magicType)
        {
            Debug.Log($"UseMagic");
        }
    }
}