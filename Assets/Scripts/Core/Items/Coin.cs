using System;
using UnityEngine;

namespace IJuniorPlatformer
{
    public class Coin : MonoBehaviour, IPickable
    {
        public event Action<Coin> Picked;

        public void Pick(Player picker)
        {
            Picked?.Invoke(this);

            picker.Wallet.AddCoin();
        }
    }
}
