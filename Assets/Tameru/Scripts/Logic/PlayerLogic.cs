using Tameru.Entity;
using Tameru.Parameter;
using Tameru.View;
using UniRx;
using UnityEngine;

namespace Tameru.Logic
{
    public class PlayerLogic
    {
        private readonly ChargeEntity _chargeEntity;
        private readonly PlayerEntity _playerEntity;
        private readonly PlayerView _playerView;
        private PlayerParameterSerializer _playerParameter;

        public PlayerLogic(ChargeEntity chargeEntity,PlayerEntity playerEntity,PlayerView playerView,PlayerParameterSerializer playerParameterSerializer)
        {
            _chargeEntity = chargeEntity;
            _playerEntity = playerEntity;
            _playerView = playerView;
            _playerParameter = playerParameterSerializer;
            
            RegisterReactiveProperty();
        }
        
        private void RegisterReactiveProperty()
        {
            _playerEntity.currentMoveSpeed
                .Subscribe( _playerView.Move)
                .AddTo(_playerView);
            
            _playerEntity.currentMoveAnimationSpeed
                .Subscribe(_playerView.AnimateMove)
                .AddTo(_playerView);

            _playerParameter.PlayerParameter
                .Subscribe(_playerEntity.ChangeWalkSpeedParameter);
        }
        
        public void Move()
        {
            float verticalInputValue= Input.GetAxis("Vertical");
            float horizontalInputValue= Input.GetAxis("Horizontal");
            
            _playerEntity.ChangeSpeed(verticalInputValue,horizontalInputValue);
        }

        public void Charge()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _chargeEntity.AddDefault();
            }
        }
    }
}