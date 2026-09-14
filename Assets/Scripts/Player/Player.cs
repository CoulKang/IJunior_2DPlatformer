using UnityEngine;

namespace IJuniorPlatformer
{
    [RequireComponent (typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
    public class Player : MonoBehaviour 
    {
        private Wallet _wallet;

        private void Awake()
        {
            if ( _wallet == null )
                _wallet = GetComponent<Wallet>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Coin>() != null)
                _wallet.AddCoin();
        }
    }
}
