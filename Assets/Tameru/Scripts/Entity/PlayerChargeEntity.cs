using UniRx;
using UnityEngine;

namespace Tameru.Entity
{
    //MEMO: 大きくなるにつれて強い魔法になる
    public enum MagicType
    {
        None,
        SmallBall,
        MediumBall,
        LargeBall,
        SmallBullet,
        MediumBullet,
        LargeBullet,
    }
    public class PlayerChargeEntity
    {
        /// <summary>
        /// 次出す魔法に向けてチャージしている量
        /// </summary>
        public IReadOnlyReactiveProperty<float> chargeGaugeValue =>_chargeGaugeValue;
        
        /// <summary>
        /// 合計チャージ量
        /// </summary>
        public IReadOnlyReactiveProperty<float> chargingValue =>_chargingValue;
        public IReadOnlyReactiveProperty<MagicType> currentMagic =>_currentMagic;

        private const float DefaultChargeValue = 0.1f;
        private readonly ReactiveProperty<float> _chargeGaugeValue;
        private readonly ReactiveProperty<float> _chargingValue;
        private readonly ReactiveProperty<MagicType> _currentMagic;

        private float _maxChargeValue = 0;
        public PlayerChargeEntity()
        {
            _chargeGaugeValue = new ReactiveProperty<float>(0);
            _chargingValue = new ReactiveProperty<float>(0);
            _currentMagic = new ReactiveProperty<MagicType>();
        }
        
        public void SetCurrentMagic(MagicType newMagicType)
        {
            _currentMagic.Value = newMagicType;
        }
        
        public void SetChargeGaugeValue(float currentMagicChargeValue)
        {
            _chargeGaugeValue.Value = _chargingValue.Value - currentMagicChargeValue;
        }

        public void SetMaxChargeValue(float maxValue)
        {
            _maxChargeValue = maxValue;
        }

        public void AddDefault()
        {
            Add(DefaultChargeValue);
        }
        
        public void Add(float addValue)
        {
            _chargingValue.Value = Mathf.Min(_chargingValue.Value + addValue,_maxChargeValue);
        }

        public void ConsumeAll()
        {
            Consume(chargingValue.Value);
        }
        
        public void Consume(float consumeValue)
        {
            _chargingValue.Value = Mathf.Max(_chargingValue.Value - consumeValue,0);
        }

    }
}