using System.Collections;
using UnityEngine;

namespace IJuniorPlatformer
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private Coin _prefab;
        [SerializeField] private CoinPool _pool;

        [Space(10)]
        [SerializeField] private float _spawnRadius;
        [SerializeField] private float _spawnHeight;
        [SerializeField] private float _repeatRate = 1f;

        private Coroutine _startCoroutine;

        private void Awake()
        {
            _pool.Initialize(_prefab, _spawnRadius, _spawnHeight);
        }

        private void Start()
        {
            if (_startCoroutine == null)
                _startCoroutine = StartCoroutine(Spawn(_repeatRate));
        }

        private IEnumerator Spawn(float delay)
        {
            var wait = new WaitForSeconds(delay);
            bool isWork = true;

            while (isWork)
            {
                SpawnCoin();

                yield return wait;
            }
        }

        private void SpawnCoin()
        {
            Coin coin = _pool.Get();

            Vector2 randomOffset = Random.insideUnitCircle * _spawnRadius;
            float spawnX = transform.position.x + randomOffset.x;

            coin.transform.position = new Vector2(spawnX, _spawnHeight);
            coin.Picked += OnCoinPicked;
        }

        private void OnCoinPicked(Coin coin)
        {
            coin.Picked -= OnCoinPicked;
            _pool.Release(coin);
        }
    }
}