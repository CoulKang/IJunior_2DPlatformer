using System;
using UnityEngine;

namespace IJuniorPlatformer
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerDetector : MonoBehaviour
    {
        [SerializeField] private CollisionHandler _collisionHandler;

        public event Action<Transform> TargetDetected;
        public event Action TargetLost;

        private void OnEnable()
        {
            _collisionHandler.TriggerEntered += VerifyEnterTrigger;
            _collisionHandler.TriggerLeft += VerifyExitTrigger;
        }

        private void OnDisable()
        {
            _collisionHandler.TriggerEntered -= VerifyEnterTrigger;
            _collisionHandler.TriggerLeft -= VerifyExitTrigger;
        }

        private void VerifyEnterTrigger(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player _) == false)
                return;

            TargetDetected?.Invoke(collision.transform);
        }

        private void VerifyExitTrigger(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player _) == false)
                return;

            TargetLost?.Invoke();
        }
    }
}
