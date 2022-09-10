﻿using System;
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

        public PlayerLogic(PlayerMoveEntity playerMoveEntity,PlayerHealthEntity playerHealthEntity,PlayerView playerView,PlayerParameter playerParameter)
        {
            _playerMoveEntity = playerMoveEntity;
            _playerView = playerView;
            _playerParameter = playerParameter;
            
            _playerMoveEntity.SetMoveMode(MoveMode.Walk);
            playerHealthEntity.isAlive.Where(alive => !alive)
                .Subscribe(_=>Debug.Log("PlayerDefeated"))
                .AddTo(playerView);
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

            Vector2 inputVec= new Vector2(InputKeyData.HorizontalMoveValue, InputKeyData.VerticalMoveValue);
            Vector2 normalizedVec = inputVec.normalized;
            _playerMoveEntity.SetMoveVec(normalizedVec);
            _playerMoveEntity.SetPlayerPos(_playerView.transform.position);
            
            var newMoveAnimationSpeed = inputVec / Enum.GetValues(typeof(MoveMode)).Length *
                                        (int) _playerMoveEntity.moveMode;
            _playerView.AnimateMove(newMoveAnimationSpeed);
            
            var newMoveSpeedRate=GetMoveSpeedRate(_playerMoveEntity.moveMode);
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