using System;
using Tameru.Entity;
using Tameru.Parameter;
using Tameru.View;
using UniRx;
using UnityEngine;

namespace Tameru.Logic
{
    public class EnemyHealthLogic
    {
        private readonly PlayerMagicParameter _playerMagicParameter;
        private readonly ScoreEntity _scoreEntity;
        private readonly ISoundEntity _soundEntity;
        
        public EnemyHealthLogic(PlayerMagicParameter playerMagicParameter, ScoreEntity scoreEntity, ISoundEntity soundEntity)
        {
            _playerMagicParameter = playerMagicParameter;
            _scoreEntity = scoreEntity;
            _soundEntity = soundEntity;
        }

        public Action<BaseEnemyView> registerHealthObserver => RegisterHealthObserver;

        private void RegisterHealthObserver(BaseEnemyView enemyView)
        {
            enemyView.hitMagic.Subscribe(hitMagic =>
            {
                AddDamage(enemyView, hitMagic);
                CheckDefeated(enemyView);
            }).AddTo(enemyView);

        }
        
        private void AddDamage(BaseEnemyView enemyView,MagicType hitMagic)
        {
            enemyView.AddDamage(_playerMagicParameter.FindDamage(hitMagic),enemyView);
            enemyView.PlayDamagedAnimation();
            _soundEntity.SetUpPlaySe(SeType.HitMagic);
        }

        private void CheckDefeated(BaseEnemyView enemyView)
        {
            if (enemyView.hp<=0)
            {
                enemyView.Destroy();
                _scoreEntity.Add(enemyView.score);
            }
        }
    }
}