using System;
using System.Linq;
using Tameru.Application;
using Tameru.Entity;
using Tameru.Parameter;
using Tameru.View;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tameru.Logic
{
    public class EnemySpawnLogic
    {
        private readonly EnemySpawnView _enemySpawnView;
        private readonly CameraView _cameraView;
        private readonly PhaseEntity _phaseEntity;
        private readonly EnemySpawnEntity _enemySpawnEntity;
        private readonly PhaseParameter _phaseParameter;
        private readonly EnemyCommonParameter _enemyCommonParameter;
        private readonly EnemyParameter _enemyParameter;

        private readonly Action<BaseEnemyView> _registerAttackingFlag;
        private readonly Action<BaseEnemyView> _registerHealthObserver;
        private readonly Action<BaseEnemyView> _registerMoveEnemyMover;

        public EnemySpawnLogic(EnemySpawnView enemySpawnView, CameraView cameraView, PhaseEntity phaseEntity,
            EnemySpawnEntity enemySpawnEntity, PhaseParameter phaseParameter, EnemyCommonParameter enemyCommonParameter,
            EnemyParameter enemyParameter,
            Action<BaseEnemyView> registerAttackingFlag, Action<BaseEnemyView> registerHealthObserver,
            Action<BaseEnemyView> registerMoveEnemyMover)
        {
            _enemySpawnView = enemySpawnView;
            _cameraView = cameraView;
            _phaseEntity = phaseEntity;
            _enemySpawnEntity = enemySpawnEntity;
            _phaseParameter = phaseParameter;
            _enemyCommonParameter = enemyCommonParameter;
            _enemyParameter = enemyParameter;
            _registerAttackingFlag = registerAttackingFlag;
            _registerHealthObserver = registerHealthObserver;
            _registerMoveEnemyMover = registerMoveEnemyMover;

            RegisterReactiveProperty();
        }

        private void RegisterReactiveProperty()
        {
            _phaseEntity.phaseProperty
                .Subscribe(_ =>
                {
                    if (!_phaseParameter.IsPlayingPhase(_phaseEntity.phase))
                    {
                        return;
                    }

                    float instanceInterval = _phaseParameter.GetEnemyInstanceInterval(_phaseEntity.phase);
                    _enemySpawnEntity.SetInterval(instanceInterval);
                })
                .AddTo(_enemySpawnView);
        }

        public void UpdateSpawnEnemy()
        {
            if (!_phaseParameter.IsPlayingPhase(_phaseEntity.phase))
            {
                Debug.Log($"FinishAllPhase!");
                return;
            }

            TimeInterval();
        }

        private void TimeInterval()
        {
            _enemySpawnEntity.AddPassedTime(Time.deltaTime);
            if (_enemySpawnEntity.currentPauseTime >= _enemySpawnEntity.currentInterval)
            {
                _enemySpawnEntity.ResetTime();
                BaseEnemyView enemyView = _enemySpawnView.InstanceEnemy(GetSpawnEnemy(), GetEnemySpawnPos());
                if (enemyView != null) //MEMO: 生成位置条件に当てはまるものがあったかどうか
                {
                    InitEnemyView(enemyView);
                }

            }
        }

        private void InitEnemyView(BaseEnemyView enemyView)
        {
            var hp = _enemyParameter.FindHp(enemyView.type);
            enemyView.Init(hp);
            _registerAttackingFlag.Invoke(enemyView);
            _registerHealthObserver.Invoke(enemyView);
            _registerMoveEnemyMover.Invoke(enemyView);
        }

        //MEMO: Spawnする場所の候補をあらかじめパラメータとして持っておき、画面外にある候補から抽選する
        private Vector2 GetEnemySpawnPos()
        {
            Vector3[] spawnAblePos = _enemyCommonParameter.SpawnerPos
                .Where(IsOutOfViewPort)
                .ToArray();

            int spawnPosIndex = Random.Range(0, spawnAblePos.Length);
            return spawnAblePos[spawnPosIndex];
        }

        private BaseEnemyView GetSpawnEnemy()
        {
            InstanceData[] instanceData = _phaseParameter.GetInstance(_phaseEntity.phase);
            if (instanceData.Length == 0)
            {
                throw new Exception("NoneInstanceData(Please Check PhaseParameter)");
            }

            int[] percentages = instanceData
                .Select(data => data.instanceRate)
                .ToArray();

            EnemyType spawnEnemyType = instanceData[GetRandomIndex(percentages)].type;
            BaseEnemyView spawnEnemy = _enemyParameter.FindPrefab(spawnEnemyType);
            return spawnEnemy;
        }

        /// <summary>
        /// 引数の配列に何も入ってない、または全て0の場合、-1を返す
        /// </summary>
        /// <param name="percentages"></param>
        /// <returns></returns>
        private int GetRandomIndex(int[] percentages)
        {
            int instanceNum = Random.Range(0, percentages.Sum());
            int a = 0;
            for (int i = 0; i < percentages.Length; i++)
            {
                a += percentages[i];
                if (a > instanceNum)
                {
                    return i;
                }
            }

            return -1;
        }

        private bool IsOutOfViewPort(Vector3 worldPos)
        {
            Vector3 viewPortPos = _cameraView.Camera.WorldToViewportPoint(worldPos);
            bool isOutOfXPort = viewPortPos.x < 0 || 1 < viewPortPos.x;
            bool isOutOfYPort = viewPortPos.y < 0 || 1 < viewPortPos.y;
            if (isOutOfXPort || isOutOfYPort)
            {
                return true;
            }

            return false;
        }
    }
}