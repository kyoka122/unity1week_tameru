using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly EnemyCommonParameter _enemyCommonParameter;
        private readonly PlayerHealthEntity _playerHealthEntity;
        
        private readonly EnemyParameter _enemyParameter;

        public EnemyAttackLogic(AttackingEnemyEntity attackingEnemyEntity, PlayerHealthEntity playerHealthEntity,
            EnemyParameter enemyParameter, EnemyCommonParameter enemyCommonParameter)
        {
            _attackingEnemyEntity = attackingEnemyEntity;
            _playerHealthEntity = playerHealthEntity;
            _enemyCommonParameter = enemyCommonParameter;
            _enemyParameter = enemyParameter;
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
                .Subscribe(_ => { _attackingEnemyEntity.UnRegisterAttackingEnemy(enemyView); }).AddTo(enemyView);
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

                    float damage = _enemyParameter.FindAttack(enemyView.type);
                    _playerHealthEntity.AddDamage(damage);
                    _attackingEnemyEntity.ResetTime(enemyView);
                }
            }
        }
        
        
    }
}