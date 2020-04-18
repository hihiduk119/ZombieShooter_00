﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class MeleeAttackModule : ICharacterAttackModule
    {
        //공격 시작 플레그
        private bool attackStart = false;
        public bool AttackStart { get => attackStart; set => attackStart = value; }

        private float attackDelay = 0;
        private float attackDeltaTime = 0;
        private bool hitStart = false;
        private float hitDeltaTime = 0;
        private float hitDelay = 0;
        private IHaveHit haveHit;
        private IHaveHealth haveHealth;
        private int damage;

        //private UnityEvent _hitEvent = new UnityEvent();
        //public UnityEvent HitEvent { get => _hitEvent; set => throw new System.NotImplementedException(); }

        /// <summary>
        /// 근접 공격 세팅
        /// </summary>
        /// <param name="attackDelay">공격 후 다음 공격간의 딜레이</param>
        /// <param name="hitDelay">공격실행 후 실제 공격 까지의 딜레이</param>
        public MeleeAttackModule(MonsterSettings monsterSettings , IHaveHit haveHit, IHaveHealth haveHealth) {
            //몬스터 데이터 세팅
            this.attackDelay = monsterSettings.AttackDelay;
            this.hitDelay = monsterSettings.HitDelay;
            this.damage = monsterSettings.Damage;

            //인터페이스 세팅
            this.haveHit = haveHit;
            this.haveHealth = haveHealth;

            //[중요]처음 시작시 바로 공격을 해야하기에 attackDelay값과 동일하게 마춰줌
            attackDeltaTime = attackDelay;
        }

        public void Attack(ICharacterAnimatorModule characterAnimatorModule)
        {
            //공격이 시작되면 호출
            if (attackStart)
            {
                //공격을 바로 할려고 값을 일부러 넣어줌
                DoAttack(characterAnimatorModule);

                //Debug.Log("attackDelay = " + this.attackDelay + "  this.hitDelay = " + this.hitDelay + "    this.damage = " + this.damage);
            }
        }

        /// <summary>
        /// 몬스터 공격 시작
        /// </summary>
        public void DoAttack(ICharacterAnimatorModule characterAnimatorModule)
        {
            attackDeltaTime += Time.deltaTime;
            hitDeltaTime += Time.deltaTime;
            //Debug.Log("attackDeltaTime = " + attackDeltaTime + "         attackDelay = " + attackDelay);

            if (attackDeltaTime > attackDelay)
            {
                //Debug.Log("attack s");
                characterAnimatorModule.Attack();

                attackDeltaTime = 0;
                hitDeltaTime = 0;
                //공격이 시작되면 바리케이트 맞는 연출 활성화.
                hitStart = true;
            }

            if (hitStart)
            {
                //Debug.Log("Hit !!");
                //_hitEvent.Invoke();
                Hit();
            }
        }

        /// <summary>
        /// 실제 공격
        /// </summary>
        public void Hit()
        {
            //Debug.Log("hitDeltaTime = " + hitDeltaTime + "         hitDelay = " + hitDelay);
            if (hitDeltaTime > hitDelay)
            {
                //Debug.Log("hit s");
                //바리케이트에 히트 호출
                haveHit.Hit();

                hitDeltaTime = 0;

                //베리어에 데미지 이벤트 호출
                if (haveHealth != null)
                {
                    haveHealth.DamagedEvent.Invoke(this.damage, Vector3.zero);
                }

                hitStart = false;
            }
        }
    }
}
