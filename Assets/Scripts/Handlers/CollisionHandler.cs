using System;
using UnityEngine;

namespace IJuniorPlatformer
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class CollisionHandler : MonoBehaviour
    {
        public event Action<Collider2D> TriggerEntered;
        public event Action<Collider2D> TriggerLeft;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            TriggerEntered?.Invoke(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            TriggerLeft?.Invoke(collision);
        }
    }
}
