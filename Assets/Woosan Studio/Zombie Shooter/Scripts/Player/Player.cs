using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using WoosanStudio.Common;

namespace WoosanStudio.ZombieShooter
{
    public class Player : MonoBehaviour
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

        //캐슁용
        IWeapon _iWeapon;
        IGun _iGun;
        Transform target;

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
        public void Initialize(WeaponFactory weaponFactory,ICameraShaker cameraShaker , ref UnityAction<Vector3> lookAction , ref ILookPoint lookPoint)
        {
            this._weaponFactory = weaponFactory;
            this._cameraShaker = cameraShaker;

            //lookPoint.UpdatePositionEvent.AddListener(lookAction);
        }

        IEnumerator Start()
        {
            //플레이어 팩토리 동작시 삭제 예정
            //_weaponFactory = FindObjectOfType<WeaponFactory>();

            yield return new WaitForSeconds(0.2f);

            if (_inputEvents == null) { Debug.Log("키 인풋이 널임!!"); }

            //playerFSM = new PlayerFSM();

            //키인풋으로 사격 컨트롤
            _iWeapon = _weaponFactory.MakeWeapon(_inputEvents, _cameraShaker, _reloadActionList, ref _iGun, Joint, 1);

            yield return new WaitForSeconds(0.1f);
        }

        //private void Update()
        //{
        //    FSM();
        //}

        //public void FSM()
        //{
        //    Movement();
        //    Input();
        //}

        //void Movement()
        //{

        //}

        //void Input()
        //{
        //    target = TargetUtililty.GetNearestTarget(MonsterList.Instance.Items, this.transform);


        //    #region [-TestCode : Player와 현재 가장 가까운 몬스터 타겟의 거리를 표시한다]
        //    if (target != null)
        //    {
        //        Vector3 pos = GameObject.FindGameObjectWithTag("Player").transform.position;
        //        Debug.DrawLine(pos, target.position, Color.green) ;
        //    }
        //    #endregion
        //}

        public void PlayAnimation(float number)
        {

        }

        public void AttackStart()
        {
            _iWeapon.Attack();
        }

        public void AttackStop()
        {
            _iWeapon.Stop();
        }
    }
}
