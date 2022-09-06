using System;
using Tameru.Struct;
using UniRx;
using UnityEngine;

namespace Tameru.Entity
{
    public enum MoveMode
    {
        Freeze,
        SlowWalk,
        Walk
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

        private MoveMode _currentMoveMode;

        private float _walkSpeedRate=0;
        private float _slowWalkSpeedRate=0;
        private float _currentSpeedRate=0;

        

        public PlayerEntity(PlayerParameter playerParameter)
        {
            _walkSpeedRate = playerParameter.WalkSpeed;
            _slowWalkSpeedRate = playerParameter.SlowWalkSpeed;
            _currentMoveSpeed = new Vector3ReactiveProperty(Vector3.zero);
            _currentMoveAnimationSpeed = new Vector3ReactiveProperty(Vector3.zero);
        }
        
        public void SetWalkSpeedParameter(float newWalkSpeed)
        {
            _walkSpeedRate = newWalkSpeed;
        }
        
        public void SetSlowWalkSpeedParameter(float newSlowWalkSpeed)
        {
            _slowWalkSpeedRate=newSlowWalkSpeed;
        }

        public void ChangeSpeed(float verticalInputValue,float horizontalInputValue)
        {
            Vector3 inputValue= new Vector3(horizontalInputValue,  verticalInputValue,0);
            _currentMoveAnimationSpeed.Value = inputValue / (int) MoveMode.Walk * (int) _currentMoveMode;
            _currentMoveSpeed.Value = inputValue * _currentSpeedRate;
        }

        //MEMO: チャージ中にキャラの移動スピードが切り替わるため、enumを用いて区別する
        public void ChangeSpeedRate(MoveMode moveMode)
        {
            _currentMoveMode = moveMode;
            _currentSpeedRate = _currentMoveMode switch
            {
                MoveMode.Freeze => FreezeSpeedRate,
                MoveMode.SlowWalk => _slowWalkSpeedRate,
                MoveMode.Walk => _walkSpeedRate,
                _ => throw new ArgumentOutOfRangeException(nameof(moveMode), moveMode, null)
            };
        }
        
        //MEMO: Animator側で「ゆっくり歩く」と「歩く」を変更してくれているので必要なし
        /*public void ChangeMoveAnimation(MoveMode moveMode)
        {
            
        }*/
        

        

    }
}