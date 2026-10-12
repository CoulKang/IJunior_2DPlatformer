using UnityEngine;

namespace IJuniorPlatformer
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _currentHealth;

        public bool IsAlive { get { return _currentHealth > 0f; } }

        public bool IsFull { get { return _currentHealth >= _maxHealth; } }

        private void OnValidate()
        {
            if (_currentHealth > _maxHealth)
                _currentHealth = _maxHealth;
        }

        public void Deduct(float totalDamage)
        {
            _currentHealth -= totalDamage;

            if (_currentHealth < 0f)
                _currentHealth = 0f;
        }

        public void Recover(float health)
        {
            _currentHealth += health;

            if (_currentHealth > _maxHealth)
                _currentHealth = _maxHealth;
        }
    }
}
