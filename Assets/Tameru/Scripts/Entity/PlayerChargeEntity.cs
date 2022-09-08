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
        /// 現在溜めている魔法のチャージ量
        /// </summary>
        public IReadOnlyReactiveProperty<float> currentMagicChargingValue =>_currentMagicChargingValue;
        
        /// <summary>
        /// 合計チャージ量
        /// </summary>
        public IReadOnlyReactiveProperty<float> currentValue =>_currentValue;
        public IReadOnlyReactiveProperty<MagicMode> currentMagic =>_currentMagic;

        private const float DefaultChargeValue = 0.1f;
        public Dictionary<MagicMode, int> needValue { get; private set; }
        
        private readonly ReactiveProperty<float> _currentMagicChargingValue;
        private readonly ReactiveProperty<float> _currentValue;
        private readonly ReactiveProperty<MagicMode> _currentMagic;
        
        public PlayerChargeEntity(Dictionary<MagicMode, int> newNeedValue)
        {
            _currentMagicChargingValue = new ReactiveProperty<float>(0);
            _currentValue = new ReactiveProperty<float>(0);
            _currentMagic = new ReactiveProperty<MagicMode>();
            needValue = newNeedValue;
        }
        
        public void UpdateNeedChargeValue(MagicMode mode,int newValue)
        {
            needValue[mode] = newValue;
        }

        public void UpdateCurrentMagic(MagicMode newMagicMode)
        {
            _currentMagic.Value = newMagicMode;
        }
        
        public void UpdateCurrentChargeValue()
        {
            //MEMO: 最大魔法のチャージ量がMaxになった時、スライダーを満タンの状態で維持するために-1
            var chargingValue = Mathf.Min((int) _currentMagic.Value, EnumHelper.MaxIndex<MagicMode>()-1);
            _currentMagicChargingValue.Value = _currentValue.Value - GetNeedValue((MagicMode) chargingValue);
        }

        public float GetNeedValue(MagicMode magicMode)
        {
            return needValue
                .Where(x=>x.Key<=magicMode)
                .Sum(x=>x.Value);
        }
        
        public void AddDefault()
        {
            Add(DefaultChargeValue);
        }
        
        public void Add(float addValue)
        {
            var maxValue=GetNeedValue((MagicMode) EnumHelper.MaxIndex<MagicMode>());
            _currentValue.Value = Mathf.Min(_currentValue.Value + addValue,maxValue);
        }

        public void ConsumeAll()
        {
            Consume(currentValue.Value);
        }
        
        public void Consume(float consumeValue)
        {
            _currentValue.Value = Mathf.Max(_currentValue.Value - consumeValue,0);
        }

    }
}