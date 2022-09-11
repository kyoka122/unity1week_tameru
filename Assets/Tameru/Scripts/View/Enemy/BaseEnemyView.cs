using Tameru.Entity;
using UniRx;
using UnityEngine;

namespace Tameru.View
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class BaseEnemyView:MonoBehaviour
    {
        [SerializeField] private ExplosionView explosionView = default;

        public IReadOnlyReactiveProperty<bool> isAttackingPlayer => _isAttackingPlayer;
        public IReadOnlyReactiveProperty<MagicType> hitMagic=>_hitMagic;
        public abstract EnemyType type { get;  }
        public int hp { get; private set; }
        public int score { get; private set; }
        public Vector2 pos => gameObject.transform.position;
        
        private ReactiveProperty<bool> _isAttackingPlayer;
        private ReactiveProperty<MagicType> _hitMagic;

        private bool _hadInit=false;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private const string DamageString = "Damage";

        public void Init(int maxHp, int scoreValue)
        {
            _isAttackingPlayer = new ReactiveProperty<bool>(false);
            _hitMagic = new ReactiveProperty<MagicType>(MagicType.None);
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            hp = maxHp;
            score = scoreValue;
            _hadInit = true;
        }

        public void AddDamage(int damage,BaseEnemyView enemyView)
        {
            if (damage==0)
            {
                return;
            }
            //MEMO: Enemyが攻撃を受けたかどうかUnity上では分からないためLogに残す
            Debug.Log($"Enemy Add Damage! damage={damage}",enemyView);
            hp -= damage;
        }

        public void Destroy()
        {
            Destroy(gameObject);
            Instantiate(explosionView, transform.position, Quaternion.identity);
        }

        public void MoveEnemyMover(Vector2 newVelocity)
        {
            _rigidbody2D.velocity = newVelocity;
        }
        
        public void PlayDamagedAnimation()
        {
            _animator.SetTrigger(DamageString);
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