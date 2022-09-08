﻿using Tameru.Entity;
using Tameru.Logic;
using Tameru.Parameter;
using Tameru.Struct;
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
        
        [SerializeField] private PlayerParameter playerParameter;
        [SerializeField] private PlayerMagicParameter playerMagicParameter;
        
        
        private void Awake()
        {
            var playerChargeEntity = new PlayerChargeEntity(playerMagicParameter.GetNeedChargeParameter());
            var chargeLogic = new PlayerChargeLogic(playerChargeEntity, playerChargeView,playerMagicView,playerMagicParameter);
            var playerEntity = new PlayerMoveEntity(playerParameter);
            var playerLogic = new PlayerLogic(playerEntity,playerView,playerParameter);
            var playerUseMagicLogic = new PlayerUseMagicLogic(playerChargeEntity, playerMagicView,playerMagicParameter);

            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    chargeLogic.UpdateCharge();
                    playerUseMagicLogic.UpdateUseMagic();
                    playerLogic.Move();
                })
                .AddTo(this);
        }
        
    }
}