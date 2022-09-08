using System;
using Tameru.Application;
using Tameru.Entity;
using Tameru.Parameter;
using Tameru.View;
using UniRx;
using UnityEngine;

namespace Tameru.Logic
{
    public class PlayerChargeLogic
    {
        private readonly PlayerChargeEntity _playerChargeEntity;
        private readonly PlayerChargeView _playerChargeView;
        private readonly PlayerMagicView _playerMagicView;
        private readonly PlayerMagicParameter _playerMagicParameter;

        public PlayerChargeLogic(PlayerChargeEntity playerChargeEntity, PlayerChargeView playerChargeView,PlayerMagicView playerMagicView,PlayerMagicParameter playerMagicParameter)
        {
            _playerChargeEntity = playerChargeEntity;
            _playerChargeView = playerChargeView;
            _playerMagicView = playerMagicView;
            _playerMagicParameter = playerMagicParameter;
            _playerChargeView.InitSliderParam(_playerChargeEntity.GetNeedValue((MagicMode)1));
            
            RegisterReactiveProperty();
        }

        private void RegisterReactiveProperty()
        {
            _playerChargeEntity.currentValue.Subscribe(currentValue=>
            {
                TryUpdateMagic(currentValue);
                _playerChargeEntity.UpdateCurrentChargeValue();
            });
            _playerChargeEntity.currentMagic
                .Subscribe(magic =>
                {
                    var magicModeMax=EnumHelper.MaxIndex<MagicMode>();
                    var nextMagicMode = (MagicMode) Mathf.Min((int) magic + 1, magicModeMax);
                    _playerChargeView.InitSliderParam(_playerChargeEntity.needValue[nextMagicMode]);
                    _playerMagicView.UpdateUseAbleMagicText(_playerMagicParameter.MagicName[(int)_playerChargeEntity.currentMagic.Value]);
                });


            _playerChargeEntity.currentMagicChargingValue
                .Subscribe(_playerChargeView.Render)
                .AddTo(_playerChargeView);
        }
        
        public void UpdateCharge()
        {
            if (InputKeyData.IsCharging)
            {
                _playerChargeEntity.AddDefault();
            }
        }
        
        private void TryUpdateMagic(float currentValue)
        {
            var nextMagic=GetUpdatedMagic(currentValue);
            _playerChargeEntity.UpdateCurrentMagic(nextMagic);
        }

        private MagicMode GetUpdatedMagic(float currentValue)
        {
            float needValue = 0;
            MagicMode newMagic = 0;
            foreach (var value in _playerChargeEntity.needValue)
            {
                needValue = _playerChargeEntity.GetNeedValue(value.Key);
                if (currentValue < needValue)
                {
                    break;
                }
                newMagic = value.Key;
            }

            return newMagic;
        }


    }
}