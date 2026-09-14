using UnityEngine;

namespace IJuniorPlatformer
{
    public class EnemyMovement : MonoBehaviour
    {
        private readonly int MoveHash = Animator.StringToHash("Move");

        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private DetectCheck _detector;

        [Space(10)]
        [SerializeField, Min(0)] private float _moveSpeed = 2f;

        private float _direction;
        private float _moveVertical;
        private float _moveHorizontal;

        private float _minBoundX;
        private float _maxBoundX;
        private bool _hasBounds;

        private bool _playerDetected;
        private Transform _playerTransform;

        private void OnEnable()
        {
            _detector.PlayerDetected += OnPlayerDetected;
            _detector.PlayerLost += OnPlayerLost;
        }

        private void OnDisable()
        {
            _detector.PlayerDetected -= OnPlayerDetected;
            _detector.PlayerLost -= OnPlayerLost;
        }

        private void Update()
        {
            UpdateAnimator();
        }

        private void FixedUpdate()
        {
            if (_playerDetected)
            {
                ChasePlayer();
                return;
            }

            if (_hasBounds == false)
                return;

            Patrol();
        }

        public void SetPatrolBounds(float minX, float maxX)
        {
            _minBoundX = minX;
            _maxBoundX = maxX;
            _hasBounds = true;

            float centerBound = (minX + maxX) * 0.5f;

            if (transform.position.x < centerBound)
                _direction = 1f;
            else
                _direction = -1f;

            Flip(_direction);
        }

        private void OnPlayerDetected(Transform playerTransform)
        {
            _playerDetected = true;
            _playerTransform = playerTransform;
        }

        private void OnPlayerLost()
        {
            _playerDetected = false;
            _playerTransform = null;
        }

        private void Patrol()
        {
            float positionX = _rigidbody.position.x;

            if (positionX <= _minBoundX && _direction < 0f)
                _direction = 1f;
            else if (positionX >= _maxBoundX && _direction > 0f)
                _direction = -1f;

            Move();
            Flip(_direction);
        }

        private void ChasePlayer()
        {
            if (_playerTransform == null)
                return;

            float offsetX = _playerTransform.position.x - _rigidbody.position.x;

            if (Mathf.Abs(offsetX) < 0.05f)
                _direction = 0f;
            else
                _direction = Mathf.Sign(offsetX);

            Move();
            Flip(_direction);
        }

        private void Move()
        {
            _moveHorizontal = _direction * _moveSpeed;
            _moveVertical = _rigidbody.velocity.y;

            _rigidbody.velocity = new Vector2(_moveHorizontal, _moveVertical);
        }

        private void Flip(float lastFacingDirection)
        {
            if (lastFacingDirection >= 0)
                transform.localEulerAngles = Vector3.zero;
            else
                transform.localEulerAngles = new Vector3(0, -180, 0);
        }

        private void UpdateAnimator()
        {
            _animator.SetFloat(MoveHash, Mathf.Abs(_rigidbody.velocity.x));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_playerDetected)
                return;

            _direction = -_direction;
        }
    }
}
