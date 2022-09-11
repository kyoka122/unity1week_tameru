using EFUK;
using Tameru.Application;
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
        [SerializeField] private PlayerHealthView playerHealthView;

        [SerializeField] private ScoreView scoreView = default;
        [SerializeField] private TimeView timeView = default;
        [SerializeField] private GameStateView stateView = default;

        private void Start()
        {
            // Fade完了後に初期化
            this.Delay(UiConfig.FADE_TIME, Init);
        }

        private void Init()
        {
            var playerChargeEntity = new PlayerChargeEntity();
            var playerMoveEntity = new PlayerMoveEntity();
            var attackingEnemyEntity = new AttackingEnemyEntity();
            var playerHealthEntity = new PlayerHealthEntity();
            var phaseEntity = new PhaseEntity();
            var enemySpawnEntity = new EnemySpawnEntity();
            var scoreEntity = new ScoreEntity();
            var stateEntity = new GameStateEntity();

            var soundEntity = CommonInstaller.Instance.soundEntity;

            var gameTimeKeeperLogic = new GameTimeKeeperLogic(phaseEntity,phaseParameter);
            var enemyHealthLogic = new EnemyHealthLogic(playerMagicParameter, scoreEntity, soundEntity);
            var playerHealthLogic = new PlayerHealthLogic(playerHealthView, playerHealthEntity, playerParameter); 
            
            var chargeLogic = new PlayerChargeLogic(playerChargeEntity, soundEntity, playerChargeView,playerMagicView,playerMagicParameter);
            var playerLogic = new PlayerLogic(playerMoveEntity,playerHealthEntity,stateEntity,playerView,playerParameter);
            var playerUseMagicLogic = new PlayerUseMagicLogic(playerChargeEntity, playerMoveEntity, soundEntity, playerMagicView,playerMagicParameter);
            var enemyAttackLogic = new EnemyAttackLogic(attackingEnemyEntity, playerHealthEntity, playerMoveEntity,
                soundEntity,playerView, enemyParameter, enemyCommonParameter, playerParameter);
            var enemyLogic = new EnemyLogic(playerMoveEntity, enemyParameter, playerParameter);
            var enemySpawnLogic = new EnemySpawnLogic(enemySpawnView, cameraView, phaseEntity, enemySpawnEntity,
                stateEntity,
                phaseParameter, enemyCommonParameter, enemyParameter, enemyAttackLogic.registerAttackingFlag,
                enemyHealthLogic.registerHealthObserver,enemyLogic.registerMoveEnemyMover);

            var scoreLogic = new ScoreLogic(scoreEntity, scoreView);
            var timeLogic = new TimeLogic(phaseEntity, timeView);
            var stateLogic = new GameStateLogic(stateEntity, scoreEntity, stateView);

            soundEntity.SetUpPlayBgm(BgmType.Main);
            this.Delay(UiConfig.READY_TIME, () => stateEntity.Set(GameState.Main));
            this.UpdateAsObservable()
                .Where(_ => stateEntity.IsState(GameState.Main))
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