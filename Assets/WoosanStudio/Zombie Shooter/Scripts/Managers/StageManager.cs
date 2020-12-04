using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
using WoosanStudio.Camera;
using WoosanStudio.Common;

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

        [Header("[씬이름 리스트 [Auto->Awake()]]")]
        public List<string> StageNames = new List<string>();

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

        //[Header("[몬스터 팩토리 [Auto-Awake()]]")]
        //public MonsterFactory MonsterFactory;

        [Header("[몬스터 요청 [Auto-Awake()]]")]
        public MonsterRequester MonsterRequester;

        [Header("[몬스터 스폰 스케줄에 따라 몬스터 요청 [Auto-Awake()]]")]
        public MonsterSpawnScheduleManager MonsterSpawnScheduleManager;

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

        [Header("[UI 카운터 [Auto-Awake()]]")]
        public StartCounter StartCounter;

        [Header("[Fog 컨트롤러 [Auto-Awake()]]")]
        public FogController FogController;

        [Header("[카드 셀렉트 팝업 오프너]")]
        public Ricimi.PopupOpener CardSelectPopupOpener;

        //카메라를 움직이는 컨트롤
        private CameraMoveController CameraMoveController;

        //플레이어 팩토리에서 생성된 플레이어
        private PlayerController Player;

        //플레이어가 가지고 있는 무기 요청 스크립트
        private WeaponRequester WeaponRequester;

        //생성된 플레이어 활성 비활성 제어
        //private PlayersController playersController;        

        private void Awake()
        {
            Instance = this;

            //자동으로 가져오기
            //MonsterFactory = GameObject.FindObjectOfType<MonsterFactory>();
            MonsterRequester = GameObject.FindObjectOfType<MonsterRequester>();
            MonsterSpawnScheduleManager = GameObject.FindObjectOfType<MonsterSpawnScheduleManager>();

            //스테이지 네임 자동 세팅 => 해당 씬 이름과 같음
            for (int i = 0; i < MonsterSpawnScheduleManager.MonsterScheduleList.Count; i++)
            {
                StageNames.Add(MonsterSpawnScheduleManager.MonsterScheduleList[i].StageName);
            }

            CameraMoveController = GameObject.FindObjectOfType<CameraMoveController>();
            //playersController = GameObject.FindObjectOfType<PlayersController>();
            //플레이어 생성 담당
            PlayerFactory = GameObject.FindObjectOfType<PlayerFactory>();
            FollowCameraTarget = GameObject.FindObjectOfType<FollowCameraTarget>();

            //UI 카운터
            StartCounter = GameObject.FindObjectOfType<StartCounter>();
            //안개 컨트롤
            FogController = GameObject.FindObjectOfType<FogController>();
        }

        //몬스터 리스트 생성보다 늦게 셋업
        private void Start()
        {
            //매 라운드 종료시마다 Popup 호출하는 메서드 리스너에 등록
            //*매 라운드 몬스터 생성이 끝났을때 모든 몬스터가 죽으면 호출 되는 이벤트
            MonsterList.Instance.ListEmptyEvent.AddListener(CallPopup);

            
            
        }

        /// <summary>
        /// 씬이 로드되고 첫 실행 
        /// </summary>
        public void FirstStage()
        {
            //플레이어 생성
            Initialize();
            //카메라 락 해제
            //**해당 부분은 나중에 이벤트 호출로 변경 되어야 함
            //CM_카메라 활성화. CM 카메라가 활성화되면 카메라 피봇이 강제 조정됨.
            //On();

            //자동으로 스테이지 카운팅하면서 스테이지 스왑
            //CameraMoveController.AutoChange();

            //Popup 테스트를 위해서 잠시 제거
            NextStage();
        }


        /// <summary>
        /// 자동으로 스테이지 이동
        /// </summary>
        public void NextStage()
        {
            //카메라 락 해제
            //**해당 부분은 나중에 이벤트 호출로 변경 되어야 함
            //CM_카메라 활성화. CM 카메라가 활성화되면 카메라 피봇이 강제 조정됨.
            On();

            //자동으로 스테이지 카운팅하면서 스테이지 스왑
            CameraMoveController.AutoChange();
        }


        /// <summary>
        /// 최초 플레이어 생성 및 디스에이블
        /// </summary>
        public void Initialize()
        {
            //플레이어 생성 - 생성된 플레이어 저장
            Player = PlayerFactory.Initialize().GetComponent<PlayerController>();
            //플레이어가 가지고 있는 무기 요청 가지고 옴
            WeaponRequester = Player.GetComponent<WeaponRequester>();
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
            //펠로의 캡에 모든 타겟들의 포지션을 초기화 시킴.ㅌ
            FollowCameraTarget.Reposition();
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
            StartCoroutine(WaitAndDoCoroutine(1f, () => {
                //조이스틱으로 화면 따라다니는 카메라 활성화
                CustomCamFollow.enabled = true;
            }));

            #region [TestCode] -테스트 폰에 올리기 위한 강제 실행 코드
            //무기 만들어서 앵커에 달
            //WeaponRequester.Anchor();
            //카운팅 시작
            StartCounter.Count();
            #endregion
        }

        /// <summary>
        /// 카운팅 까지 다 끝나고 UI카운터의 끝에서 호출.
        /// *실질적인 스테이지의 시작
        /// </summary>
        public void CountingEnd()
        {
            //테스토 용으로 남겨놔야 함
            //*해당 스테이지 강제 생성 테스트용
            //MonsterRequester.MakeMonsterByStageLevel();

            //스케줄러에 의해 몬스터 생성
            //해당 레벨에 맞는 몬스터 생성
            for (int i = 0; i < StageNames.Count; i++)
            {
                Debug.Log("현재 스테이지 이름 = " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
                Debug.Log("[" + i + "] = " + StageNames[i]);

                //현재 씬 이름과 씬 이름 리스트에서 같은 이름 찾아서 인덱스 가져오기
                if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals(StageNames[i]))
                {
                    //현재 씬과 같은 이름을 찾았다면 해당 인덱스로 스테이지로 스폰시작
                    MonsterSpawnScheduleManager.SpawnByStage(i);
                }
            }
        }

        /// <summary>
        /// 해당 레벨에서 로드할수 있는 모든 것을 로드 한다.
        /// *지금 왜 사용안하는지 모름
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

            //안개 연하게
            FogController.DencityChange(1);
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

            //안개 진하게
            FogController.DencityChange(0);
        }

        /// <summary>
        /// 카드를 선택하는 팝업을 호출할지 스테이지가 결과 팝업을 호출할지 결정
        /// * 몬스터가 클리어 된 상황에 호출
        /// * MonsterList.Instance.ListEmptyEvent 에 리스너로 등록
        /// </summary>
        public void CallPopup()
        {
            //슬로우 모션 시작
            //SlowMotionTimeManager.Instance.DoSlowMotion();

            //라운드 끝이라면 스테이지 결과 종료 팝업 호출
            if (MonsterSpawnScheduleManager.IsEndRound())
            {
                CardSelectPopupOpener.OpenPopupAndReturn();
            }
            else //아니라면 카드 선택하는 팝업 호출
            {
                GameObject clone  = CardSelectPopupOpener.OpenPopupAndReturn();

                //popup ok 버튼 클릭 이벤트에 몬스터 스케줄 다음으로 넘어가게 연결
                Ricimi.BasicButton.ButtonClickedEvent clickedEvent;

                clickedEvent = clone.GetComponent<CardRewardSelectPopupController>().BasicButton.OnClicked;

                //다음 라운드 실행
                //*만약 팝업 재사용 코드일때는 등록시 매번 중복 체크해서 지워줘야 함.
                clickedEvent.AddListener(MonsterSpawnScheduleManager.SpawnByNextRound);
            }

            Debug.Log("aa");
        }

        /// <summary>
        /// 스테이지 변경을 위한 테스트 코드
        /// </summary>
        #region [-TestCode]
        /*void Update()
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

            //스테이지 이동을 자동으로 함 -> StageManager가 할일을 하고있음.
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                NextStage();
            }

            //카드 셀렉터를 오픈한다.
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CardSelectPopupOpener.OpenPopup();
            }

            //펠로우 캠 포지셔너만 해당위치로 이동
            //스테이지 이동시 카메라 이상행동 때문에 
            if (Input.GetKeyDown(KeyCode.K))
            {
                FollowCameraPositioner.Move();
            }
        }*/
        #endregion
    }
}
