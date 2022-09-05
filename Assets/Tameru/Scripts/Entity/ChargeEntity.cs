using UniRx;

namespace Tameru.Entity
{
    public class ChargeEntity
    {
        private ReactiveProperty<int> _value;

        public ChargeEntity()
        {
            _value = new ReactiveProperty<int>(0);
        }

        public ReactiveProperty<int> value =>_value;

        private const int DefaultChargeValue=1;
        
        public void AddDefault()
        {
            Add(DefaultChargeValue);
        }
        public void Add(int addValue)
        {
            _value.Value += addValue;
        }

        public void Consume()
        {
            
        }
    }
}