using Tameru.Entity;
using UniRx;
using UnityEngine;

namespace Tameru.View
{
    public abstract class BaseEnemyView:MonoBehaviour
    {
        public IReadOnlyReactiveProperty<bool> isAttackingPlayer => _isAttackingPlayer;
        public IReadOnlyReactiveProperty<MagicType> hitMagic=>_hitMagic;
        public abstract EnemyType type { get;  }
        public int hp { get; private set; }
        
        private ReactiveProperty<bool> _isAttackingPlayer;
        private ReactiveProperty<MagicType> _hitMagic;

        private bool _hadInit=false;

        public void Init(int maxHp)
        {
            _isAttackingPlayer = new ReactiveProperty<bool>(false);
            _hitMagic = new ReactiveProperty<MagicType>(MagicType.None);
            hp = maxHp;
            _hadInit = true;
        }

        public void AddDamage(int damage,BaseEnemyView enemyView)
        {
            //MEMO: Enemyが攻撃を受けたかどうかUnity上では分からないためLogに残す
            Debug.Log($"Enemy Add Damage! damage={damage}",enemyView);
            hp -= damage;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_hadInit)
            {
                return;
            }
            if (other.gameObject.GetComponent<PlayerView>()!=null)
            {
                _isAttackingPlayer.Value = true;
            }
            if (other.gameObject.TryGetComponent(out BaseMagicView magicView))
            {
                _hitMagic.Value = magicView.magicType;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!_hadInit)
            {
                return;
            }
            if (other.gameObject.GetComponent<PlayerView>()!=null)
            {
                _isAttackingPlayer.Value = false;
            }
        }
    }
}