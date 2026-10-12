using UnityEngine;

namespace IJuniorPlatformer
{
    public class Chaser : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _attackRange = 1f;
        [SerializeField, Min(0f)] private float _stopDistance = 0.25f;

        private Transform _target;

        public float Direction { get; private set; }

        public bool HasTarget { get { return _target != null; } }

        public bool IsTargetInAttackRange { get; private set; }

        public void SetTarget(Transform target)
        {
            _target = target;
            IsTargetInAttackRange = false;
        }

        public void ClearTarget()
        {
            _target = null;
            Direction = 0f;
            IsTargetInAttackRange = false;
        }

        public void Activate()
        {
            if (_target == null)
            {
                Direction = 0f;
                IsTargetInAttackRange = false;
                return;
            }

            float offsetX = _target.position.x - transform.position.x;
            float distance = Mathf.Abs(offsetX);

            IsTargetInAttackRange = distance <= _attackRange;

            Direction = distance > _stopDistance ? Mathf.Sign(offsetX) : 0f;
        }
    }
}
