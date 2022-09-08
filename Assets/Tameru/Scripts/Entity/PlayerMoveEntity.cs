using Tameru.Application;
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
    
    public class PlayerMoveEntity
    {
        public Vector3ReactiveProperty currentMoveSpeed => _currentMoveSpeed;
        public Vector3ReactiveProperty currentMoveAnimationSpeed => _currentMoveAnimationSpeed;
        
        private readonly Vector3ReactiveProperty _currentMoveSpeed;
        private readonly Vector3ReactiveProperty _currentMoveAnimationSpeed;

        public MoveMode currentMoveMode { get; private set; }

        public float FreezeSpeedRate { get; } = 0;
        public float walkSpeedRate{ get; private set; }
        public float slowWalkSpeedRate{ get; private set; }
        
        public PlayerMoveEntity(PlayerParameter playerParameter)
        {
            walkSpeedRate = playerParameter.WalkSpeed;
            slowWalkSpeedRate = playerParameter.SlowWalkSpeed;
            _currentMoveSpeed = new Vector3ReactiveProperty(Vector3.zero);
            _currentMoveAnimationSpeed = new Vector3ReactiveProperty(Vector3.zero);
        }
        
        public void SetWalkSpeedParameter(float newWalkSpeed)
        {
            walkSpeedRate = newWalkSpeed;
        }
        
        public void SetSlowWalkSpeedParameter(float newSlowWalkSpeed)
        {
            slowWalkSpeedRate=newSlowWalkSpeed;
        }

        //MEMO: チャージ中かどうかでキャラの移動スピードが切り替わるため、enumを用いて区別する
        public void SetMoveMode(MoveMode newMoveMode)
        {
            currentMoveMode = newMoveMode;
        }
        
        public void SetMoveSpeed(Vector3 newCurrentMoveSpeed)
        {
            _currentMoveSpeed.Value = newCurrentMoveSpeed;
        }

        public void SetAnimationSpeed(Vector3 newMoveAnimationSpeed)
        {
            _currentMoveAnimationSpeed.Value = newMoveAnimationSpeed;
        }

        
        //MEMO: Animator側で「ゆっくり歩く」と「歩く」を変更してくれているので必要なし
        /*public void ChangeMoveAnimation(MoveMode moveMode)
        {
            
        }*/
        

        

    }
}