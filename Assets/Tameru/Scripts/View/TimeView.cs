using TMPro;
using UnityEngine;

namespace Tameru.View
{
    public sealed class TimeView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timeText = default;

        public void Render(float value)
        {
            var time = Mathf.FloorToInt(value);
            var min = time / 60;
            var sec = time % 60;
            timeText.text = $"{min:00}:{sec:00}";
        }
    }
}