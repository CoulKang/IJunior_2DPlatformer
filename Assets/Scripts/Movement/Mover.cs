using UnityEngine;

namespace IJuniorPlatformer
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        [Space(5)]
        [SerializeField] private float _moveSpeed = 7f;

        public void Tick(Vector2 direction)
        {
            _rigidbody.velocity = new Vector2(direction.x * _moveSpeed, _rigidbody.velocity.y);
        }
    }
}
