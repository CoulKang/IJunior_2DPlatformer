using System;
using UnityEngine;

namespace IJuniorPlatformer
{
    public class AnimEventCatcher : MonoBehaviour
    {
        public event Action AttackActivated;
        public event Action AttackFinished;

        public void OnAttackAnimationEnd()
        {
            AttackFinished?.Invoke();
        }

        public void OnAttackActivate()
        {
            AttackActivated?.Invoke();
        }
    }
}
