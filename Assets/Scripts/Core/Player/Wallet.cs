using UnityEngine;

namespace IJuniorPlatformer
{
    public class Wallet : MonoBehaviour
    {

        private int _balance = 0;

        public void AddCoin()
        {
            _balance++;
        }
    }
}
