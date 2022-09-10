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
        public Vector2 direction { get; private set; }

        public PlayerMoveEntity()
        {
            direction  = Vector2.down;
        }

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

        public void SetDirection(Vector2 newDirection)
        {
            direction = newDirection;
        }
    }
}