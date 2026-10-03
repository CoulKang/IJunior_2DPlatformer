using UnityEngine;

namespace IJuniorPlatformer
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour 
    {
        [SerializeField] private Mover _mover;
        [SerializeField] private Rotator _rotator;
        [SerializeField] private Patrol _patrol;
        [SerializeField] private CharacterAnimator _animator;

        public void Initialize(float minX, float maxX)
        {
            _patrol.SetPatrolBounds(minX, maxX);
        }

        private void Update()
        {
            UpdateAnimations();
        }

        private void FixedUpdate()
        {
            _patrol.SetDirectionOnBounds();

            _mover.Move(_patrol.Direction.x);

            _rotator.Rotate(_patrol.Direction.x);
        }

        private void UpdateAnimations()
        {
            if (_patrol.Direction.x == 0f)
                _animator.PlayIdle();

            if (_patrol.Direction.x != 0f)
                _animator.PlayMove(_patrol.Direction.x);
        }
    }
}
