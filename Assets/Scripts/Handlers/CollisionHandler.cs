using System;
using UnityEngine;

namespace IJuniorPlatformer
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class CollisionHandler : MonoBehaviour
    {
        public event Action<Collider2D> TriggerEntered;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            TriggerEntered?.Invoke(collision);
        }
    }
}
