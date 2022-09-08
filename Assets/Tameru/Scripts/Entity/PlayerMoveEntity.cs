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
        public Vector3ReactiveProperty currentMoveVec => _currentMoveVec;
        public MoveMode currentMoveMode { get; private set; }
        private readonly Vector3ReactiveProperty _currentMoveVec;
        

        //MEMO: チャージ中かどうかでキャラの移動スピードが切り替わるため、enumを用いて区別する
        public void SetMoveMode(MoveMode newMoveMode)
        {
            currentMoveMode = newMoveMode;
        }
        
        public void SetMoveVec(Vector3 newMoveVec)
        {
            _currentMoveVec.Value = newMoveVec;
        }
        
        //MEMO: Animator側で「ゆっくり歩く」と「歩く」を変更してくれているので必要なし
        /*public void ChangeMoveAnimation(MoveMode moveMode)
        {
            
        }*/
        

        

    }
}