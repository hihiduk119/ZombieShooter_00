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
    /// 2. 사격 통제 & 재장전
    /// 3. 조준 비조준
    /// </summary>
    public class Player : MonoBehaviour , IHaveHit
    {
        //[Auto-Awake()]
        //움직임
        public WoosanStudio.Player.Move Move;
        //에니메이션 -> 이건 따로 스크립트 만들어야 할듯
        public Animator Animator;
        //PlayerMoveActor 와 연결된 실제 회전 움직임
        public MyThirdPersonCharacter MyThirdPersonCharacter;
        //회전 및 조준
        public PlayerMoveActor PlayerMoveActor;
        //데미지 UI텍스트 
        public TextDamageBridge TextDamageBridge;
        //데미지 UI체력바 
        public HealthBar HealthBar;
        //실제 체력
        public HaveHit HaveHit;
        //조준시 팔다리몸 IK
        public AimIK AimIK;
        //조준시 헤드 IK
        public LookAtIK LookAtIK;
        //조준 IK들을 더부드럽게 움직이게 만들어주는 Aim컨트롤
        public PlayerAimSwaper PlayerAimSwaper;
        //실제 사격 컨트롤
        public FireController FireController;
        //포지션 재배체
        public Positioner Positioner;
        //모델 변경
        public Model Model;

        void Awake()
        {
            //생성과 동시에 자동 셋업
            Move = GetComponent<WoosanStudio.Player.Move>();
            Animator = GetComponent<Animator>();
            MyThirdPersonCharacter = GetComponent<MyThirdPersonCharacter>();
            PlayerMoveActor = GetComponent<PlayerMoveActor>();
            TextDamageBridge = GetComponent<TextDamageBridge>();
            HealthBar = GetComponent<HealthBar>();
            HaveHit = GetComponent<HaveHit>();
            AimIK = GetComponent<AimIK>();
            LookAtIK = GetComponent<LookAtIK>();
            PlayerAimSwaper = GetComponent<PlayerAimSwaper>();
            FireController = GetComponent<FireController>();
            Positioner = GetComponent<Positioner>();
            Model = GetComponentInChildren<Model>();
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
            HealthBar.HealthbarPrefab.gameObject.SetActive(true);
            PlayerAimSwaper.enabled = true;
            FireController.enabled = true;
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
            HealthBar.HealthbarPrefab.gameObject.SetActive(false);
            AimIK.enabled = false;
            LookAtIK.enabled = false;
            PlayerAimSwaper.enabled = false;
            FireController.enabled = false;
        }

        /// <summary>
        /// 타겟 조준
        /// </summary>
        public void Aiming()
        {
            AimIK.enabled = true;
            LookAtIK.enabled = true;
        }

        /// <summary>
        /// 조준 해제
        /// </summary>
        public void Release()
        {
            AimIK.enabled = false;
            LookAtIK.enabled = false;
        }


        /// <summary>
        /// 사격
        /// </summary>
        public void Fire()
        {
            FireController.StartEvent.Invoke();
        }

        /// <summary>
        /// 재장ㅖ
        /// </summary>
        public void Reload()
        {

        }






        //=====================   [Old code]   =====================
        /*
        //무기를 만들어주는 팩토리 패턴 적용.
        public WeaponFactory _weaponFactory;
        //IInputEvents _inputEvents;
        //공격 시작 이벤트
        IStart startEvent;
        //공격 끝 이벤트
        IEnd endEvent;
        //ICameraShaker를 가져오기 위한 용도
        public GameObject Shaker;
        //무기 연결 부분
        public Transform Joint;
        //카메라 쉐이커
        ICameraShaker _cameraShaker;
        //리로딩시 콜벡 액션 리스트
        List<IReloadAction> _reloadActionList = new List<IReloadAction>();

        public GunSettings.WeaponType weaponType = GunSettings.WeaponType.AssaultRifle;

        //플레이어 좌우 이동 에니메이션 제어용.
        public AnimatorControl AnimatorControl;

        //유저가 사용할 인풋
        public IInput userInput;

        //플레이어의 건
        public IGun Gun;

        //캐슁용
        IWeapon _iWeapon;
        
        Transform target;
        float h;
        float v;

        //프로퍼티 형태의 클래스
        private DoRoll _doRoll;
        public Animator _animator;

        private void Awake()
        {
            //플레이어 팩토리 동작시 삭제 예정
            //_cameraShaker = Shaker.GetComponent<ICameraShaker>();
            //_inputEvents = GetComponent<IInputEvents>();
            startEvent = GetComponent<IStart>();
            endEvent = GetComponent<IEnd>();

            _reloadActionList.Add(GetComponent<IReloadAction>());

            //Roll 에니메이션 세팅
            _doRoll = FindObjectOfType<DoRoll>();
            _doRoll.SetAnimator(_animator);

        }

        /// <summary>
        /// 플레이어 팩토리에서 생성시 반드시 초기화 해야함.
        /// </summary>
        /// <param name="weaponFactory">무기공장 세팅</param>
        /// <param name="cameraShaker">카메라 쉐이커 세팅</param>
        /// <param name="playerConfig">플레이어 데이터</param>
        public void Initialize(WeaponFactory weaponFactory,ICameraShaker cameraShaker , ref UnityAction<Vector3> lookAction
            , ref ILookPoint lookPoint,PlayerConfig playerConfig , IInput playerMoveInput)
        {
            this._weaponFactory = weaponFactory;
            this._cameraShaker = cameraShaker;
            //플레이어의 기본 인풋에 유저 인풋 셋팅 및  실제 움직임울 담당하는 Move.cs에 input인터페이스 세팅 해줌
            GetComponent<Move>().userInput = this.userInput = playerMoveInput;

            //키인풋으로 사격 컨트롤 및 몇번 무기를 사용할지 결정
            _iWeapon = _weaponFactory.MakeWeapon(startEvent,endEvent, _cameraShaker, _reloadActionList, ref Gun, Joint, (int)weaponType, playerConfig.useLaserPointer,playerConfig.ShadowProjector);

            //좌우 이동 에니메이션 컨트롤러 생성 => Player의 에니메이터를 찾아서 생성 및 세팅
            AnimatorControl = new AnimatorControl(transform.GetComponentInChildren<Animator>());
        }

        

        public void Move(float value)
        {
            //실제 플레이어 조종

            //에니메이션 연출
            //value가 마이너스 라면 +로 변경
            if(value <= 0) {
                value *= -1;
                //Debug.Log("마이너스 value =" + value);
            } else
            {
                //Debug.Log("플러1 value =" + value);
            }
            
            AnimatorControl.SetMoveValue(value);
        }

        

        #region [-TestCode]
        //에니메이션 동작 확인용
        private void FixedUpdate()
        {
            //조이스틱 사용시
            //h = UltimateJoystick.GetHorizontalAxis("LookAhead");
            //가속 센서 사용시
            //h = Input.acceleration.x * 2;
            //인풋 인터페이스에서 설정
            h = userInput.Horizontal;
            v = userInput.Vertical;

            //[움직임 변경중]
            //Move(new Vector2(h,v).magnitude);
        }
        #endregion
        */



        /// <summary>
        ///강제 에니메이션 재시작[Gun Trigger 방식] 
        /// </summary>
        public void Hit()
        {
            Debug.Log("do nothing!!");
        }
    }
}
