using UnityEngine;

namespace IJuniorPlatformer
{
    public class Patrol : MonoBehaviour
    {
        private float _minBoundX;
        private float _maxBoundX;
        private bool _hasBounds;

        public Vector2 Direction { get; private set; }

        public void SetPatrolBounds(float minX, float maxX)
        {
            _minBoundX = minX;
            _maxBoundX = maxX;
            _hasBounds = true;

            if (Random.value < 0.5f)
                Direction = new Vector2(-1f, 0);
            else
                Direction = new Vector2(1f, 0);
        }

        public void Tick()
        {
            if (_hasBounds == false)
                return;

            float positionX = transform.position.x;

            if (positionX >= _maxBoundX)
                Direction = new Vector2(-1f, 0);
            else if (positionX <= _minBoundX)
                Direction = new Vector2(1f, 0);
        }
    }
}
