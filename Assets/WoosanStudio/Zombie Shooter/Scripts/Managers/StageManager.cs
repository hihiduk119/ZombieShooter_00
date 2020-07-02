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
    /// 1.캐릭터는 몇명 생성 할지
    /// 2.몇번째 스테이지로 씬을 보낼지
    /// 
    /// </summary>
    public class StageManager : MonoBehaviour
    {
        //싱글톤 패턴으로 만들기
        static public StageManager Instance;

        [Header("[화면 터치시 자동으로 하면을 따라 다니면서 연출하는 컨트롤러]")]
        public CustomCamFollow CustomCamFollow;

        [Header("[시네머신의 가상 카메라]")]
        public CinemachineVirtualCamera VirtualCamera;

        [Header("[카메라의 느리게 좌우로 움직임]")]
        public CameraNativeWalk CameraNativeWalk;

        [Header("[플레이어 포지셔너]")]
        //**이벤츠 호출 방식으로 변경 되어야함
        public Positioner PlayerPositioner;

        [Header("[펠로우 카메라 포지셔너]")]
        //**이벤츠 호출 방식으로 변경 되어야함
        public Positioner FollowCameraPositioner;

        private CameraMoveController stageChangeController;

        //플레이어 생성
        private PlayerFactory playerFactory;
        //몬스터 생성
        private MonsterFactory monsterFactory;
        //생성된 플레이어 활성 비활성 제어
        private PlayersController playersController;

        private void Awake()
        {
            Instance = this;

            //자동으로 가져오기
            playerFactory = GameObject.FindObjectOfType<PlayerFactory>();
            monsterFactory = GameObject.FindObjectOfType<MonsterFactory>();
            stageChangeController = GameObject.FindObjectOfType<CameraMoveController>();
            playersController = GameObject.FindObjectOfType<PlayersController>();
        }

        /// <summary>
        /// 플레이이어와 몬스터 들을 세팅 한다.
        /// </summary>
        public void Load()
        {
            LoadAllProps();

            //조이스틱으로 화면 따라다니는 카메라 포지션 초기화
            CustomCamFollow.GetComponent<Transform>().localPosition = Vector3.zero;
            //조이스틱으로 화면 따라다니는 카메라 활성화
            CustomCamFollow.enabled = true;
            //카메라 느리게 좌우로 흔듬 시작
            CameraNativeWalk.Run();
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
            stageChangeController.Change(level);
        }



        /// <summary>
        /// 캠 이동전 호출
        /// 씨네머신 활성화
        /// </summary>
        public void On()
        {
            Debug.Log("On");
            CustomCamFollow.enabled = false;
            VirtualCamera.enabled = true;
            //카메라 느리게 좌우로 흔듬 정지
            CameraNativeWalk.Stop();
        }

        /// <summary>
        /// 캠 이동 완료 후 호출
        /// 씨네머신 해제
        /// </summary>
        public void Off()
        {
            Debug.Log("Off");
            //CustomCamFollow.enabled = true;
            VirtualCamera.enabled = false;
            //플레이어 위치 재조정
            //PlayerPositioner.Move();
            //펠로우 카메라 위치 재조정
            //FollowCameraPositioner.Move();
        }


        /// <summary>
        /// 스테이지 변경을 위한 테스트 코드
        /// </summary>
        #region [-TestCode]
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Initialize(0);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Initialize(1);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Load();
            }
        }
        #endregion

    }
}
