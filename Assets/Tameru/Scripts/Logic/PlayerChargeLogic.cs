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
        private readonly ISoundEntity _soundEntity;
        private readonly PlayerChargeView _playerChargeView;
        private readonly PlayerMagicView _playerMagicView;
        private readonly PlayerMagicParameter _playerMagicParameter;

        public PlayerChargeLogic(PlayerChargeEntity playerChargeEntity, ISoundEntity soundEntity,
            PlayerChargeView playerChargeView, PlayerMagicView playerMagicView,
            PlayerMagicParameter playerMagicParameter)
        {
            _playerChargeEntity = playerChargeEntity;
            _soundEntity = soundEntity;
            _playerChargeView = playerChargeView;
            _playerMagicView = playerMagicView;
            _playerMagicParameter = playerMagicParameter;
            _playerChargeView.InitSliderMaxValue(_playerMagicParameter.FindChargeValue((MagicType)1));
            
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

            this.ObserveEveryValueChanged(_=>_playerMagicParameter.GetAllChargeValue())
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
            _playerChargeEntity.SetMaxChargeValue(GetChargeValue((MagicType) EnumHelper.MaxIndex<MagicType>()));
            MagicType nextMagicType = (MagicType) Mathf.Min((int)_playerChargeEntity.currentMagic.Value + 1,EnumHelper.MaxIndex<MagicType>());
            _playerChargeView.InitSliderMaxValue(_playerMagicParameter.FindChargeValue(nextMagicType));
        }

        private void UpdateMagic(float currentValue)
        {
            var nextMagic=GetUpdatedMagic(currentValue);
            _playerChargeEntity.SetCurrentMagic(nextMagic);
        }
        
        private MagicType GetUpdatedMagic(float currentValue)
        {
            MagicType newMagic = 0;
            foreach (var value in _playerMagicParameter.GetAllChargeValue())
            {
                if (currentValue < GetChargeValue(value.Key))
                {
                    break;
                }
                newMagic = value.Key;
            }

            return newMagic;
        }

        private float GetChargeValue(MagicType magicType)
        {
            return _playerMagicParameter.GetAllChargeValue()
                .Where(x=>x.Key<=magicType)
                .Sum(x=>x.Value);
        }

        private void UpdateViewByCharge()
        {
            //MEMO: 最大魔法のチャージ量がMaxになった時、スライダーを満タンの状態で維持するために-1
            var currentMagic =
                (MagicType) Mathf.Min((int) _playerChargeEntity.currentMagic.Value, EnumHelper.MaxIndex<MagicType>() - 1);
            _playerChargeEntity.SetChargeGaugeValue(GetChargeValue(currentMagic));
        }
        
        private void UpdateViewByMagicChange(MagicType magic)
        {
            _soundEntity.SetUpPlaySe(SeType.Charged);

            var nextMagicMode = (MagicType) Mathf.Min((int) magic + 1, EnumHelper.MaxIndex<MagicType>());
            _playerChargeView.InitSliderMaxValue(_playerMagicParameter.FindChargeValue(nextMagicMode));
            
            _playerMagicView.UpdateUseAbleMagicText(_playerMagicParameter.FindName(_playerChargeEntity.currentMagic.Value));
        }

    }
}