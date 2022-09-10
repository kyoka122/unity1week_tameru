using Tameru.Entity;
using Tameru.View;
using UniRx;

namespace Tameru.Logic
{
    public sealed class ScoreLogic
    {
        private readonly ScoreEntity _scoreEntity;
        private readonly ScoreView _scoreView;

        public ScoreLogic(ScoreEntity scoreEntity, ScoreView scoreView)
        {
            _scoreEntity = scoreEntity;
            _scoreView = scoreView;

            _scoreEntity.Update()
                .Subscribe(_scoreView.Render)
                .AddTo(_scoreView);
        }
    }
}