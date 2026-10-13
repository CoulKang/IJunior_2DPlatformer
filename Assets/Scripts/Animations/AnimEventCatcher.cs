using System;
using UnityEngine;

namespace IJuniorPlatformer
{
    public class AnimEventCatcher : MonoBehaviour
    {
        public event Action ActivateAttack;
        public event Action AttackFinished;

        public void AttackAnimationEnd()
        {
            AttackFinished?.Invoke();
        }

        public void AttackActivate()
        {
            ActivateAttack?.Invoke();
        }
    }
}
