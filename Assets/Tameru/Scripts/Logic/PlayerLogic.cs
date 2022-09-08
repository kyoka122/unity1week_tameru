using System;
using Tameru.Application;
using Tameru.Entity;
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

            Vector3 inputVec= new Vector3(InputKeyData.HorizontalMoveValue, InputKeyData.VerticalMoveValue,0);
            Vector3 normalizedVec = inputVec.normalized;
            _playerMoveEntity.SetMoveVec(normalizedVec);
            
            var newMoveAnimationSpeed = inputVec / Enum.GetValues(typeof(MoveMode)).Length *
                                        (int) _playerMoveEntity.currentMoveMode;
            _playerView.AnimateMove(newMoveAnimationSpeed);
            
            var newMoveSpeedRate=GetMoveSpeedRate(_playerMoveEntity.currentMoveMode);
            var newMoveSpeed = normalizedVec * newMoveSpeedRate;
            _playerView.Move(newMoveSpeed);
        }

        //MEMO: チャージ中にキャラの移動スピードが切り替わるため、enumを用いて区別する
        private float GetMoveSpeedRate(MoveMode moveMode)
        {
            return moveMode switch
            {
                MoveMode.Freeze => _playerParameter.FreezeSpeed,
                MoveMode.SlowWalk => _playerParameter.SlowWalkSpeed,
                MoveMode.Walk => _playerParameter.WalkSpeed,
                _ => throw new ArgumentOutOfRangeException(nameof(moveMode), moveMode, null)
            };
        }
    }
}