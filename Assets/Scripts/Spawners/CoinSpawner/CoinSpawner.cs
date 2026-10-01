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
                _pool.GetCoin();

                yield return wait;
            }
        }
    }
}