using System;
using UnityEngine;

namespace IJuniorPlatformer
{
    public class Coin : MonoBehaviour
    {
        public event Action<Coin> OnPicked;

        public void Pick() => OnPicked?.Invoke(this);
    }
}
