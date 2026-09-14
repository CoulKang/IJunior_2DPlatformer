using UnityEngine;

namespace IJuniorPlatformer
{
    public class Wallet : MonoBehaviour
    {
        [SerializeField, Min(0)] private int _amountCoins = 0;

        public void AddCoin()
        {
            _amountCoins++;
        }
    }
}
