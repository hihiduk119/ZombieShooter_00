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
            Debug.Log("몬스터 공격 AttackEnd");
            AttackEndEvent.Invoke();
        }

        //public void MeleeAttackEnd()
        //{
        //    Debug.Log("몬스터 공격 AttackEnd");
        //    AttackEndEvent.Invoke();
        //}
        //public void DrawAttackEnd()
        //{
        //    Debug.Log("몬스터 공격 AttackEnd");
        //    //AttackEndEvent.Invoke();
        //}
    }
}
