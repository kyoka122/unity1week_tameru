using Tameru.Application;
using Tameru.Entity;
using Tameru.Parameter;
using Tameru.View;
using UniRx;

namespace Tameru.Logic
{
    public class PlayerUseMagicLogic
    {
        private readonly PlayerChargeEntity _playerChargeEntity;
        private readonly PlayerMagicView _playerMagicView;
        private readonly PlayerMagicParameter _playerMagicParameter;

        public PlayerUseMagicLogic(PlayerChargeEntity playerChargeEntity,PlayerMagicView playerMagicView,PlayerMagicParameter playerMagicParameter)
        {
            _playerChargeEntity = playerChargeEntity;
            _playerMagicView = playerMagicView;
            _playerMagicParameter = playerMagicParameter;
            RegisterReactiveProperty();
        }

        private void RegisterReactiveProperty()
        {

        }

        public void UpdateUseMagic()
        {
            if (InputKeyData.CanUseMagic)
            {
                TryUseMagic();
            }
        }

        private bool TryUseMagic()
        {
            _playerChargeEntity.ConsumeAll();
            var magic = _playerChargeEntity.currentMagic.Value;
            if (magic==MagicMode.None)
            {
                return false;
            }
            _playerMagicView.UseMagic(magic);
            return true;
        }
    }
}