using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class MonsterFSM : IFiniteStateMachine
    {
        ICharacterInput characterInput;
        ICharacterDrivingModule characterDrivingModule;
        ICharacterAnimatorModule characterAnimatorModule;
        MonsterSettings monsterSettings;
        IHaveHealth haveHealth;

        Transform target;

        //공격 시작 플레그
        private bool attackStart = false;
        private float attackDelay = 2f;
        private float attackDeltaTime = 0; 
        private bool hitStart = false;
        private float hitDeltaTime = 0;
        private float hitDelay = 0.75f;


        //플레이어 와 중첩으로 사용되어 임시로 하나 선언
        public void SetFSM(Transform target, ICharacterInput characterInput, ICharacterDrivingModule characterDrivingModule, ICharacterAnimatorModule characterAnimatorModule, PlayerConfig playerConfig) { throw new System.NotImplementedException(); }

        public void SetFSM(Transform target, ICharacterInput characterInput,ICharacterDrivingModule characterDrivingModule, ICharacterAnimatorModule characterAnimatorModule , MonsterSettings monsterSettings)
        {
            this.characterInput = characterInput;
            this.characterDrivingModule = characterDrivingModule;
            this.characterAnimatorModule = characterAnimatorModule;
            this.monsterSettings = monsterSettings;

            //공격이 시작되었음을 등록
            characterDrivingModule.ReachDestinationEvent.AddListener(() => {
                attackStart = true;
            });

            //목적지 도달시 공격 모션을 수행하기 위해 해당 이벤트 세팅
            //characterDrivingModule.ReachDestinationEvent.AddListener(characterAnimatorModule.Attack);

            this.target = target;

            //몬스터 데이터를 실제 공격 부분과 일치하게 세팅함.
            attackDelay = monsterSettings.AttackDelay;
            
            //처음 시작시 바로 공격을 해야하기에 attackDelay값과 동일하게 마춰줌
            attackDeltaTime = attackDelay;

            //타겟의 체력 가져오기. 베리어의 체력
            haveHealth = target.GetComponent<IHaveHealth>();
        }

        //Update와 같은 역활.
        public void Tick()
        {
            if (characterInput != null) { characterInput.ReadInput(); }
            if (characterDrivingModule != null) { characterDrivingModule.Tick(); }
            if (characterAnimatorModule != null) { characterAnimatorModule.Move(characterDrivingModule.Speed); }

            //공격이 시작되면 호출
            if (attackStart)
            {
                //공격을 바로 할려고 값을 일부러 넣어줌
                Attack();
            }

            //if (hitStart)
            //{
            //    //공격을 바로 할려고 값을 일부러 넣어줌
            //    Hit();
            //}
        }

        /// <summary>
        /// 뫁스터 공격 시작
        /// </summary>
        public void Attack()
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
                Hit();
            }
            
        }

        public void Hit()
        {
            //Debug.Log("hitDeltaTime = " + hitDeltaTime + "         hitDelay = " + hitDelay);
            if (hitDeltaTime > hitDelay)
            {
                //Debug.Log("hit s");
                //바리케이트에 히트 호출
                target.GetComponent<IHaveHit>().Hit();

                hitDeltaTime = 0;

                //베리어에 데미지 이벤트 호출
                if(haveHealth != null)
                {
                    haveHealth.DamagedEvent.Invoke(this.monsterSettings.Damage, Vector3.zero);
                }

                hitStart = false;
            }
        }
    }
}
