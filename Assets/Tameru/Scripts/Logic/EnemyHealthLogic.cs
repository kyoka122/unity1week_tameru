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
        
        public EnemyHealthLogic(PlayerMagicParameter playerMagicParameter)
        {
            _playerMagicParameter = playerMagicParameter;
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
        }

        private void CheckDefeated(BaseEnemyView enemyView)
        {
            if (enemyView.hp<=0)
            {
                enemyView.Destroy();
            }
            
        }
    }
}