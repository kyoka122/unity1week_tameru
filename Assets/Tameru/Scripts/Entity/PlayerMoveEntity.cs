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
        public MoveMode moveMode { get; private set; }
        public Vector2 moveVec { get; private set; }
        public Vector2 pos { get; private set; }

        //MEMO: チャージ中かどうかでキャラの移動スピードが切り替わるため、enumを用いて区別する
        public void SetMoveMode(MoveMode newMoveMode)
        {
            moveMode = newMoveMode;
        }
        
        public void SetMoveVec(Vector2 newMoveVec)
        {
            moveVec = newMoveVec;
        }

        public void SetPlayerPos(Vector2 newPos)
        {
            pos = newPos;
        }
        
        //MEMO: Animator側で「ゆっくり歩く」と「歩く」を変更してくれているので必要なし
        /*public void ChangeMoveAnimation(MoveMode moveMode)
        {
            
        }*/
        

        

    }
}