using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using WoosanStudio.Common;

using UnityStandardAssets.Characters.ThirdPerson;
using RootMotion.FinalIK;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 플레이어가 가진 모든 컴퍼턴트를 다 가지고 컨트롤함
    /// 1. 활성 비활성
    /// 2. 조준 됐으니 사격 통제 장치에 사격 할수있으면 하라 통지
    /// 3. 비 조준 상태니 사격 하지말라 명령
    /// </summary>
    public class PlayerController : MonoBehaviour , IHaveHit
    {
        //싱글톤 패턴 적용
        static public PlayerController Instance;
        //[Auto-Awake()]
        //움직임
        [Header("[(Auto->Awake())]")]
        public WoosanStudio.Player.Move Move;
        //에니메이션 -> 이건 따로 스크립트 만들어야 할듯
        [Header("[(Auto->Awake())]")]
        public Animator Animator;
        //PlayerMoveActor 와 연결된 실제 회전 움직임
        [Header("[(Auto->Awake())]")]
        public MyThirdPersonCharacter MyThirdPersonCharacter;
        //회전 및 조준
        [Header("[(Auto->Awake())]")]
        public PlayerMoveActor PlayerMoveActor;
        //데미지 UI텍스트
        [Header("[(Auto->Awake())]")]
        public TextDamageBridge TextDamageBridge;
        //데미지 UI체력바
        //[Header("[(Auto->Awake())]")]
        //public HealthBar HealthBar;
        //데미지,리로드 UI바
        //[Header("[(Auto->Awake())]")]
        //public List<HealthBar> PlayerBars;
        //실제 체력
        [Header("[(Auto->Awake())]")]
        public PlayerHit HaveHit;
        //조준시 팔다리몸 IK
        [Header("[(Auto->Awake())]")]
        public AimIK AimIK;
        //조준시 헤드 IK
        [Header("[(Auto->Awake())]")]
        public LookAtIK LookAtIK;
        //조준 IK들을 더부드럽게 움직이게 만들어주는 Aim컨트롤
        [Header("[(Auto->Awake())]")]
        public PlayerAimSwaper PlayerAimSwaper;
        //실제 사격 컨트롤
        //public FireController FireController;
        //교체 하려는 사격 컨트롤러
        [Header("[(Auto->Awake())]")]
        public AutoFireControlInputBasedOnGunSetting AutoFireControlInputBasedOnGunSetting;
        //포지션 재배체
        [Header("[(Auto->Awake())]")]
        public Positioner Positioner;
        //모델 변경
        [Header("[(Auto->Awake())]")]
        public CharacterModelController Model;
        //실제 조준 및 조준 해제 컨트롤 함
        [Header("[(Auto->Awake())]")]
        public LookAtAimedTarget LookAtAimedTarget;

        [Header("[타겟과 조준선이 정렬 됐는지 판별(Auto->Awake())]")]
        public RayCheck RayCheck;


        //LookAtAimedTarget 에서 가져온 Aim,Release 이벤트 인터페이스
        private IAim aim;
            

        void Awake()
        {
            //싱글톤 패턴 적용
            Instance = this;

            //생성과 동시에 자동 셋업
            Move = GetComponent<WoosanStudio.Player.Move>();
            Animator = GetComponent<Animator>();
            MyThirdPersonCharacter = GetComponent<MyThirdPersonCharacter>();
            PlayerMoveActor = GetComponent<PlayerMoveActor>();
            TextDamageBridge = GetComponent<TextDamageBridge>();
            //HealthBar = GetComponent<HealthBar>();
            //PlayerBars = new List<HealthBar>(GetComponents<HealthBar>());
            HaveHit = GetComponent<PlayerHit>();
            AimIK = GetComponent<AimIK>();
            LookAtIK = GetComponent<LookAtIK>();
            PlayerAimSwaper = GetComponent<PlayerAimSwaper>();
            //FireController = GetComponent<FireController>();
            AutoFireControlInputBasedOnGunSetting = GetComponent<AutoFireControlInputBasedOnGunSetting>();
            Positioner = GetComponent<Positioner>();
            Model = GetComponentInChildren<CharacterModelController>();
            LookAtAimedTarget = GetComponent<LookAtAimedTarget>();

            //조준 및 조준 해제 이벤트 연결
            //* LookAtAimedTarget.cs 조준 이벤트 발생시 사격 시작
            aim = (IAim)LookAtAimedTarget;
            //조준 이벤트 연결
            aim.AimEvent.AddListener(() => {
                //사격 컨트롤러 시작 이벤트 호출
                AutoFireControlInputBasedOnGunSetting.AimEvent.Invoke();
                
            });
            //조준 해제 이벤트 연결
            aim.ReleaseEvent.AddListener(() => {
                //사격 컨트롤러 중지 이벤트 호출
                AutoFireControlInputBasedOnGunSetting.ReleaseEvent.Invoke();
            });

            RayCheck = GetComponentInChildren<RayCheck>();
            //자동 사격 시스템의 조준선 정렬 플레그 와 레이 체커 Hit 플레그 연결
            RayCheck.RayHitEvent.AddListener(value => AutoFireControlInputBasedOnGunSetting.IsSightAlimentComplete = value);
        }

        /// <summary>
        /// 플레이어를 활성화 시킴
        /// </summary>
        public void Active()
        {
            Move.enabled = true;
            MyThirdPersonCharacter.enabled = true;
            PlayerMoveActor.enabled = true;
            //UI Health Bar 비활성화
            //HealthBar.HealthbarPrefab.gameObject.SetActive(true);
            //PlayerBars.ForEach(value => value.HealthbarPrefab.gameObject.SetActive(true));
            //FireController.enabled = true;

            Debug.Log("==[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "]==");
        }

        /// <summary>
        /// 플레이어 비활성화
        /// </summary>
        public void Deactive()
        {
            Move.enabled = false;
            MyThirdPersonCharacter.enabled = false;
            PlayerMoveActor.enabled = false;
            //UI Health Bar 비활성화
            //HealthBar.HealthbarPrefab.gameObject.SetActive(false);
            //PlayerBars.ForEach(value => value.HealthbarPrefab.gameObject.SetActive(false));
            AimIK.enabled = false;
            LookAtIK.enabled = false;
            //FireController.enabled = false;

            Debug.Log("==["+this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name+ "]==");
        }

        /// <summary>
        /// 타겟 조준
        /// </summary>
        //public void Aiming()
        //{
        //    AimIK.enabled = true;
        //    LookAtIK.enabled = true;
        //    PlayerAimSwaper.enabled = true;

        //    Debug.Log("Player.Aiming()");
        //}

        /// <summary>
        /// 조준 해제
        /// </summary>
        //public void Release()
        //{
        //    AimIK.enabled = false;
        //    LookAtIK.enabled = false;
        //    PlayerAimSwaper.enabled = false;

        //    Debug.Log("Player.Release()");
        //}


        /// <summary>
        /// 사격 
        /// </summary>
        //public void Fire()
        //{
        //    FireController.StartEvent.Invoke();
        //}

        /// <summary>
        /// 사격 중지
        /// </summary>
        //public void Stop()
        //{
        //    FireController.EndEvent.Invoke();
        //}

        /// <summary>
        /// 재장전
        /// </summary>
        public void Reload()
        {

        }

        /// <summary>
        ///강제 에니메이션 재시작[Gun Trigger 방식] 
        /// </summary>
        public void Hit()
        {
            Debug.Log("do nothing!!");
        }
    }
}
