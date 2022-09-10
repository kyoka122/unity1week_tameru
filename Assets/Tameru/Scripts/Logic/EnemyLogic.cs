using System;
using Tameru.Application;
using Tameru.Entity;
using Tameru.Parameter;
using Tameru.View;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;

namespace Tameru.Logic
{
    public class EnemyLogic
    {
        private readonly PlayerMoveEntity _playerMoveEntity;
        private readonly EnemyParameter _enemyParameter;
        private readonly PlayerParameter _playerParameter;
        
        public Action<BaseEnemyView> registerMoveEnemyMover => RegisterMoveEnemyMover;

        public EnemyLogic(PlayerMoveEntity playerMoveEntity, EnemyParameter enemyParameter, PlayerParameter playerParameter)
        {
            _playerMoveEntity = playerMoveEntity;
            _enemyParameter = enemyParameter;
            _playerParameter = playerParameter;
        }

        private void RegisterMoveEnemyMover(BaseEnemyView enemyView)
        {
            enemyView.UpdateAsObservable().Subscribe(_ =>
            {
                Debug.Log($"GetToTargetVector(enemyView.pos):{GetToTargetVector(enemyView.pos)}");
                Vector2 newVelocity = GetToTargetVector(enemyView.pos).normalized * _enemyParameter.FindSpeed(enemyView.type);
                enemyView.MoveEnemyMover(newVelocity);
            }).AddTo(enemyView);

        }

        private Vector2 GetToTargetVector(Vector2 enemyPos)
        {
            float toPlayerDistance = Vector2.Distance(enemyPos, _playerMoveEntity.pos);
            Debug.Log($"enemyPos:{enemyPos}");
            Debug.Log($"_playerMoveEntity.pos:{_playerMoveEntity.pos}");
            return Vector2.MoveTowards(enemyPos,_playerMoveEntity.pos, toPlayerDistance-_playerParameter.Territory);
        }
    }
}