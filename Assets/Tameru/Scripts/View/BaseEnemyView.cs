using Tameru.Entity;
using UniRx;
using UnityEngine;

namespace Tameru.View
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class BaseEnemyView:MonoBehaviour
    {
        public IReadOnlyReactiveProperty<bool> isAttackingPlayer => _isAttackingPlayer;
        public IReadOnlyReactiveProperty<MagicType> hitMagic=>_hitMagic;
        public abstract EnemyType type { get;  }
        public int hp { get; private set; }
        public Vector2 pos => gameObject.transform.position;
        
        private ReactiveProperty<bool> _isAttackingPlayer;
        private ReactiveProperty<MagicType> _hitMagic;

        private bool _hadInit=false;
        private Rigidbody2D _rigidbody2D;

        public void Init(int maxHp)
        {
            _isAttackingPlayer = new ReactiveProperty<bool>(false);
            _hitMagic = new ReactiveProperty<MagicType>(MagicType.None);
            _rigidbody2D = GetComponent<Rigidbody2D>();
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

        public void MoveEnemyMover(Vector2 newVelocity)
        {
            _rigidbody2D.velocity = newVelocity;
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