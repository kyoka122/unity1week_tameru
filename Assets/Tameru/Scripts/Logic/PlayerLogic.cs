using System;
using Tameru.Application;
using Tameru.Entity;
using Tameru.Struct;
using Tameru.View;
using UniRx;
using UnityEngine;

namespace Tameru.Logic
{
    public class PlayerLogic
    {
        private readonly PlayerMoveEntity _playerMoveEntity;
        private readonly PlayerView _playerView;
        private readonly PlayerParameter _playerParameter;

        public PlayerLogic(PlayerMoveEntity playerMoveEntity,PlayerView playerView,PlayerParameter playerParameter)
        {
            _playerMoveEntity = playerMoveEntity;
            _playerView = playerView;
            _playerParameter = playerParameter;

            _playerMoveEntity.SetMoveMode(MoveMode.Walk);
            RegisterReactiveProperty();
        }
        
        private void RegisterReactiveProperty()
        {
            _playerMoveEntity.currentMoveSpeed
                .Subscribe( _playerView.Move)
                .AddTo(_playerView);
            
            _playerMoveEntity.currentMoveAnimationSpeed
                .Subscribe(_playerView.AnimateMove)
                .AddTo(_playerView);

            _playerParameter
                .ObserveEveryValueChanged(x => x.WalkSpeed)
                .Subscribe(_playerMoveEntity.SetWalkSpeedParameter);
            
            _playerParameter
                .ObserveEveryValueChanged(x => x.SlowWalkSpeed)
                .Subscribe(_playerMoveEntity.SetSlowWalkSpeedParameter);
        }
        
        public void Move()
        {
            if (InputKeyData.IsCharging||InputKeyData.CanUseMagic)
            {
                _playerMoveEntity.SetMoveMode(MoveMode.SlowWalk);
            }
            else
            {
                _playerMoveEntity.SetMoveMode(MoveMode.Walk);
            }
            UpdateMoveSpeedParameters();
        }

        //MEMO: キーの入力量によるパラメータ変化
        private void UpdateMoveSpeedParameters()
        {
            Vector3 inputValue= new Vector3(InputKeyData.HorizontalMoveValue, InputKeyData.VerticalMoveValue,0);

            var newMoveAnimationSpeed = inputValue / Enum.GetValues(typeof(MoveMode)).Length *
                                        (int) _playerMoveEntity.currentMoveMode;
            _playerMoveEntity.SetAnimationSpeed(newMoveAnimationSpeed);
            
            var newMoveSpeedRate=GetMoveSpeedRate(_playerMoveEntity.currentMoveMode);
            var newMoveSpeed = inputValue * newMoveSpeedRate;
            _playerMoveEntity.SetMoveSpeed(newMoveSpeed);
        }

        //MEMO: チャージ中にキャラの移動スピードが切り替わるため、enumを用いて区別する
        private float GetMoveSpeedRate(MoveMode moveMode)
        {
            return moveMode switch
            {
                MoveMode.Freeze => _playerMoveEntity.FreezeSpeedRate,
                MoveMode.SlowWalk => _playerMoveEntity.slowWalkSpeedRate,
                MoveMode.Walk => _playerMoveEntity.walkSpeedRate,
                _ => throw new ArgumentOutOfRangeException(nameof(moveMode), moveMode, null)
            };
        }
    }
}