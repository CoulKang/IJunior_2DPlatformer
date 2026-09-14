using UnityEngine;

namespace IJuniorPlatformer
{
    public class PlayerMovement : MonoBehaviour
    {
        private const string Horizontal = nameof(Horizontal);
        private const string Jump = nameof(Jump);

        private readonly int MoveHash = Animator.StringToHash("Move");
        private readonly int JumpHash = Animator.StringToHash("Jumping");

        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rigidbody;

        [Header("Movement")]
        [SerializeField] private float _moveSpeed = 7f;
        [SerializeField] private float _jumpForce = 12f;

        [Space(20)]
        [SerializeField] private Transform _groundCheck;

        [Space(1)]
        [SerializeField] private float _groundRadius = 0.2f;
        [SerializeField] private LayerMask _groundLayer;

        private float _moveHorizontal;
        private float _moveVertical;
        private float _moveInput;
        private float _lastFacingDirection;
        private bool _isGrounded;

        private void Update()
        {
            _moveHorizontal = GetMove();
            _moveVertical = GetJump();

            _rigidbody.velocity = new Vector2(_moveHorizontal, _moveVertical);

            Flip(_lastFacingDirection);
            UpdateAnimator();
        }

        private void FixedUpdate()
        {
            _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundRadius, _groundLayer);
        }

        private float GetMove()
        {
            _moveInput = Input.GetAxisRaw(Horizontal);

            if (_moveInput != 0)
            {
                _lastFacingDirection = Mathf.Sign(_moveInput);
                return _moveInput * _moveSpeed;
            }

            return 0f;
        }

        private float GetJump()
        {
            if (Input.GetButtonDown(Jump) && _isGrounded)
            {
                return _jumpForce;
            }

            return _rigidbody.velocity.y;
        }

        private void Flip(float lastFacingDirection)
        {
            if (lastFacingDirection >= 0)
            {
                transform.localEulerAngles = Vector3.zero;
            }
            else
            {
                transform.localEulerAngles = new Vector3(0, -180, 0);
            }
        }

        private void UpdateAnimator()
        {
            _animator.SetBool(JumpHash, _isGrounded);
            _animator.SetFloat(MoveHash, Mathf.Abs(_rigidbody.velocity.x));
        }
    }
}
