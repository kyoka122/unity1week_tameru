using System;
using UniRx;

namespace Tameru.Entity
{
    public sealed class GameStateEntity
    {
        private readonly ReactiveProperty<GameState> _gameState;

        public GameStateEntity()
        {
            _gameState = new ReactiveProperty<GameState>(GameState.Ready);
        }

        public IObservable<GameState> gameState => _gameState.Where(x => x != GameState.None);

        public void Set(GameState state)
        {
            _gameState.Value = state;
        }

        public bool IsState(GameState state)
        {
            return _gameState.Value == state;
        }
    }
}