using UnityEditor.Sprites;
using UnityEngine;

namespace IJuniorPlatformer
{
    [RequireComponent (typeof(CapsuleCollider2D), typeof(Rigidbody2D))]
    public class Player : MonoBehaviour, IDamageable
    {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private Health _health;
        [SerializeField] private Mover _mover;
        [SerializeField] private Combat _combat;
        [SerializeField] private Rotator _rotator;
        [SerializeField] private GroundSensor _groundSensor;
        [SerializeField] private CharacterAnimator _animator;
        [SerializeField] private AnimEventCatcher _animEventCatcher;
        [SerializeField] private CollisionHandler _collisionHandler;
        [SerializeField] private Wallet _wallet;

        private bool _isJumping;
        private bool _isAttacking;

        public Wallet Wallet => _wallet;    

        public Health Health => _health;

        private void OnEnable()
        {
            _collisionHandler.TriggerEntered += HandleTriggerEnter;
            _animEventCatcher.AttackFinished += FinishAttack;
            _animEventCatcher.ActivateAttack += ActivateAttack;
        }

        private void OnDisable()
        {
            _collisionHandler.TriggerEntered -= HandleTriggerEnter;
            _animEventCatcher.AttackFinished -= FinishAttack;
            _animEventCatcher.ActivateAttack -= ActivateAttack;
        }

        private void Update()
        {
            UpdateAnimations();
        }

        private void FixedUpdate()
        {
            _groundSensor.Scan();

            _mover.Move(_input.MoveDirection.x);

            if (_input.GetIsJump() && _groundSensor.IsGround)
            {
                _mover.Jump();
                _isJumping = true;
            }

            if (_groundSensor.IsGround && Mathf.Abs(_mover.VelocityY) < 0.01f || _isAttacking)
                _isJumping = false;

            if (_input.GetIsAttack() && _isAttacking == false)
            {
                _isAttacking = true;
            }

            _rotator.Rotate(_input.MoveDirection.x);
        }

        public void TakeDamage(float damage)
        {
            _health.Deduct(damage);

            if (_health.IsAlive == false)
                Destroy(gameObject);
        }

        private void UpdateAnimations()
        {
            bool isGrounded = _groundSensor.IsGround;
            bool isFalling = isGrounded == false && _mover.VelocityY < 0f;

            _animator.SetGrounded(isGrounded);
            _animator.SetFall(isFalling);
            _animator.SetJumping(_isJumping);
            _animator.SetAttacking(_isAttacking);

            if (_input.MoveDirection.x == 0f && isGrounded)
                _animator.PlayIdle();

            if (_input.MoveDirection.x != 0f && isGrounded)
                _animator.PlayMove(_input.MoveDirection.x);

            if (_input.JumpPressed)
            {
                _animator.PlayJump();
            }

            if (_input.AttackPressed)
            {
                _animator.PlayAttack();
            }
        }

        private void FinishAttack()
        {
            _isAttacking = false;
        }

        private void ActivateAttack()
        {
            _combat.Attack();
        }

        private void HandleTriggerEnter(Collider2D collision)
        {
            if (collision.TryGetComponent(out IPickable resource))
            {
                if (collision.TryGetComponent(out Coin _))
                    _wallet.AddCoin();

                if (collision.TryGetComponent(out MedicineKit medicineKit))
                {
                    if (Health.IsFull)
                        return;

                    _health.Recover(medicineKit.HealthScore);
                }

                resource.Pick();

                return;
            }
        }
    }
}
