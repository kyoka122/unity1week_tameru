﻿using Tameru.Application;
using Tameru.Entity;
using Tameru.Logic;
using Tameru.Parameter;
using Tameru.View;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Tameru.Installer
{
    public class InGameInstaller:MonoBehaviour
    {
        [SerializeField] private PlayerChargeView playerChargeView;
        [SerializeField] private PlayerView playerView;
        [SerializeField] private PlayerMagicView playerMagicView;
        [SerializeField] private CameraView cameraView;

        [SerializeField] private PlayerParameter playerParameter;
        [SerializeField] private PlayerMagicParameter playerMagicParameter;
        [SerializeField] private EnemyParameter enemyParameter;
        [SerializeField] private EnemyCommonParameter enemyCommonParameter;
        [SerializeField] private EnemySpawnView enemySpawnView;
        [SerializeField] private PhaseParameter phaseParameter;
        
        
        
        
        private void Awake()
        {
            var playerChargeEntity = new PlayerChargeEntity();
            var playerEntity = new PlayerMoveEntity();
            var attackingEnemyEntity = new AttackingEnemyEntity();
            var playerHealthEntity = new PlayerHealthEntity();
            var phaseEntity = new PhaseEntity();
            var enemySpawnEntity = new EnemySpawnEntity();
            var gameTimeKeeperLogic = new GameTimeKeeperLogic(phaseEntity,phaseParameter);
            var enemyHealthLogic = new EnemyHealthLogic(playerMagicParameter);
            
            var chargeLogic = new PlayerChargeLogic(playerChargeEntity, playerChargeView,playerMagicView,playerMagicParameter);
            var playerLogic = new PlayerLogic(playerEntity,playerView,playerParameter);
            var playerUseMagicLogic = new PlayerUseMagicLogic(playerChargeEntity, playerMagicView,playerMagicParameter);
            var enemyAttackLogic = new EnemyAttackLogic(attackingEnemyEntity, playerHealthEntity, enemyParameter,
                enemyCommonParameter);
            var enemySpawnLogic = new EnemySpawnLogic(enemySpawnView, cameraView, phaseEntity, enemySpawnEntity,
                phaseParameter, enemyCommonParameter, enemyParameter, enemyAttackLogic.registerAttackingFlag,
                enemyHealthLogic.registerHealthObserver);
            

            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    chargeLogic.UpdateCharge();
                    playerUseMagicLogic.UpdateUseMagic();
                    playerLogic.Move();
                    enemySpawnLogic.UpdateSpawnEnemy();
                    enemyAttackLogic.CheckEnemyAttackStatus();
                    
                    //MEMO: ↓ゲーム開始後に呼び出す
                    gameTimeKeeperLogic.UpdateGameTime();
                })
                .AddTo(this);
        }
        
    }
}