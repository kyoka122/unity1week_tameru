using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tameru.View
{
    public class PlayerMagicView : MonoBehaviour
    {
        [SerializeField] private Image magicIcon = default;
        [SerializeField] private TextMeshProUGUI useAbleMagicNameText;

        public void UpdateUseAbleMagicText(string magicName)
        {
            useAbleMagicNameText.text = magicName;
        }

        public void UpdateMagicIcon(Sprite icon)
        {
            magicIcon.sprite = icon;
        }
    }
}