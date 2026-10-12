using System.Collections;
using UnityEngine;

namespace IJuniorPlatformer
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour, IDamageable 
    {
        [SerializeField] private Mover _mover;
        [SerializeField] private Health _health;
        [SerializeField] private Combat _combat;
        [SerializeField] private Rotator _rotator;
        [SerializeField] private PlayerDetector _detector;
        [SerializeField] private Patrol _patrol;
        [SerializeField] private Chaser _chaser;
        [SerializeField] private CharacterAnimator _animator;
        [SerializeField] private AnimEventCatcher _animEventCatcher;

        [SerializeField, Min(0f)] private float _attackCooldown = 2f;

        private Coroutine _attackCooldownCoroutine = null;
        private bool _isAttacking;

        public bool IsChasing => _chaser.HasTarget;

        public float TotalDirection { get; private set; }

        public void Initialize(float minX, float maxX)
        {
            _patrol.SetPatrolBounds(minX, maxX);
        }

        private void OnEnable()
        {
            _detector.TargetDetected += OnTargetDetected;
            _detector.TargetLost += OnTargetLost;
            _animEventCatcher.AttackActivated += OnAttackActivated;
            _animEventCatcher.AttackFinished += OnAttackFinished;
        }

        private void OnDisable()
        {
            _detector.TargetDetected -= OnTargetDetected;
            _detector.TargetLost -= OnTargetLost;
            _animEventCatcher.AttackActivated -= OnAttackActivated;
            _animEventCatcher.AttackFinished -= OnAttackFinished;
        }

        private void Update()
        {
            UpdateAnimations(TotalDirection);
        }

        private void FixedUpdate()
        {
            TotalDirection = GetDirection();

            Move(TotalDirection);

            if (IsChasing)
                TryAttack();
        }

        public void TakeDamage(float damage)
        {
            _health.Deduct(damage);

            if (_health.IsAlive == false)
                Destroy(gameObject);
        }

        private float GetDirection()
        {
            if (IsChasing)
            {
                _chaser.Activate();
                return _chaser.Direction;
            }

            _patrol.SetDirectionOnBounds();
            return _patrol.Direction.x;
        }

        private void Move(float directionX)
        {
            _mover.Move(directionX);

            _rotator.Rotate(directionX);
        }

        private void TryAttack()
        {
            if (_chaser.IsTargetInAttackRange == false)
                return;

            if (_isAttacking)
                return;

            if (_attackCooldownCoroutine != null)
                return;

            _isAttacking = true;
            _animator.PlayAttack();
        }

        private void OnAttackActivated()
        {
            _combat.Attack();
        }

        private void OnAttackFinished()
        {
            _isAttacking = false;
            _attackCooldownCoroutine = StartCoroutine(AttackCooldown(_attackCooldown));
        }

        private IEnumerator AttackCooldown(float cooldown)
        {
            yield return new WaitForSeconds(cooldown);

            _attackCooldownCoroutine = null;
        }

        private void UpdateAnimations(float directionX)
        {
            if (_isAttacking)
                return;

            if (_chaser.IsTargetInAttackRange)
                return;

            if (directionX == 0f)
                _animator.PlayIdle();

            if (directionX != 0f)
                _animator.PlayMove(directionX);
        }

        private void OnTargetDetected(Transform target)
        {
            _chaser.SetTarget(target);
        }

        private void OnTargetLost()
        {
            _chaser.ClearTarget();
        }
    }
}
