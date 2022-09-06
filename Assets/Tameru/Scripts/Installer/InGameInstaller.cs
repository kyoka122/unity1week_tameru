using Tameru.Entity;
using Tameru.Logic;
using Tameru.Struct;
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
        [SerializeField] private PlayerParameter playerParameter;
        
        private void Awake()
        {
            var chargeEntity = new ChargeEntity();
            var chargeLogic = new ChargeLogic(chargeEntity, chargeView);
            var playerEntity = new PlayerEntity(playerParameter);
            
            
            var playerLogic = new PlayerLogic(chargeEntity,playerEntity,playerView,playerParameter);

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