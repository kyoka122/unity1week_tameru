using UniRx;
using UnityEngine;

namespace Tameru.Entity
{
    public class PlayerHealthEntity
    {
        public IReadOnlyReactiveProperty<int> hp => _hp;
        public IReadOnlyReactiveProperty<bool> isAlive => _isAlive;

        private ReactiveProperty<int> _hp;
        private ReactiveProperty<bool> _isAlive;
        
        public PlayerHealthEntity()
        {
            _hp = new ReactiveProperty<int>();
            _isAlive = new ReactiveProperty<bool>(true);
        }

        public void SetMaxHp(int hp)
        {
            _hp.Value = hp;
        }
        
        public void AddDamage(int damage)
        {
            _hp.Value -= damage;
            CheckAlive();
        }

        private void CheckAlive()
        {
            if (_hp.Value<=0)
            {
                _isAlive.Value=false;
            }
        }
    }
}