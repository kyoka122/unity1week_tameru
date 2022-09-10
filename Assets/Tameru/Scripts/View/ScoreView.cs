using TMPro;
using UnityEngine;

namespace Tameru.View
{
    public sealed class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText = default;

        public void Render(int value)
        {
            scoreText.text = $"{value.ToString()}";
        }
    }
}