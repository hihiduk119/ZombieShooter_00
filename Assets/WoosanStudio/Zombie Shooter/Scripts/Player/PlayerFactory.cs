using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WoosanStudio.Extension;
using WoosanStudio.Common;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class PlayerFactory : MonoBehaviour
    {
        //플레이어 모델 타입
        public enum PlayerModels : int
        {
            Sheriff = 0,
            RiotCop,
            BusinessMan,
        }

        //[Header("[플레이어 오브젝트를 담은 프리팹들]")]
        //public List<GameObject> Prefabs = new List<GameObject>();
        [Header("[무기를 만들어주는 팩토리 패턴 적용.]")]
        public WeaponFactory WeaponFactory;
        [Header("[ICameraShaker를 가져오기 위한 용도]")]
        public GameObject Shaker;
        [Header("[조이스틱에 의해 화면 움직임 담당 [Auto-Awake()]]")]
        public FollowCameraTarget FollowCameraTarget;

        public Transforms PlayerPointRoot;
        [Header("[플레이어 생성 위치]")]
        public List<Transform> PlayerPoints;
        [Header("[(AI 미 사용시)[플레이어 터치 위치에 따른 회전]")]
        public MoveTouchPointToRayPosition MoveTouchPointToRayPosition;
        [Header("[(AI 사용시) 가장 가까운 몬스터 자동 회전]")]
        public AutoAim AutoAim;

        [Header("플레이어 캐릭터 좌우 움직임에 조이스틱 사용")]
        public bool UseJoystick;
        private IInput userInput;


        //카메라 쉐이커
        private ICameraShaker cameraShaker;

        [Header("[플레이어의 모든 정보를 가지고 있는 프리팹]")]
        public PlayerConfig[] playerConfigs;

        //[Header("[플레이어생성 이후 활성 비활성 시키는 컨트롤러]")]
        //public PlayersController PlayersController;

        //플레이어
        //private Player player;

        public delegate void Look(Vector3 pos);
        public Look lookDelegate;

        //public UnityAction<Vector3> lookAction;

        //캐릭터가 생성 됐는지 아닌지 여부 확인을 위해 사용.
        //캐릭터의 갯수와 상관없이 Initialize 가 한번이라도 호출됐다면 체크됨.
        private bool mbInitialized = false;
        public bool IsInitialzed { get => mbInitialized; set => mbInitialized = value; }

        //이벤트
        #region [-캐쉬용]
        GameObject clone = null;
        #endregion

        private void Awake()
        {
            //cameraShaker = Shaker.GetComponent<ICameraShaker>();

            //하나만 존제 하기에 파인드 세팅
            if(FollowCameraTarget == null) FollowCameraTarget = GameObject.FindObjectOfType<FollowCameraTarget>();
        }

        #region [-TestCode]
        //private void Start()
        //{
        //    Initialize();
        //}

        /// <summary>
        /// 초기화
        /// 실제 캐릭터를 생성함.
        /// </summary>
        public GameObject Initialize()
        {
            //초기화 체크
            //캐릭터의 갯수와 상관없이 Initialize 가 한번이라도 호출됐다면 체크됨.
            IsInitialzed = true;

            //플레어 포인트를 루트에서 가져옴
            PlayerPoints = PlayerPointRoot.GetChilds("Point");

            //PlayersController.Players.Add(Make(PlayerPoints[0], playerConfigs[0]).GetComponent<Player>());
            return Make(PlayerPoints[0], playerConfigs[3]);

            //플레이어 사격 및 사격 정지 등을 컨트롤하는 컨트롤러는 필요함.

            //2명의 플레이어를 더 만들수 있는 구조로 되어 있음
            //PlayersController.Players.Add( Make(PlayerPoints[1], playerConfigs[1]).GetComponent<Player>() );
            //PlayersController.Players.Add( Make(PlayerPoints[2], playerConfigs[1]).GetComponent<Player>() );

            //강제로 캐릭터의 조준점 마추는 코드 실행
            //임시 임으로 반드시 삭제가 필요.
            //Invoke("FocusAim", 1f);
        }

        //void FocusAim()
        //{
        //    WoosanStudio.ZombieShooter.Test.TestCode01.Instance.Swap();
        //}
        #endregion


        /// 플레이어 생성
        public GameObject Make(Transform parent, PlayerConfig playerConfig)
        {
            clone = Instantiate(playerConfig.prefab) as GameObject;
            //player = clone.GetComponent<Player>();

            //생성도니 플레이어에 스테이지 메니저와 연결 시켜야함.
            

            //플레이어 포지셔너 와 스테이지 메니져 연결
            //스테이지 메니저는 매 스테이지의 컨트롤을 담당하기에 필요.
            StageManager.Instance.PlayerPositioner = clone.GetComponent<Positioner>();

            //FollowCamTarget.cs 와 플레이어 연결.
            //플레이어 중심으로 화면 고정 시킴
            FollowCameraTarget.player = clone;


            //그림자 사용여부에 따라 그림자 활성화
            if (playerConfig.useShadow)
            {
                GameObject shadowProjector = Instantiate(playerConfig.ShadowProjector);
                shadowProjector.transform.parent = clone.transform;
            }

            clone.transform.parent = parent;
            clone.transform.Reset(Quaternion.Euler(0,270,0));

            return clone;
        }
    }
}
