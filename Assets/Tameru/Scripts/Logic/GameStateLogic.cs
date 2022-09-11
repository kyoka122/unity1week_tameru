using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tameru.Entity;
using Tameru.View;
using UniRx;

namespace Tameru.Logic
{
    public sealed class GameStateLogic
    {
        private readonly GameStateEntity _stateEntity;
        private readonly ScoreEntity _scoreEntity;
        private readonly GameStateView _stateView;
        private readonly CancellationTokenSource _tokenSource;

        public GameStateLogic(GameStateEntity stateEntity, ScoreEntity scoreEntity, GameStateView stateView)
        {
            _stateEntity = stateEntity;
            _scoreEntity = scoreEntity;
            _stateView = stateView;
            _tokenSource = new CancellationTokenSource();
            _tokenSource
                .AddTo(_stateView);

            _stateEntity.gameState
                .Subscribe(x =>
                {
                    switch (x)
                    {
                        case GameState.Ready:
                            _stateView.ShowReadyAsync(_tokenSource.Token).Forget();
                            break;
                        case GameState.Main:
                            break;
                        case GameState.Over:
                        case GameState.Clear:
                            ShowResultAsync(x, _tokenSource.Token).Forget();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(x), x, null);
                    }
                })
                .AddTo(stateView);
        }

        private async UniTaskVoid ShowResultAsync(GameState state, CancellationToken token)
        {
            if (state == GameState.Clear)
            {
                await _stateView.ShowClearAsync(token);
            }
            else if (state == GameState.Over)
            {
                await _stateView.ShowOverAsync(token);
            }

            RankingLoader.Instance.SendScoreAndShowRanking(_scoreEntity.currentValue);
        }
    }
}