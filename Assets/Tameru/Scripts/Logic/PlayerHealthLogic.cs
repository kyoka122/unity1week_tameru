using Tameru.Application;
using Tameru.Entity;
using Tameru.View;
using UniRx;

namespace Tameru.Logic
{
    public class PlayerHealthLogic
    {
        private readonly PlayerHealthView _playerHealthView;
        private readonly PlayerHealthEntity _playerHealthEntity;
        private readonly PlayerParameter _playerParameter;

        public PlayerHealthLogic(PlayerHealthView playerHealthView, PlayerHealthEntity playerHealthEntity, PlayerParameter playerParameter)
        {
            _playerHealthView = playerHealthView;
            _playerHealthEntity = playerHealthEntity;
            _playerParameter = playerParameter;
            
            Init();
        }

        public void Init()
        {
            _playerHealthEntity.SetMaxHp(_playerParameter.MaxHp);
            _playerHealthView.SetMaxHp(_playerParameter.MaxHp);
            _playerHealthEntity.hp.Subscribe(_playerHealthView.SetHp).AddTo(_playerHealthView);
        }

        
        
    }
}