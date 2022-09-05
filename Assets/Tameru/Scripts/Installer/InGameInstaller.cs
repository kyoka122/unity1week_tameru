using System;
using Tameru.Entity;
using Tameru.Logic;
using Tameru.View;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Tameru.Installer
{
    public class InGameInstaller:MonoBehaviour
    {
        [SerializeField] private ChargeView chargeView;
        
        private void Awake()
        {
            var chargeEntity = new ChargeEntity();
            var chargeLogic = new ChargeLogic(chargeEntity, chargeView);
            var playerLogic = new PlayerLogic(chargeEntity);

            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    playerLogic.Charge();
                })
                .AddTo(this);
        }
        
    }
}