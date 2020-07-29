using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
using WoosanStudio.Camera;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 게임 전체를 통합 관리 한다.
    ///
    /// 1.캐릭터는 생성 할지
    /// 2.캐릭터 활성 비활성화
    /// 3.몬스터 생성
    /// 4.몇번째 스테이지로 씬을 보낼지
    /// 5.스테이지의 시작과 끝 및 중간 UI 표출등 모든 부분 관리.
    /// 
    /// </summary>
    public class StageManager : MonoBehaviour
    {
        //싱글톤 패턴으로 만들기
        static public StageManager Instance;

        [Header("[화면 터치시 자동으로 하면을 따라 다니면서 연출하는 컨트롤러]")]
        public CustomCamFollow CustomCamFollow;

        [Header("[캐릭터 중심으로 캠 따라다니는 컨트롤러]")]
        public FollowCameraTarget FollowCameraTarget;

        [Header("[시네머신의 가상 카메라]")]
        public CinemachineVirtualCamera VirtualCamera;

        [Header("[카메라의 느리게 좌우로 움직임]")]
        public CameraNativeWalk CameraNativeWalk;

        [Header("[플레이어 팩토리 [Auto-Awake()]]")]
        public PlayerFactory PlayerFactory;

        [Header("[플레이어 포지셔너]")]
        //이벤트 호출 방식으로 변경 하려 했으나 호출 우선 순위 문제로 변경 보류
        //플레이어 포지션 변경
        public Positioner PlayerPositioner;

        [Header("[펠로우 카메라 포지셔너]")]
        //이벤트 호출 방식으로 변경 하려 했으나 호출 우선 순위 문제로 변경 보류
        //펠로우 캠 포지션 변경
        public Positioner FollowCameraPositioner;

        [Header("[DoNotEnterSign 포지셔너]")]
        //이벤트 호출 방식으로 변경 하려 했으나 호출 우선 순위 문제로 변경 보류
        public Positioner DoNotEnterPositioner;

        [Header("[AimIK Target 포지셔너]")]
        public Positioner AimIKTargetPositioner;

        //카메라를 움직이는 컨트롤
        private CameraMoveController CameraMoveController;

        //플레이어 생성
        private PlayerFactory playerFactory;

        //플레이어 팩토리에서 생성된 플레이어
        private Player Player;

        //몬스터 생성
        private MonsterFactory monsterFactory;
        //생성된 플레이어 활성 비활성 제어
        //private PlayersController playersController;

        

        private void Awake()
        {
            Instance = this;

            //자동으로 가져오기
            playerFactory = GameObject.FindObjectOfType<PlayerFactory>();
            monsterFactory = GameObject.FindObjectOfType<MonsterFactory>();
            CameraMoveController = GameObject.FindObjectOfType<CameraMoveController>();
            //playersController = GameObject.FindObjectOfType<PlayersController>();
            //플레이어 생성 담당
            PlayerFactory = GameObject.FindObjectOfType<PlayerFactory>();
            FollowCameraTarget = GameObject.FindObjectOfType<FollowCameraTarget>();
        }

        /// <summary>
        /// 씬이 로드되고 첫 실행 
        /// </summary>
        public void FirstStage()
        {
            //플레이어 생성
            Initialize();
            //카메라 연출 시
            CameraMoveController.AutoChange();
        }


        /// <summary>
        /// 최초 플레이어 생성 및 디스에이블
        /// </summary>
        public void Initialize()
        {
            //플레이어 생성 - 생성된 플레이어 저장
            Player = PlayerFactory.Initialize().GetComponent<Player>();
        }

        /// <summary>
        /// 람다식 코루틴
        /// </summary>
        /// <param name="time">대기시간</param>
        /// <param name="action">실행액션</param>
        /// <returns></returns>
        IEnumerator WaitAndDoCoroutine(float time, System.Action action)
        {
            yield return new WaitForSeconds(time);
            action();
        }

        /// <summary>
        /// 플레이이어와 몬스터 들을 세팅 한다.
        /// </summary>
        public void Load()
        {
            Debug.Log("!!!!!!!!!! => Load");
            LoadAllProps();

            //카메라 느리게 좌우로 흔듬 시작
            CameraNativeWalk.Run();
            //플레이어 위치 위치 재조정
            PlayerPositioner.Move();
            //펠로우 카메라 위치 위치 재조정
            //**펠로우 캠 의 포지션이 끝난후에 CustomCamFollow.cs 초기화가 호출되어야 한다.
            FollowCameraPositioner.Move();
            //DoNotEnterSign 위치 재조정
            DoNotEnterPositioner.Move();

            //AimIK Target 위치 재조정
            AimIKTargetPositioner.Move();

            //두 낫 엔터 가이드 활성화
            DoNotEnterPositioner.gameObject.SetActive(true);

            //딜레이 후에 펠로우캠 실행해야 포커스 이상 발생이 없음.
            //**왜 그런지는 알수 없음. 추후 문재가 발생시 수정 해야함.
            //*원인 찾은듯 AheadTarget이 타겟 위치로 움직이는 시간이 걸려서 그런듯함.
            //*AheadTarget을 씬 마무리에 캐릭터 위치로 초기화하는 부분이 필요할듯.
            StartCoroutine(WaitAndDoCoroutine(0.2f, () => {
                //조이스틱으로 화면 따라다니는 카메라 활성화
                CustomCamFollow.enabled = true;
            }));
        }

        /// <summary>
        /// 해당 레벨에서 로드할수 있는 모든 것을 로드 한다.
        /// </summary>
        private void LoadAllProps()
        {
            //몬스터 초기화
            //monsterFactory.Initialize();
            //플레이어 초기화
            //playerFactory.Initialize();
        }

        /// <summary>
        /// 해당 레벨로 초기화 시킴
        /// </summary>
        /// <param name="level"></param>
        public void Initialize(int level)
        {
            //해당 스테이지로 화면을 이동시킴
            CameraMoveController.Change(level);
        }

        /// <summary>
        /// 캠 이동전 호출
        /// 씨네머신 활성화
        /// </summary>
        public void On()
        {
            Debug.Log("On");
            //조이스틱으로 화면 따라다니는 카메라 포지션 비활성화
            CustomCamFollow.enabled = false;
            //조이스틱으로 화면 따라다니는 카메라 포지션 초기화
            CustomCamFollow.GetComponent<Transform>().localPosition = Vector3.zero;

            VirtualCamera.enabled = true;
            //카메라 느리게 좌우로 흔듬 정지
            CameraNativeWalk.Stop();

            //플레이어 비활성화
            Player.Deactive();

            //FollowCamTarget 비활성화
            FollowCameraTarget.enabled = false;

            //두 낫 엔터 가이드 비활성화
            DoNotEnterPositioner.gameObject.SetActive(false);
        }

        /// <summary>
        /// 캠 이동 완료 후 호출
        /// 씨네머신 해제
        /// </summary>
        public void Off()
        {
            Debug.Log("Off");

            //연출 카메라 비활성화
            VirtualCamera.enabled = false;

            //플레이어 비활성화
            Player.Active();

            //FollowCamTarget 비활성화
            FollowCameraTarget.enabled = true;

        }

        /// <summary>
        /// 스테이지 변경을 위한 테스트 코드
        /// </summary>
        #region [-TestCode]
        void Update()
        {
            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    Initialize(0);
            //}

            //if (Input.GetKeyDown(KeyCode.S))
            //{
            //    Initialize(1);
            //}

            //if (Input.GetKeyDown(KeyCode.D))
            //{
            //    Load();
            //}

            //씬이 첫 로딩이 되고 실행되는 메서 
            if (Input.GetKeyDown(KeyCode.F))
            {
                FirstStage();
            }


            //펠로우 캠 포지셔너만 해당위치로 이동
            //스테이지 이동시 카메라 이상행동 때문에 
            if(Input.GetKeyDown(KeyCode.K))
            {
                FollowCameraPositioner.Move();
            }
        }
        #endregion

    }
}
