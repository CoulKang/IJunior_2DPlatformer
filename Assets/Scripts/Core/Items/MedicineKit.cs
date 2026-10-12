using UnityEngine;

namespace IJuniorPlatformer
{
    public class MedicineKit : MonoBehaviour, IPickable
    {
        [SerializeField, Min(0f)] private float _healthScore = 5f;

        public void Pick(Player picker)
        {
            if (picker == null)
                return;

            if (picker.Health.IsFull)
                return;

            picker.Health.Recover(_healthScore);

            Destroy(gameObject);
        }
    }
}
