using System;
using UnityEngine;

namespace IJuniorPlatformer
{
    [RequireComponent (typeof(CapsuleCollider2D))]
    public class CoinBehaviour : MonoBehaviour
    {
        public event Action<CoinBehaviour> OnPicked;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>() == null)
                return;

            OnPicked?.Invoke(this);
        }
    }
}
