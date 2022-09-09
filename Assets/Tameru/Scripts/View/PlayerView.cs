using Tameru.Application;
using UnityEngine;

namespace Tameru.View
{
    public class PlayerView:MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody;
        [SerializeField] private Animator animator;
        
        public void Move(Vector3 moveSpeed)
        {
            rigidbody.velocity = moveSpeed;
        }

        public void AnimateMove(Vector3 animationSpeed)
        {
            animator.SetFloat(AnimationParameterStrings.FrontMove, animationSpeed.y);
            animator.SetFloat(AnimationParameterStrings.RightMove, animationSpeed.x);
        }

        public void OnCollisionEnter(Collision other)
        {
            
        }
    }
}