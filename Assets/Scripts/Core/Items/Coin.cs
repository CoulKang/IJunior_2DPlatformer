using System;
using UnityEngine;

namespace IJuniorPlatformer
{
    public class Coin : MonoBehaviour
    {
        public event Action<Coin> Picked;

        public void Pick()
        {
            Picked?.Invoke(this);
        }
    }
}
