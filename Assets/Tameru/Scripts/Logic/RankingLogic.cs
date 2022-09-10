using Tameru.Entity;
using Tameru.View;
using UnityEngine;

namespace Tameru.Logic
{
    public sealed class RankingLogic
    {
        private readonly SceneEntity _sceneEntity;
        private readonly SoundEntity _soundEntity;

        public RankingLogic(SceneEntity sceneEntity, SoundEntity soundEntity)
        {
            _sceneEntity = sceneEntity;
            _soundEntity = soundEntity;

            foreach (var button in Object.FindObjectsOfType<BaseButtonView>())
            {
                button.Init(_soundEntity.SetUpPlaySe, _soundEntity.SetUpPlaySe);

                switch (button)
                {
                    case LoadButtonView loadButton:
                        loadButton.Init(_sceneEntity.SetUpLoad);
                        break;
                    case SendScoreButtonView sendScoreButton:
                        sendScoreButton.Init();
                        break;
                    case TweetButtonView tweetButton:
                        tweetButton.Init((int)RankingLoader.Instance.lastScore.value);
                        break;
                }
            }
        }
    }
}