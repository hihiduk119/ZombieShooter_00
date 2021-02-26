using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using WoosanStudio.ZombieShooter.Character;

namespace WoosanStudio.ZombieShooter
{
    public class ZombieFSM : MonoBehaviour ,IFiniteStateMachine ,  IProjectileLauncher
    {
        //ICharacterInput characterInput;
        ICharacterDrivingModule characterDrivingModule;
        ICharacterAnimatorModule characterAnimatorModule;
        ICharacterAttackModule characterAttackModule;

        #region [-IProjectileLauncher Implement]
        public ProjectileLauncher ProjectileLauncher { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public UnityEvent TriggerEvent { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void Fire() { throw new System.NotImplementedException(); }
        public void Stop() { throw new System.NotImplementedException(); }

        //FSM 코루틴용 상태
        public enum State
        {
            Idle,       //아무것도 안하는 기본 상태
            Tracking,   //플레이어 추적 상태
            Attacking,  //플레이어 공격 상태
        }

        //FSM 코루틴용 상태
        public State FSM_State = State.Idle;

        //FSM 프리퀀시
        private WaitForSeconds WFS = new WaitForSeconds(0.1f);

        //플레이어
        private Transform target;

        public IProjectileLauncherEvents GetProjectileLauncherEvents()
        {
            throw new System.NotImplementedException();
        }

        public void ReloadAmmo()
        {
            throw new System.NotImplementedException();
        }
        #endregion


        #region [-IProjectileLauncher Implement]
        //플레이어 와 중첩으로 사용되어 임시로 하나 선언
        public void SetFSM(Transform target, ICharacterDrivingModule characterDrivingModule, ICharacterAnimatorModule characterAnimatorModule, ICharacterAttackModule characterAttackModule, PlayerConfig playerConfig) { throw new System.NotImplementedException(); }

        public void SetFSM(Transform target, ICharacterDrivingModule characterDrivingModule, ICharacterAnimatorModule characterAnimatorModule , ICharacterAttackModule characterAttackModule)
        {
            //플레이어 세팅
            this.target = target;

            //this.characterInput = characterInput;
            this.characterDrivingModule = characterDrivingModule;
            this.characterAnimatorModule = characterAnimatorModule;
            this.characterAttackModule = characterAttackModule;

            //this.monsterSettings = monsterSettings;

            //공격이 시작되었음을 등록
            //[중요]ICharacterAttackModule 에 공격 시작을 알림 등록
            characterDrivingModule.ReachDestinationEvent.AddListener(() => {
                characterAttackModule.AttackStart = true;
            });

            //FSM여기서 시작
            StartCoroutine(FSM_State.ToString());
        }

        //Update와 같은 역활.
        //각 모듈의 구동부 호출.
        //
        public void Tick()
        {
            //AI 인풋 컨트롤 호출
            //AI 불필요 하여 제거
            //characterInput?.ReadInput();

            /*
            //움직임 모듈 호출
            characterDrivingModule?.Tick();

            if (characterAnimatorModule == null) Debug.Log("CharacterAnimatorModule null 0");
            if (characterDrivingModule == null) Debug.Log("CharacterDrivingModule null 1");

            //에니메이션 모듈 호출
            //드라이빙 모듈에서 움직임 상태일 때만 움직임 에니메이션 호출
            if (characterDrivingModule.State == DrivingState.Move) { characterAnimatorModule?.Move(characterDrivingModule.Speed); }
            
            //공격 모듈 호출
            characterAttackModule?.Attack(characterAnimatorModule);

            */
        }

        /// <summary>
        /// 아무것도 안함
        /// </summary>
        /// <returns></returns>
        IEnumerator Idle()
        {
            while(true)
            {
                //if()

                Debug.Log("Idle");
                yield return WFS;
            }

            //다음 상태 전환
            StartCoroutine(FSM_State.ToString());
        }

        /// <summary>
        /// 플레이어 추적
        /// </summary>
        /// <returns></returns>
        IEnumerator Tracking()
        {
            while (true)
            {
                if (characterAnimatorModule == null) Debug.Log("CharacterAnimatorModule null 0");
                if (characterDrivingModule == null) Debug.Log("CharacterDrivingModule null 1");

                //에니메이션 모듈 호출
                //드라이빙 모듈에서 움직임 상태일 때만 움직임 에니메이션 호출
                if (characterDrivingModule.State == DrivingState.Move) { characterAnimatorModule?.Move(characterDrivingModule.Speed); }

                yield return WFS;
            }

            //다음 상태 전환
            StartCoroutine(FSM_State.ToString());
        }

        /// <summary>
        /// 공격 상태
        /// </summary>
        /// <returns></returns>
        IEnumerator Attacking()
        {
            while (true)
            {
                //공격 모듈 호출
                characterAttackModule?.Attack(characterAnimatorModule);

                yield return WFS;
            }

            //다음 상태 전환
            StartCoroutine(FSM_State.ToString());
        }


        public void UseAmmo()
        {
            throw new System.NotImplementedException();
        }

        //public ICharacterInput GetCharacterInput()
        //{
        //    return this.characterInput;
        //}

        public ICharacterDrivingModule GetCharacterDrivingModule()
        {
            return this.characterDrivingModule;
        }

        public ICharacterAnimatorModule GetCharacterAnimatorModule()
        {
            return this.characterAnimatorModule;
        }

        public ICharacterAttackModule GetCharacterAttackModule()
        {
            return this.characterAttackModule;
        }
        #endregion
    }
}
