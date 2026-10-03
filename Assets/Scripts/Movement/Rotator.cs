using UnityEngine;

namespace IJuniorPlatformer
{
    public class Rotator : MonoBehaviour
    {
        private const float ActivationTreshold = 0.01f;

        private Quaternion _faceRight;
        private Quaternion _faceLeft;

        private void Awake()
        {
            _faceRight = Quaternion.identity;
            _faceLeft = Quaternion.Euler(0f, 180f, 0f);
        }

        public void Rotate(float direction)
        {
            if (Mathf.Abs(direction) < ActivationTreshold) 
                return;

            if (direction >= 0f)
                transform.localRotation = _faceRight;
            else
                transform.localRotation = _faceLeft;
        }
    }
}
