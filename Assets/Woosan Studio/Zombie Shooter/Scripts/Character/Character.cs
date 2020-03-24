using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 모든 캐릭터의 시작점.
    /// Player도 될수 있고 AI 도 될수 있다.
    /// 
    /// </summary>
    public class Character : MonoBehaviour , IHaveHit ,  ICanDestory
    {
        [SerializeField] public CharacterSettings characterSettings;
        public Transform target;

        //캐릭터 조증을 위한 유한상태기게
        private IFiniteStateMachine FSM;
        //캐릭터의 입력 부분.
        private ICharacterInput characterInput;
        //캐릭터를 해당 입력에 의해 움직이는 부분.
        private ICharacterDrivingModule characterDrivingModule;
        //캐릭터의 에니메이션을 조작하는 부분
        private ICharacterAnimatorModule characterAnimatorModule;

        //죽었는지 살았는지 확인용
        public bool isDead = false;
        

        //데이지 연출용
        IBlink blink;

        private IEnumerator waitThenCallback(float time, System.Action callback)
        {
            yield return new WaitForSeconds(time);
            callback();
        }

        private void Awake()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;

            if (target == null)
            {
                Debug.Log("[error] Player Tag is null!! ");
                return;
            }

            //FSM 세팅
            FSM = characterSettings.UseAi ?
                new MonsterFSM() as IFiniteStateMachine :
                new PlayerFSM() as IFiniteStateMachine;

            //입력부분 세팅
            characterInput = characterSettings.UseAi ?
                new AiInput() as ICharacterInput :
                new ControllerInput() as ICharacterInput;
                

            //움직임부분 세팅 
            UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            characterDrivingModule = characterSettings.UseAi ?
                new AiDrivingModule(agent, transform, target, characterSettings) as ICharacterDrivingModule :
                new PlayerDrivingModule(characterInput, transform, characterSettings) as ICharacterDrivingModule;
                

            //에니메이션부분 세팅
            Animator animator = GetComponentInChildren<Animator>();
            characterAnimatorModule = new ZombieAnimatorModule(animator) as ICharacterAnimatorModule;

            //유한상태기계 세팅
            FSM.SetFSM(characterInput,characterDrivingModule, characterAnimatorModule);

            //데미지 연출용 블링크
            blink = transform.GetComponentInChildren<IBlink>();
        }

        private void Update()
        {
            if (isDead) return;

            if (target == null)
            {
                Debug.Log("[Player Tag is null] ");
                return;
            }

            FSM.Tick();
        }

        /// <summary>
        /// 데미지 받았을때 연출
        /// </summary>
        public void Hit()
        {
            //Debug.Log("hi");
            //빨갛게 깜빡임.
            if (blink != null && blink.myGameObject.activeSelf) { blink.Blink(); }
        }

        /// <summary>
        /// 자동 삭제
        /// </summary>
        /// <param name="deley">해당 시간만큼 딜레이</param>
        public void Destory(float deley)
        {
            StartCoroutine(waitThenCallback(deley, () => { Object.Destroy(this.gameObject); }));
        }
    }
}
