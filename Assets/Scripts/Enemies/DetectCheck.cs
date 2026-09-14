using System;
using UnityEngine;

namespace IJuniorPlatformer
{
    public class DetectCheck : MonoBehaviour 
    {
        public event Action<Transform> PlayerDetected;
        public event Action PlayerLost;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsPlayer(collision) == false)
                return;

            PlayerDetected?.Invoke(collision.transform);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (IsPlayer(collision) == false)
                return;

            PlayerLost?.Invoke();
        }

        private bool IsPlayer(Collider2D collision)
        {
            return collision.GetComponent<Player>();
        }
    }
}
