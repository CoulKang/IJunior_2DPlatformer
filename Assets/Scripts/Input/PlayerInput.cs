using UnityEngine;

namespace IJuniorPlatformer
{
    public class PlayerInput : MonoBehaviour
    {
        private const string Horizontal = nameof(Horizontal);
        private const string Jump = nameof(Jump);

        private bool _jumpPressed;

        public Vector2 MoveDirection { get; private set; }

        public bool JumpPressed => _jumpPressed;

        private void Update()
        {
            float moveX = Input.GetAxisRaw(Horizontal);

            MoveDirection = new Vector2(moveX, 0f);

            if (Input.GetButtonDown(Jump))
                _jumpPressed = true;
        }

        public bool GetIsJump() => GetBoolAsTrigger(ref _jumpPressed);

        private bool GetBoolAsTrigger(ref bool value)
        {
            bool localValue = value;
            value = false;
            return localValue;
        }
    }
}
