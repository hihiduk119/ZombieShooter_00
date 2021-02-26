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
            //Die,        //*죽음 상태 -> 미구현
        }

        //FSM 코루틴용 상태
        public State FSM_State = State.Idle;

        //FSM 프리퀀시
        private WaitForSeconds WFS = new WaitForSeconds(0.1f);
        private WaitForSeconds WFS_ForFindTarget = new WaitForSeconds(0.2f);
        private WaitForEndOfFrame WFEF = new WaitForEndOfFrame();

        //플레이어
        private Transform target;
        public Transform Target { get => target; set => target = value; }

        //죽었는지 살았는지 확인용
        public bool IsDead = false;

        //캐쉬용
        private Coroutine seekCoroutine;
        private Coroutine FSM_Coroutine;

        public IProjectileLauncherEvents GetProjectileLauncherEvents()
        {
            throw new System.NotImplementedException();
        }

        public void ReloadAmmo()
        {
            throw new System.NotImplementedException();
        }
        #endregion

        /// <summary>
        /// 플레이어 타겟 찾기
        /// </summary>
        /// <returns></returns>
        IEnumerator SeekCoroutine(string tag)
        {
            //임시 타겟
            GameObject findTarget;

            while (true)
            {
                //0.25초 단위로 타겟 찾기.
                findTarget = GameObject.FindGameObjectWithTag(tag);

                //태그를 못찾았으면 타겟 및 목표 null
                if (findTarget == null)
                {
                    //Debug.Log("타겟 없음...");
                    //강제로 FSM 타겟에 null 넣음
                    GetCharacterDrivingModule().Destination = null;
                    this.target = null;
                }
                else //태그를 찾았으면 타겟 및 목표 재설정
                {
                    //Debug.Log("타겟 찾음!!!");
                    this.target = findTarget.transform;
                    //FSM 새팅이 되어야만 설정
                    
                    GetCharacterDrivingModule().Destination = this.target;
                    
                }

                yield return WFS_ForFindTarget;
            }
        }


        #region [-IProjectileLauncher Implement]
        //플레이어 와 중첩으로 사용되어 임시로 하나 선언
        public void SetFSM(string tag, ICharacterDrivingModule characterDrivingModule, ICharacterAnimatorModule characterAnimatorModule, ICharacterAttackModule characterAttackModule, PlayerConfig playerConfig) { throw new System.NotImplementedException(); }

        public void SetFSM(string tag, ICharacterDrivingModule characterDrivingModule, ICharacterAnimatorModule characterAnimatorModule , ICharacterAttackModule characterAttackModule)
        {
            //플레이어 세팅
            //this.target = target;

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

            //플레이어 지속적으로 찾음
            seekCoroutine = StartCoroutine(SeekCoroutine(tag));
        }

        /*
        //Update와 같은 역활.
        //각 모듈의 구동부 호출.
        public void Tick()
        {
            //AI 인풋 컨트롤 호출
            //AI 불필요 하여 제거
            //characterInput?.ReadInput();

            
            //움직임 모듈 호출
            characterDrivingModule?.Tick();

            if (characterAnimatorModule == null) Debug.Log("CharacterAnimatorModule null 0");
            if (characterDrivingModule == null) Debug.Log("CharacterDrivingModule null 1");

            //에니메이션 모듈 호출
            //드라이빙 모듈에서 움직임 상태일 때만 움직임 에니메이션 호출
            if (characterDrivingModule.State == DrivingState.Move) { characterAnimatorModule?.Move(characterDrivingModule.Speed); }

            //공격 모듈 호출
            characterAttackModule?.Attack(characterAnimatorModule);  
        }
        */

        /// <summary>
        /// 서있음
        /// </summary>
        /// <returns></returns>
        IEnumerator Idle()
        {
            //Debug.Log("[대기중]");
            //캐릭터 이동 정지
            this.characterDrivingModule.Stop();
            //아이들 애니메이션 상태 실행
            this.characterAnimatorModule.Idle();

            while (true)
            {
                yield return WFS;

                //플레이어 존제시 추적 실행
                if (this.target != null)
                {
                    //Debug.Log("[타겟 확인 추적 실행]");
                    FSM_State = State.Tracking;

                    //다음 상태 전환
                    FSM_Coroutine = StartCoroutine(FSM_State.ToString());
                    yield break;
                }
            } 
        }

        /// <summary>
        /// 플레이어 추적
        /// </summary>
        /// <returns></returns>
        IEnumerator Tracking()
        {
            //캐릭터 이동 실행
            this.characterDrivingModule.Run();
            //추적 실행
            //Debug.Log("[추적 실행 중]");
            characterDrivingModule.Destination = this.target;
            //속도 설정
            characterAnimatorModule.Move(characterDrivingModule.Speed);

            while (true)
            {
                //플레이어가 없다면 idle상태로 변경
                if (this.target == null)
                {
                    //Debug.Log("[Tracking => Idle상태로 이동]");

                    //대기 상태로 이동
                    FSM_State = State.Idle;
                    FSM_Coroutine = StartCoroutine(FSM_State.ToString());
                    yield break;
                }

                //플레이어와 거리 체크
                characterDrivingModule.Tick();

                //거리가 공격 가능 거리면 공격상태로 변경
                if (characterDrivingModule.State == DrivingState.Stand)
                {
                    //대기 상태로 이동
                    FSM_State = State.Attacking;
                    FSM_Coroutine = StartCoroutine(FSM_State.ToString());
                    yield break;
                }

                

                yield return WFS;
            }
        }

        /// <summary>
        /// 공격 상태
        /// </summary>
        /// <returns></returns>
        IEnumerator Attacking()
        {
            while (true)
            {
                //플레이어가 없다면 idle상태로 변경
                if (this.target == null)
                {
                    //Debug.Log("[Attacking => Idle상태로 이동]");

                    //대기 상태로 이동
                    FSM_State = State.Idle;
                    FSM_Coroutine = StartCoroutine(FSM_State.ToString());
                    yield break;
                }

                //플레이어 거리 체크
                characterDrivingModule.Tick();

                //공격이 완전히 끝났고 공격 거리가 멀면 추적상태로 변경
                if (characterDrivingModule.State == DrivingState.Move )
                {
                    characterAnimatorModule.Idle();

                    characterAttackModule.AttackStart = false;

                    //추적 상태로 이동
                    FSM_State = State.Tracking;
                    FSM_Coroutine = StartCoroutine(FSM_State.ToString());
                    yield break;
                }

                //공격 모듈 호출
                characterAttackModule?.Attack(characterAnimatorModule);

                yield return WFEF;
            }
        }

        /// <summary>
        /// FSM 종료
        /// 코루틴 종료
        /// </summary>
        public void Shotdown()
        {
            StopCoroutine(FSM_Coroutine);
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
