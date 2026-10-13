using UnityEngine;

namespace IJuniorPlatformer
{
    public class MedicineKit : MonoBehaviour, IPickable
    {
        [SerializeField, Min(0f)] private float _healthScore = 5f;

        public float HealthScore => _healthScore;

        public void Pick()
        {
            Destroy(gameObject);
        }
    }
}
