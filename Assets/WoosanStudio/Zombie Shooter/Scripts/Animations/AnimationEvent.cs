using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class AnimationEvent : MonoBehaviour
    {
        public UnityEvent AttackEndEvent = new UnityEvent();

        public void AttackEnd()
        {
            Debug.Log("AttackEnd");
            AttackEndEvent.Invoke();
        }
    }
}
