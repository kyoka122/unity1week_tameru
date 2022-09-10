using System;
using System.Collections.Generic;
using Tameru.Application;
using Tameru.Entity;
using Tameru.Parameter;
using Tameru.View;
using UniRx;
using UnityEngine;

namespace Tameru.Logic
{
    public class EnemyAttackLogic
    {
        public Action<BaseEnemyView> registerAttackingFlag => RegisterReactiveProperty;

        private readonly AttackingEnemyEntity _attackingEnemyEntity;
        private readonly PlayerHealthEntity _playerHealthEntity;
        private readonly PlayerMoveEntity _playerMoveEntity;
        private readonly PlayerView _playerView;

        private readonly EnemyCommonParameter _enemyCommonParameter;
        private readonly EnemyParameter _enemyParameter;
        private readonly PlayerParameter _playerParameter;

        public EnemyAttackLogic(AttackingEnemyEntity attackingEnemyEntity, PlayerHealthEntity playerHealthEntity,
            PlayerMoveEntity playerMoveEntity,
            PlayerView playerView,EnemyParameter enemyParameter, EnemyCommonParameter enemyCommonParameter,
            PlayerParameter playerParameter)
        {
            _attackingEnemyEntity = attackingEnemyEntity;
            _playerHealthEntity = playerHealthEntity;
            _playerMoveEntity = playerMoveEntity;
            _playerView = playerView;
            _enemyCommonParameter = enemyCommonParameter;
            _enemyParameter = enemyParameter;
            _playerParameter = playerParameter;

            
        }

        public void CheckEnemyAttackStatus()
        {
            IReadOnlyDictionary<BaseEnemyView, float> attackingTime =new Dictionary<BaseEnemyView, float>(_attackingEnemyEntity.attackingTime);
            AddPassedTime(attackingTime);
            CheckTimeOver(attackingTime);
        }
        
        //MEMO: EnemySpawnLogicで登録するされる
        private void RegisterReactiveProperty(BaseEnemyView enemyView)
        {
            enemyView.isAttackingPlayer
                .Where(isAttacking => isAttacking)
                .Subscribe(_ =>
                {
                    _attackingEnemyEntity.RegisterAttackingEnemy(enemyView);
                }).AddTo(enemyView);

            enemyView.isAttackingPlayer
                .Where(isAttacking => !isAttacking)
                .Subscribe(_ =>  _attackingEnemyEntity.UnRegisterAttackingEnemy(enemyView)).AddTo(enemyView);
        }

        private void AddPassedTime(IReadOnlyDictionary<BaseEnemyView, float> attackingTime)
        {
            float passedTime = Time.deltaTime;
            foreach (var enemy in attackingTime)
            {
                _attackingEnemyEntity.AddTime(enemy.Key, passedTime);
            }
        }

        private void CheckTimeOver(IReadOnlyDictionary<BaseEnemyView, float> attackingTime)
        {
            foreach (var attackingTimes in attackingTime)
            {
                if (attackingTimes.Value>=_enemyCommonParameter.DamageInterval)
                {
                    BaseEnemyView enemyView = attackingTimes.Key;

                    int damage = _enemyParameter.FindAttack(enemyView.type);
                    _playerHealthEntity.AddDamage(damage);
                    _playerView.NockBack(-GetVecToPlayer(_playerMoveEntity.pos, enemyView.pos) *
                                         _playerParameter.NockBackForce);
                    _attackingEnemyEntity.ResetTime(enemyView);
                }
            }
        }

        private Vector2 GetVecToPlayer(Vector2 current,Vector2 target)
        {
            return target - current;
        }

    }
}