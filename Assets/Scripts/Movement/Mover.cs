using UnityEngine;

namespace IJuniorPlatformer
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 7f;
        [SerializeField] private float _jumpForce = 9f;
        [SerializeField] private Rigidbody2D _rigidbody;

        public void Jump()
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        public void Move(float direction)
        {
            _rigidbody.velocity = new Vector2(_moveSpeed * direction, _rigidbody.velocity.y);
        }
    }
}
