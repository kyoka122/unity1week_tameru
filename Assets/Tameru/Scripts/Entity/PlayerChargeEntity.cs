using System;
using System.Collections.Generic;
using System.Linq;
using Tameru.Application;
using UniRx;
using UnityEngine;

namespace Tameru.Entity
{
    //MEMO: 大きくなるにつれて強い魔法になる
    public enum MagicMode
    {
        None,
        Weak,
        Strong,
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
        public IReadOnlyReactiveProperty<MagicMode> currentMagic =>_currentMagic;

        private const float DefaultChargeValue = 0.1f;
        private readonly ReactiveProperty<float> _chargeGaugeValue;
        private readonly ReactiveProperty<float> _chargingValue;
        private readonly ReactiveProperty<MagicMode> _currentMagic;

        private float _maxChargeValue = 0;
        public PlayerChargeEntity()
        {
            _chargeGaugeValue = new ReactiveProperty<float>(0);
            _chargingValue = new ReactiveProperty<float>(0);
            _currentMagic = new ReactiveProperty<MagicMode>();
        }
        
        public void SetCurrentMagic(MagicMode newMagicMode)
        {
            _currentMagic.Value = newMagicMode;
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