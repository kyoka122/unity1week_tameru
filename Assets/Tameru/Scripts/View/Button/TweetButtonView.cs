namespace Tameru.View
{
    public sealed class TweetButtonView : BaseButtonView
    {
        public void Init(int value)
        {
            var message = $"スコア: {value.ToString()}\n";
            message += $"#{ProjectConfig.GAME_ID} #unity1week\n";

            push += () => UnityRoomTweet.Tweet(ProjectConfig.GAME_ID, message);
        }
    }
}