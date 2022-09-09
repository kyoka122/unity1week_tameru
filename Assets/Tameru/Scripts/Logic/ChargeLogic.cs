using Tameru.Entity;
using Tameru.View;
using UniRx;

namespace Tameru.Logic
{
    public class ChargeLogic
    {
        private readonly ChargeEntity _chargeEntity;
        private readonly ChargeView _chargeView;

        public ChargeLogic(ChargeEntity chargeEntity, ChargeView chargeView)
        {
            _chargeEntity = chargeEntity;
            _chargeView = chargeView;
            
            _chargeEntity.value
                .Subscribe( _chargeView.Render)
                .AddTo(_chargeView);
        }


    }
}