using UnityEngine;

namespace IJuniorPlatformer
{
    public class GroundSensor : MonoBehaviour
    {
        [SerializeField, Min(0.01f)] private float _radius = 0.15f;
        [SerializeField] private LayerMask _groundMask;

        [SerializeField] public bool IsGrounded;

        public void Tick()
        {
            IsGrounded = Physics2D.OverlapCircle(transform.position, _radius, _groundMask);
        }
    }
}
