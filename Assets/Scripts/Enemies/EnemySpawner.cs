using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IJuniorPlatformer
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyMovement _enemyPrefab;
        [SerializeField] private int _count = 3;

        private BoxCollider2D _area;

        private void Awake()
        {
            if (_area == null)
                _area = GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
            for (int i = 0; i < _count; i++)
                Spawn();
        }

        private void Spawn()
        {
            Bounds bounds = _area.bounds;

            Vector2 spawnPosition = new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );

            EnemyMovement enemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);

            enemy.SetPatrolBounds(bounds.min.x, bounds.max.x);
        }
    }
}
