using UnityEngine;

namespace IJuniorPlatformer
{
    public class PlayerInput : MonoBehaviour
    {
        private const string Horizontal = nameof(Horizontal);
        private const string Jump = nameof(Jump);
        private const string Attack = nameof(Attack);

        private bool _jumpPressed;
        private bool _attackPressed;

        public Vector2 MoveDirection { get; private set; }

        public bool JumpPressed => _jumpPressed;

        public bool AttackPressed => _attackPressed;

        private void Update()
        {
            float moveX = Input.GetAxisRaw(Horizontal);

            MoveDirection = new Vector2(moveX, 0f);

            if (Input.GetButtonDown(Jump))
                _jumpPressed = true;

            if (Input.GetButtonDown(Attack))
                _attackPressed = true;
        }

        public bool GetIsJump() => GetBoolAsTrigger(ref _jumpPressed);

        public bool GetIsAttack() => GetBoolAsTrigger(ref _attackPressed);

        private bool GetBoolAsTrigger(ref bool value)
        {
            bool localValue = value;
            value = false;
            return localValue;
        }
    }
}
