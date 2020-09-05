using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class MeleeAttackModule : ICharacterAttackModule
    {
        #region [-ICharacterAttackModule Implement]
        //공격 시작 플레그
        private bool attackStart = false;
        public bool AttackStart { get => attackStart; set => attackStart = value; }

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
        #endregion

        //공격과 공격 사이의 딜레이
        private float attackDelay = 0;
        //실제 시간
        private float attackDeltaTime = 0;
        //공격이 시작됬음을 알림
        //private bool hitStart = false;
        //공격이 시작과 실제 때림 발생 사이의 간격
        private float hitDelay = 0;
        //실제시간
        private float hitDeltaTime = 0;
        //공격 데미지
        private int damage;

        private IHaveHit haveHit;
        private IHaveHealth haveHealth;
        

        /// <summary>
        /// 근접 공격 세팅
        /// </summary>
        /// <param name="attackDelay">공격 후 다음 공격간의 딜레이</param>
        /// <param name="hitDelay">공격실행 후 실제 공격 까지의 딜레이</param>
        public MeleeAttackModule(MonsterSettings monsterSettings , IHaveHit haveHit, IHaveHealth haveHealth, ref UnityEvent attackEndEvent) {
            //몬스터 데이터 세팅
            this.attackDelay = monsterSettings.AttackDelay;
            this.damage = monsterSettings.Damage;

            //인터페이스 세팅
            this.haveHit = haveHit;
            this.haveHealth = haveHealth;

            //[중요]처음 시작시 바로 공격을 해야하기에 attackDelay값과 동일하게 마춰줌
            attackDeltaTime = attackDelay;

            //실제 공격 부분 연결
            //*실제 에니메이션에 이벤트로 연결 되어 있음을 기억하라
            attackEndEvent.AddListener(Hit);
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
                
            }
        }

        /// <summary>
        /// 실제 공격
        /// </summary>
        public void Hit()
        {
            haveHit.Hit();

            //베리어에 데미지 이벤트 호출
            if (haveHealth != null)
            {
                haveHealth.DamagedEvent.Invoke(this.damage, Vector3.zero);
                Debug.Log("공격 받음");
            }
        }
    }
}
