using UnityEngine;
using UnityEngine.Pool;

namespace IJuniorPlatformer
{
    public class CoinPool : MonoBehaviour
    {
        [Space(10)]
        [SerializeField, Min(0)] private int _poolCapacity;
        [SerializeField, Min(0)] private int _poolMaxSize;

        private float _spawnRadius;
        private float _spawnY;
        private Coin _prefab;
        private ObjectPool<Coin> _pool;

        public void Initialize(Coin prefab, float spawnRadius, float fixedSpawnY)
        {
            _prefab = prefab;
            _spawnRadius = spawnRadius;
            _spawnY = fixedSpawnY;

            _pool = new ObjectPool<Coin>(
                createFunc: () => Instantiate(_prefab),
                actionOnGet: (coin) => coin.gameObject.SetActive(true),
                actionOnRelease: (coin) => coin.gameObject.SetActive(false),
                actionOnDestroy: (coin) => Destroy(coin.gameObject),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize
            );
        }

        public Coin Get() => _pool.Get();

        public void Release(Coin coin) => _pool.Release(coin);
    }
}
