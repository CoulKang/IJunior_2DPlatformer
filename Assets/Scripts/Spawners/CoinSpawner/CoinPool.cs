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
                actionOnGet: (coin) => Setup(coin),
                actionOnRelease: (coin) =>
                {
                    coin.OnPicked -= Release;
                    coin.gameObject.SetActive(false);
                },
                actionOnDestroy: (coin) => Destroy(coin.gameObject),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize
            );
        }

        public void GetCoin()
        {
            _pool.Get();
        }

        private void Release(Coin coin)
        {
            _pool.Release(coin);
        }

        private void Setup(Coin coin)
        {
            Vector3 randomOffset = Random.insideUnitSphere * _spawnRadius;
            float spawnX = transform.position.x + randomOffset.x;

            coin.transform.position = new Vector2(spawnX, _spawnY);

            coin.OnPicked += Release;

            coin.gameObject.SetActive(true);
        }
    }
}
