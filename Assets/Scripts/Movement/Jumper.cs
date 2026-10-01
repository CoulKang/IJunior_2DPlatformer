using UnityEngine;

namespace IJuniorPlatformer
{
    public class Jumper : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField, Min(0f)] private float _jumpForce = 12f;

        public void Tick(bool jumpPressed, bool isGrounded)
        {
            if (jumpPressed == false || isGrounded == false)
                return;

            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}
