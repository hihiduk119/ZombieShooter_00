using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RootMotion.FinalIK;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 타겟을 조준 및 해제 시킴.
    /// </summary>
    public class LookAtAimedTarget : MonoBehaviour , IAim
    {
        [Header("[실제 AimIK에서 조준할 타겟이름]")]
        public string TargetName = "Aim IK Target";
        [Header("[캐리겉 컨트롤러로 조준을 시킴 -> 애니메이션 연관 (Auto->Awake())]")]
        public PlayerMoveActor PlayerMoveActor;
        [Header("[조준시 팔다리몸 IK -> Final IK (Auto->Awake())]")]
        public AimIK AimIK;
        [Header("[조준시 헤드 IK -> Final IK (Auto->Awake())]")]
        public LookAtIK LookAtIK;
        [Header("[Final IK 사용시 딱딱한 움직인 부드럽게 바꿔줌 (Auto->Awake())]")]
        public PlayerAimSwaper PlayerAimSwaper;
        [Header("[가장가까운 몬스터를 가져오기위해. (Auto->Awake())]")]
        public FindAimTarget FindAimTarget;
        [Header("[카메라의 보는 위치를 카메라와 타겟 중간으로 마춰주기 위해 사용(Auto->Awake())]")]
        public FollowCameraTarget FollowCameraTarget;
        [Header("[조준하는 최소 거리]")]
        public float AimDistance = 10f;

        //이전 릴리즈 상태 저장 -> 타겟 변경시 이전 타겟지점을 잡고 있어서 확확 움직이는 동작을 막기위해 필요.
        private bool bReleased = true;

        [Header("[지정 타겟이 없을때 [AimIKTarget] 초기화 포지션]")]
        public Transform AimIKTargetAnchor;

        //조준 상태 이넘
        public enum State {
            Aim,
            Release,
        }
        //현재 상태 토글
        public State CurrentState = State.Release;
        //이전 프레임 상태 토글
        private State PriviousState = State.Release;

        //IAim 구현부 -> 조준 이벤트 발생
        public UnityEvent AimEvent => aimEvent;
        //IAim 구현부 -> 조준해제 이벤트 발생
        public UnityEvent ReleaseEvent => releaseEvent;

        [SerializeField]
        private UnityEvent aimEvent = new UnityEvent();
        [SerializeField]
        private UnityEvent releaseEvent = new UnityEvent();

        private void Awake()
        {
            //Auto
            PlayerMoveActor = GetComponent<PlayerMoveActor>();
            AimIK = GetComponent<AimIK>();
            LookAtIK = GetComponent<LookAtIK>();
            PlayerAimSwaper = GetComponent<PlayerAimSwaper>();
            FindAimTarget = GetComponent<FindAimTarget>();
            FollowCameraTarget = GameObject.FindObjectOfType<FollowCameraTarget>();

            //이름으로 타겟 설정 초기화
            Initializ(TargetName);
        }

        /// <summary>
        /// AimIK 및 LookAtIK에서 사용될 타겟을 찾음
        /// </summary>
        /// <param name="aimTarget">찾으려는 타겟 이름</param>
        public void Initializ(string aimTarget)
        {
            Transform target = GameObject.Find(TargetName).transform;
            AimIK.solver.target = target;
            LookAtIK.solver.target = target;
            PlayerMoveActor.fireTarget = target;
        }

        /// <summary>
        /// 조준 시작
        /// </summary>
        public void Aim()
        {
            //플레이어 에니메이션 관련 컨트롤
            PlayerMoveActor.aimed = true;

            //Aim IK 컨트롤
            AimIK.enabled = true;
            //LookAt IK 컨트롤
            LookAtIK.enabled = true;
            //Final IK 를 부드럽게 만듬
            PlayerAimSwaper.enabled = true;

            //현재 상태 aim으로 변경
            CurrentState = State.Aim;
            //이전 상태가 aim가 아니라면 이벤트 발생
            if (PriviousState != State.Aim) { AimEvent.Invoke(); Debug.Log("조준 이벤트 발생"); }
            //현재 상태와 이전 상태 동일하게 마춤
            PriviousState = CurrentState;
            //Debug.Log("LookAtAimedTarget.Aim()");
        }

        /// <summary>
        /// 조준 해제
        /// </summary>
        public void Release()
        {
            //플레이어 에니메이션 관련 컨트롤
            PlayerMoveActor.aimed = false;
            //Aim IK 컨트롤
            AimIK.enabled = false;
            //LookAt IK 컨트롤
            LookAtIK.enabled = false;
            //Final IK 를 부드럽게 만듬
            PlayerAimSwaper.enabled = false;

            //현재 상태 release으로 변경
            CurrentState = State.Release;
            //이전 상태가 Release가 아니라면 이벤트 발생
            if(PriviousState != State.Release) { ReleaseEvent.Invoke(); Debug.Log("조준해제 이벤트 발생"); }
            //현재 상태와 이전 상태 동일하게 마춤
            PriviousState = CurrentState;
            //Debug.Log("LookAtAimedTarget.Release()");
        }

        #region [-TestCode]
        void Update()
        {
            //가장 가까운 몬스터 찾아서 에임 스와퍼에 넣어서 조준 할수 있게 만듬.
            //[Aim IK Target]을 몬스터에 마
            //PlayerAimSwaper.AimTarget = FindAimTarget.target;

            //에임시 카메라를 캐릭터와 타겟 중간에 위치 시키기 위해 사용.
            //FollowCameraTarget.LookAtTarget = FindAimTarget.target;

            //타겟이 존제 할때만 실행
            if (FindAimTarget.target != null)
            {
                //이전 타겟과 지금 타겟이 같은지 확인.
                //*기존에 있던 포지션 때문에 확확 도는 문제 해결용
                //if (previousTarget != null)
                //{
                //    if (!previousTarget.Equals(FindAimTarget.target))
                //    {
                //        //Debug.Log("새로운 타겟으로 벼뀠음!!!!!");
                //        //PlayerAimSwaper.ImmediatelyAiming(FindAimTarget.target);
                //    }
                //} else
                //{
                //    Debug.Log("타겟이 존제하지 안습니다.");
                //}

                //나와 조준 타겟간의 거리차에 의해서
                //Aim
                if (AimDistance >= Vector3.Distance(transform.position, FindAimTarget.target.position))
                {
                    Aim();    
                }
                else//Release
                {
                    Release();

                    //타겟이 없을때 AimIKTarget 초기화 포지션으로 이동시킴
                    PlayerAimSwaper.ImmediatelyAiming(AimIKTargetAnchor);
                }

                //이전 타겟 저장
                //previousTarget = FindAimTarget.target;
            } else
            {
                //타겟이 없을때 AimIKTarget 초기화 포지션으로 이동시킴
                PlayerAimSwaper.ImmediatelyAiming(AimIKTargetAnchor);
            }


            //플레이어 몬스터 조준
            if (Input.GetKeyDown(KeyCode.O))
            {
                Aim();
            }

            //플레이어 몬스터 조준 해제
            if (Input.GetKeyDown(KeyCode.P))
            {
                Release();
            }

            //룩엣을 이용하여 화면 움직임 테스트하기 위해 사용
            //if(Input.GetKeyDown(KeyCode.Q))
            //{
            //    bTestLookAtToggle = !bTestLookAtToggle;
            //    FollowCameraTarget.bLookAt = bTestLookAtToggle;
            //    Debug.Log("룩엣 타겟 활성상태 = " + bTestLookAtToggle.ToString());
            //}
        }
        #endregion
    }
}
