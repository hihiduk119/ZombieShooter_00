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
        Transform target;

        //공격 시작 플레그
        private bool attackStart = false;
        private float attackDeltaTime = 0;
        private float attackDelay = 2.5f;

        private bool hitStart = false;
        private float hitDeltaTime = 0;
        private float hitDelay = 0.75f;

        public void SetFSM(Transform target, ICharacterInput characterInput,ICharacterDrivingModule characterDrivingModule, ICharacterAnimatorModule characterAnimatorModule)
        {
            this.characterInput = characterInput;
            this.characterDrivingModule = characterDrivingModule;
            this.characterAnimatorModule = characterAnimatorModule;


            //공격이 시작되었음을 등록
            characterDrivingModule.ReachDestinationEvent.AddListener(() => { attackStart = true;});

            this.target = target;
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

            if (hitStart)
            {
                //공격을 바로 할려고 값을 일부러 넣어줌
                Hit();
            }
        }

        /// <summary>
        /// 뫁스터 공격 시작
        /// </summary>
        public void Attack()
        {
            attackDeltaTime += Time.deltaTime;
            if(attackDeltaTime > attackDelay)
            {
                Debug.Log("attack s");
                //목적지 도달시 공격 모션을 수행하기 위해 해당 이벤트 세팅
                characterDrivingModule.ReachDestinationEvent.AddListener(characterAnimatorModule.Attack);

                hitStart = true;

                attackDeltaTime = 0;
            }
        }

        public void Hit()
        {
            hitDeltaTime += Time.deltaTime;

            if (hitDeltaTime > hitDelay)
            {
                Debug.Log("hit s");
                //바리케이트에 히트 호출
                target.GetComponent<IHaveHit>().Hit();

                hitDeltaTime = 0;
                hitStart = false;
            }
        }
    }
}
