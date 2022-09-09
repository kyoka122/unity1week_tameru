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
        public MoveMode currentMoveMode { get; private set; }
        public Vector3 currentMoveVec { get; private set; }

        //MEMO: チャージ中かどうかでキャラの移動スピードが切り替わるため、enumを用いて区別する
        public void SetMoveMode(MoveMode newMoveMode)
        {
            currentMoveMode = newMoveMode;
        }
        
        public void SetMoveVec(Vector3 newMoveVec)
        {
            currentMoveVec = newMoveVec;
        }
        
        //MEMO: Animator側で「ゆっくり歩く」と「歩く」を変更してくれているので必要なし
        /*public void ChangeMoveAnimation(MoveMode moveMode)
        {
            
        }*/
        

        

    }
}