using System;
using Tameru.Application;
using UnityEngine;

namespace Tameru.View
{
    public class PlayerView:MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody;
        [SerializeField] private Animator animator;
        
        public void Move(Vector2 moveSpeed)
        {
            rigidbody.velocity = moveSpeed;
        }

        public void AnimateMove(Vector2 animationSpeed)
        {
            animator.SetFloat(AnimationParameterStrings.FrontMove, animationSpeed.y);
            animator.SetFloat(AnimationParameterStrings.RightMove, animationSpeed.x);
        }
    }
}