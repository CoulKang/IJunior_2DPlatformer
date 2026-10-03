using UnityEngine;

namespace IJuniorPlatformer
{
    public class CharacterAnimator : MonoBehaviour
    {
        private readonly int SpeedHash = Animator.StringToHash("Speed");
        private readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");
        private readonly int JumpHash = Animator.StringToHash("Jump");

        [SerializeField] private Animator _animator;

        public void SetGrounded(bool isGrounded)
        {
            _animator.SetBool(IsGroundedHash, isGrounded);
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
    }
}
