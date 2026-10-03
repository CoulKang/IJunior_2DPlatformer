using UnityEngine;

namespace IJuniorPlatformer
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Enemy _prefab;
        [SerializeField, Min(1)] private int _count = 1;

        [SerializeField] private BoxCollider2D _zone;

        private void Start()
        {
            Spawn();
        }

        public void Spawn()
        {
            Bounds bounds = _zone.bounds;

            float minX = bounds.min.x;
            float maxX = bounds.max.x;
            float centerY = bounds.center.y;
            float step = bounds.size.x / (_count + 1);

            for (int i = 0; i < _count; i++)
            {
                float spawnX = minX + step * (i + 1);

                Vector2 spawnPosition = new Vector2(spawnX, centerY);
                Enemy enemy = Instantiate(_prefab, spawnPosition, Quaternion.identity);

                enemy.Initialize(minX, maxX);
            }
        }
    }
}
