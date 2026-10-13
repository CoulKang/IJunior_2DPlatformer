using UnityEngine;

namespace IJuniorPlatformer
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _maxScore;
        [SerializeField] private float _currentScore;

        public bool IsAlive => _currentScore > 0f;

        public bool IsFull => _currentScore >= _maxScore;

        private void OnValidate()
        {
            if (_currentScore > _maxScore)
                _currentScore = _maxScore;
        }

        public void Deduct(float totalDamage)
        {
            _currentScore -= totalDamage;

            if (_currentScore < 0f)
                _currentScore = 0f;
        }

        public void Recover(float score)
        {
            _currentScore += score;

            if (_currentScore > _maxScore)
                _currentScore = _maxScore;
        }
    }
}
