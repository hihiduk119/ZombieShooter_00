using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using WoosanStudio.Common;

namespace WoosanStudio.ZombieShooter
{
    public class Player : MonoBehaviour , IHaveHit
    {
        //무기를 만들어주는 팩토리 패턴 적용.
        public WeaponFactory _weaponFactory;
        //인풋 액션
        IInputEvents _inputEvents;
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
            _inputEvents = GetComponent<IInputEvents>();

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
            _iWeapon = _weaponFactory.MakeWeapon(_inputEvents, _cameraShaker, _reloadActionList, ref Gun, Joint, (int)weaponType, playerConfig);

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

        /// <summary>
        ///강제 에니메이션 재시작[Gun Trigger 방식] 
        /// </summary>
        public void Hit()
        {
            Debug.Log("do nothing!!");
        }
    }
}
