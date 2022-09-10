using System;
using UniRx;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Tameru.View
{
    public abstract class BaseEnemyView:MonoBehaviour
    {
        public IReadOnlyReactiveProperty<bool> isAttackingPlayer => _isAttackingPlayer;
        public abstract EnemyType type { get;  }
        
        private ReactiveProperty<bool> _isAttackingPlayer;
        private bool _hadInit=false;

        public void Init()
        {
            _isAttackingPlayer = new ReactiveProperty<bool>(false);
            _hadInit = true;
        }

        /*protected void OnTriggerStay2D(Collider2D other)
        {
            if (!_hadInit)
            {
                return;
            }
            CheckCollidingPlayer(other);
        }*/

        /*private void CheckCollidingPlayer(Collider2D collider)
        {
            if (collider.gameObject.GetComponent<PlayerView>()!=null)
            {
                _isAttackingPlayer.Value = true;
                Debug.Log($"Attacking!");
            }
            Debug.Log($"NotAttacking!");
            _isAttackingPlayer.Value = false;
        }*/

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<PlayerView>()!=null)
            {
                _isAttackingPlayer.Value = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<PlayerView>()!=null)
            {
                _isAttackingPlayer.Value = false;
            }
        }
    }
}