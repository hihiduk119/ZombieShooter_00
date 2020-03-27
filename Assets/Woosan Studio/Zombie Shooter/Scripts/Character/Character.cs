using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 몬스터의 시작점이나 문제가 있음
    /// 모든 플레이어 공용으로 사용하려 하였으나 정의가 재대로 안돼서 일단
    /// 몬스터용으로만 사용
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

            //처음부터 미리 생각해서 만들다 보면 나중에 기억나지 않아 문제가 된다.
            //차라리 일단 만들고 리팩토링 한면서 코드수정을 하는게 더 나을 수 있다.
            /*
            //FSM 세팅 생성
            FSM = characterSettings.UseAi ?
                new MonsterFSM() as IFiniteStateMachine :
                new PlayerFSM() as IFiniteStateMachine;

            //입력부분 세팅 생성
            characterInput = characterSettings.UseAi ?
                new AiInput() as ICharacterInput :
                new ControllerInput() as ICharacterInput;
            */

            //FSM 세팅 생성
            FSM = new MonsterFSM();

            //입력부분 세팅 생성
            characterInput = new AiInput();

            //움직임부분 세팅 생성
            UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            characterDrivingModule = characterSettings.UseAi ?
                new AiDrivingModule(agent, transform, target, characterSettings) as ICharacterDrivingModule :
                new PlayerDrivingModule(characterInput, transform, characterSettings) as ICharacterDrivingModule;
                

            //에니메이션부분 세팅 생성
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
