using System;
using Tameru.Logic;
using Tameru.Parameter;
using Tameru.Struct;
using UniRx;
using UnityEngine;

namespace Tameru.Entity
{
    public enum MoveMode
    {
        Freeze,
        Walk,
        Run
    }
    
    public class PlayerEntity
    {
        public Vector3ReactiveProperty currentMoveSpeed => _currentMoveSpeed;
        public Vector3ReactiveProperty currentMoveAnimationSpeed => _currentMoveAnimationSpeed;
        
        private readonly Vector3ReactiveProperty _currentMoveSpeed;
        private readonly Vector3ReactiveProperty _currentMoveAnimationSpeed;
        
        private const float FreezeSpeedRate=0;
        //private readonly float _slowWalkSpeedRate;
        //private readonly float _walkSpeedRate;
        private PlayerParameter _playerParameter;
        
        private MoveMode _currentMoveMode;

        private float _speedRate=1;

        //private ReactiveProperty<float> _currentHorizontalSpeed;

        public PlayerEntity()
        {
            _currentMoveSpeed = new Vector3ReactiveProperty(Vector3.zero);
            _currentMoveAnimationSpeed = new Vector3ReactiveProperty(Vector3.zero);
        }
        
        public void ChangeWalkSpeedParameter(PlayerParameter playerParameter)
        {
            _playerParameter = playerParameter;
        }
        
        public void ChangeSpeed(float verticalInputValue,float horizontalInputValue)
        {
            _currentMoveAnimationSpeed.Value =
                new Vector3(horizontalInputValue,  verticalInputValue,0) / (int) MoveMode.Run;
            _currentMoveSpeed.Value = _currentMoveAnimationSpeed.Value * _speedRate;
        }

        //MEMO: チャージ中にキャラの移動スピードが切り替わるため、enumを用いて区別する
        public void ChangeSpeedRate(MoveMode moveMode)
        {
            _currentMoveMode = moveMode;
            _speedRate = _currentMoveMode switch
            {
                MoveMode.Freeze => FreezeSpeedRate,
                MoveMode.Walk => _playerParameter.slowWalkSpeed,
                MoveMode.Run => _playerParameter.walkSpeed,
                _ => throw new ArgumentOutOfRangeException(nameof(moveMode), moveMode, null)
            };
        }
        
        //MEMO: Animator側で「ゆっくり歩く」と「歩く」を変更してくれているので必要なし
        /*public void ChangeMoveAnimation(MoveMode moveMode)
        {
            
        }*/
        

        

    }
}