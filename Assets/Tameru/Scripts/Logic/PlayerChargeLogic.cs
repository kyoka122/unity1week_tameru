using System.Linq;
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
            _playerChargeView.InitSliderMaxValue(_playerMagicParameter.GetChargeParameter()[(MagicMode)1]);
            
            RegisterReactiveProperty();
        }

        private void RegisterReactiveProperty()
        {
            _playerChargeEntity.chargingValue
                .Subscribe(currentValue =>
                {
                    UpdateMagic(currentValue);
                    UpdateViewByCharge();
                })
                .AddTo(_playerChargeView);
            
            _playerChargeEntity.currentMagic
                .Subscribe(UpdateViewByMagicChange)
                .AddTo(_playerChargeView);


            _playerChargeEntity.chargeGaugeValue
                .Subscribe(_playerChargeView.Render)
                .AddTo(_playerChargeView);

            this.ObserveEveryValueChanged(_=>_playerMagicParameter.MagicChargeValue)
                .Subscribe(_ => ResetEntityParameter())
                .AddTo(_playerChargeView);

            /*_playerMagicParameter.WeakMagicNeedChargeValue
                .Subscribe(_ =>ResetEntityParameter())
                .AddTo(_playerChargeView);

            _playerMagicParameter.StrongMagicNeedChargeValue
                .Subscribe(_ =>ResetEntityParameter())
                .AddTo(_playerChargeView);*/

        }
        
        public void UpdateCharge()
        {
            if (InputKeyData.IsCharging)
            {
                _playerChargeEntity.AddDefault();
            }
        }

        private void ResetEntityParameter()
        {
            _playerChargeEntity.SetMaxChargeValue(GetChargeValue((MagicMode) EnumHelper.MaxIndex<MagicMode>()));
            MagicMode nextMagicMode = _playerChargeEntity.currentMagic.Value + 1;
            _playerChargeView.InitSliderMaxValue(_playerMagicParameter.GetChargeParameter()[nextMagicMode]);
        }

        private void UpdateMagic(float currentValue)
        {
            var nextMagic=GetUpdatedMagic(currentValue);
            _playerChargeEntity.SetCurrentMagic(nextMagic);
        }
        
        private MagicMode GetUpdatedMagic(float currentValue)
        {
            MagicMode newMagic = 0;
            foreach (var value in _playerMagicParameter.GetChargeParameter())
            {
                if (currentValue < GetChargeValue(value.Key))
                {
                    break;
                }
                newMagic = value.Key;
            }

            return newMagic;
        }

        private float GetChargeValue(MagicMode magicMode)
        {
            return _playerMagicParameter.GetChargeParameter()
                .Where(x=>x.Key<=magicMode)
                .Sum(x=>x.Value);
        }

        private void UpdateViewByCharge()
        {
            //MEMO: 最大魔法のチャージ量がMaxになった時、スライダーを満タンの状態で維持するために-1
            var currentMagic =
                (MagicMode) Mathf.Min((int) _playerChargeEntity.currentMagic.Value, EnumHelper.MaxIndex<MagicMode>() - 1);
            _playerChargeEntity.SetChargeGaugeValue(GetChargeValue(currentMagic));
        }
        
        private void UpdateViewByMagicChange(MagicMode magic)
        {
            var nextMagicMode = (MagicMode) Mathf.Min((int) magic + 1, EnumHelper.MaxIndex<MagicMode>());
            _playerChargeView.InitSliderMaxValue(_playerMagicParameter.GetChargeParameter()[nextMagicMode]);
            
            _playerMagicView.UpdateUseAbleMagicText(_playerMagicParameter.MagicName[(int)_playerChargeEntity.currentMagic.Value]);
        }

    }
}