using System.Collections.Generic;
using UnityEngine;

namespace IJuniorPlatformer
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Combat : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _colliderAttack;
        [SerializeField] private float _distance;
        [SerializeField] private float _damage;
        [SerializeField] private ContactFilter2D _contactFilter;

        public void Attack()
        {
            Vector2 castDirection = transform.TransformDirection(Vector2.right);
            List<RaycastHit2D> raycastHits = new();

            _colliderAttack.Cast(castDirection, _contactFilter, raycastHits, _distance);

            foreach (var hit in raycastHits)
            {
                if (hit.transform.TryGetComponent(out IDamageable damageableTarget))
                    damageableTarget.TakeDamage(_damage);
            }
        }
    }
}