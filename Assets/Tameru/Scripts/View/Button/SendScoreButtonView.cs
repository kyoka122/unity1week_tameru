namespace Tameru.View
{
    public sealed class SendScoreButtonView : BaseButtonView
    {
        public void Init()
        {
            var rankingSceneManager = FindObjectOfType<RankingSceneManager>();
            push += () => rankingSceneManager.SendScore();
        }
    }
}