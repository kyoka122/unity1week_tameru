using UniRx;

namespace Tameru.Entity
{
    public sealed class ScoreEntity
    {
        private readonly ReactiveProperty<int> _score;

        public ScoreEntity()
        {
            _score = new ReactiveProperty<int>(0);
        }

        public IReadOnlyReactiveProperty<int> Update()
        {
            return _score;
        }

        public void Set(int value)
        {
            _score.Value = value;
        }

        public void Add(int addValue)
        {
            Set(_score.Value + addValue);
        }

        public int currentValue => _score.Value;
    }
}