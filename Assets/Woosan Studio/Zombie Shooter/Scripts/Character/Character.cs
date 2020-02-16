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
    public class Character : MonoBehaviour
    {
        [SerializeField] public CharacterSettings characterSettings;
        public Transform target;

        //캐릭터의 입력 부분.
        private ICharacterInput characterInput;
        //캐릭터를 해당 입력에 의해 움직이는 부분.
        private ICharacterDrivingModule characterDrivingModule;
        //캐릭터의 에니메이션을 조작하는 부분
        private ICharacterAnimatorModule characterAnimatorModule;

        private void Awake()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;

            if (target == null)
            {
                Debug.Log("[error] Player Tag is null!! ");
                return;
            }

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

            

            //Debug.Log("name = " + target.ToString());
        }

        private void Update()
        {
            if (target == null) {
                Debug.Log("[Player Tag is null] ");
                return;
            }

            characterInput.ReadInput();
            characterDrivingModule.Tick();
            characterAnimatorModule.Move(characterDrivingModule.Speed);
        }
    }
}
