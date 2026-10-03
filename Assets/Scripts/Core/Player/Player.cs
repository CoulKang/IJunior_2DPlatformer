using UnityEngine;

namespace IJuniorPlatformer
{
    [RequireComponent (typeof(CapsuleCollider2D), typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private Mover _mover;
        [SerializeField] private Rotator _rotator;
        [SerializeField] private GroundSensor _groundSensor;
        [SerializeField] private CharacterAnimator _animator;
        [SerializeField] private CollisionHandler _collisionHandler;
        [SerializeField] private Wallet _wallet;

        public Wallet Wallet => _wallet;

        private void OnEnable()
        {
            _collisionHandler.TriggerEntered += HandleTriggerEnter;
        }

        private void OnDisable()
        {
            _collisionHandler.TriggerEntered -= HandleTriggerEnter;
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
                _mover.Jump();

            _rotator.Rotate(_input.MoveDirection.x);
        }

        private void UpdateAnimations()
        {
            _animator.SetGrounded(_groundSensor.IsGround);

            if (_input.MoveDirection.x == 0f && _groundSensor.IsGround)
                _animator.PlayIdle();

            if (_input.MoveDirection.x != 0f && _groundSensor.IsGround)
                _animator.PlayMove(_input.MoveDirection.x);

            if (_input.JumpPressed)
            {
                _animator.PlayJump();
            }
        }

        private void HandleTriggerEnter(Collider2D collision)
        {
            if (collision.TryGetComponent(out Coin resource))
            {
                resource.Pick();
                _wallet.AddCoin();
                return;
            }
        }
    }
}
