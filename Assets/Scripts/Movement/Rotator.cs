using UnityEngine;

namespace IJuniorPlatformer
{
    public class Rotator : MonoBehaviour
    {
        public void Tick(Vector2 direction)
        {
            if (Mathf.Abs(direction.x) < 0.01f) 
                return;

            if (direction.x >= 0f)
                transform.localEulerAngles = Vector3.zero;
            else
                transform.localEulerAngles = new Vector3(0, -180, 0);
        }
    }
}
