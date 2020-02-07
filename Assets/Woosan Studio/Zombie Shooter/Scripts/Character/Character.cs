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

        private void Awake()
        {
            characterInput = characterSettings.UseAi ?
                new AiInput() as ICharacterInput :
                new ControllerInput() as ICharacterInput;

            UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

            characterDrivingModule = characterSettings.UseAi ?
                new PlayerDrivingModule(characterInput, transform, characterSettings) as ICharacterDrivingModule :
                new AiDrivingModule(agent,transform, target, characterSettings) as ICharacterDrivingModule;

        }

        private void Update()
        {
            characterInput.ReadInput();
            characterDrivingModule.Tick();
        }
    }
}
