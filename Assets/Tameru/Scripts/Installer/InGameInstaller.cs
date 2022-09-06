using System;
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
        [SerializeField] private ChargeView chargeView;
        [SerializeField] private PlayerView playerView;
        [SerializeField] private PlayerParameterSerializer playerParameterSerializer;
        
        private void Awake()
        {
            var chargeEntity = new ChargeEntity();
            var chargeLogic = new ChargeLogic(chargeEntity, chargeView);
            var playerEntity = new PlayerEntity();
            
            
            var playerLogic = new PlayerLogic(chargeEntity,playerEntity,playerView,playerParameterSerializer);

            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    playerLogic.Charge();
                    playerLogic.Move();
                })
                .AddTo(this);
        }
        
    }
}