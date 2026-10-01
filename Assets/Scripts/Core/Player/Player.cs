using System;
using UnityEngine;

namespace IJuniorPlatformer
{
    [RequireComponent (typeof(CapsuleCollider2D), typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private Mover _mover;
        [SerializeField] private Rotator _rotator;
        [SerializeField] private Jumper _jumper;
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
            _input.Read();
            UpdateAnimations();
        }

        private void FixedUpdate()
        {
            _groundSensor.Tick();

            _mover.Tick(_input.MoveDirection);
            _rotator.Tick(_input.MoveDirection);

            _jumper.Tick(_input.JumpPressed, _groundSensor.IsGrounded);
            _input.ConsumeJump();
        }

        private void UpdateAnimations()
        {
            _animator.SetGrounded(_groundSensor.IsGrounded);

            if (_input.MoveDirection.x == 0f && _groundSensor.IsGrounded)
                _animator.PlayIdle();

            if (_input.MoveDirection.x != 0f && _groundSensor.IsGrounded)
                _animator.PlayMove(_input.MoveDirection);

            if (_input.JumpPressed)
            {
                _animator.PlayJump();
            }
        }

        private void HandleTriggerEnter(Collider2D collision)
        {
            if (collision.TryGetComponent<Coin>(out var coin))
            {
                coin.Pick();
                _wallet.AddCoin();
                return;
            }
        }
    }
}
