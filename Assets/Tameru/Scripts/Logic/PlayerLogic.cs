using Tameru.Entity;
using Tameru.Struct;
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
        private PlayerParameter _playerParameter;

        public PlayerLogic(ChargeEntity chargeEntity,PlayerEntity playerEntity,PlayerView playerView,PlayerParameter playerParameter)
        {
            _chargeEntity = chargeEntity;
            _playerEntity = playerEntity;
            _playerView = playerView;
            _playerParameter = playerParameter;

            _playerEntity.ChangeSpeedRate(MoveMode.Walk);
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

            _playerParameter
                .ObserveEveryValueChanged(x => x.walkSpeed)
                .Subscribe(_playerEntity.SetWalkSpeedParameter);
            
            _playerParameter
                .ObserveEveryValueChanged(x => x.slowWalkSpeed)
                .Subscribe(_playerEntity.SetSlowWalkSpeedParameter);
        }
        
        public void Move()
        {
            float verticalInputValue= Input.GetAxis("Vertical");
            float horizontalInputValue= Input.GetAxis("Horizontal");
            
            _playerEntity.ChangeSpeed(verticalInputValue,horizontalInputValue);
        }

        public void Charge()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                _chargeEntity.AddDefault();
                _playerEntity.ChangeSpeedRate(MoveMode.SlowWalk);
                return;
            }
            _playerEntity.ChangeSpeedRate(MoveMode.Walk);
        }
    }
}