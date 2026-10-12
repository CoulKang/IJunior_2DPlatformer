using System;
using UnityEngine;

namespace IJuniorPlatformer
{
    public class CharacterAnimator : MonoBehaviour
    {
        private readonly int SpeedHash = Animator.StringToHash("Speed");
        private readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");
        private readonly int IsJumpingHash = Animator.StringToHash("IsJumping");
        private readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");
        private readonly int IsFallingHash = Animator.StringToHash("IsFalling");
        private readonly int JumpHash = Animator.StringToHash("Jump");
        private readonly int AttackHash = Animator.StringToHash("Attack");

        [SerializeField] private Animator _animator;

        public void SetGrounded(bool isGrounded)
        {
            _animator.SetBool(IsGroundedHash, isGrounded);
        }

        public void SetJumping(bool isJumping)
        {
            _animator.SetBool(IsJumpingHash, isJumping);
        }

        public void SetAttacking(bool isAttacking)
        {
            _animator.SetBool(IsAttackingHash, isAttacking);
        }

        public void SetFall(bool isFalling)
        {
            _animator.SetBool(IsFallingHash, isFalling);
        }

        public void PlayIdle()
        {
            _animator.SetFloat(SpeedHash, 0f);
        }

        public void PlayMove(float direction)
        {
            _animator.SetFloat(SpeedHash, Mathf.Abs(direction));
        }

        public void PlayJump()
        {
            _animator.SetTrigger(JumpHash);
        }

        public void PlayAttack()
        {
            _animator.SetTrigger(AttackHash);
        }
    }
}
