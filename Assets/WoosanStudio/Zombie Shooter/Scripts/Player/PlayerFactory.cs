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

        public Transforms PlayerPointRoot;
        [Header("[플레이어 생성 위치]")]
        public List<Transform> PlayerPoints;
        [Header("[(AI 미 사용시)[플레이어 터치 위치에 따른 회전]")]
        public MoveScreenPointToRayPosition MoveScreenPointToRayPosition;
        [Header("[(AI 사용시) 가장 가까운 몬스터 자동 회전]")]
        public AutoAim AutoAim;

        [Header("플레이어 캐릭터 좌우 움직임 Input, 둘중에 하나만 사용 해야함")]
        public JoystickInput JoystickInput;
        public Accelerometer Accelerometer;
        [Header("플레이어 캐릭터 좌우 움직임에 조이스틱 사용")]
        public bool UseJoystick;
        private IInput userInput;


        //카메라 쉐이커
        private ICameraShaker cameraShaker;

        [Header("[플레이어의 모든 정보를 가지고 있는 프리팹]")]
        public PlayerConfig[] playerConfigs;

        [Header("[플레이어생성 이후 활성 비활성 시키는 컨트롤러]")]
        public PlayersController PlayersController;

        //플레이어
        private Player player;

        public delegate void Look(Vector3 pos);
        public Look lookDelegate;

        public UnityAction<Vector3> lookAction;

        //캐릭터가 생성 됐는지 아닌지 여부 확인을 위해 사용.
        //캐릭터의 갯수와 상관없이 Initialize 가 한번이라도 호출됐다면 체크됨.
        private bool mbInitialized = false;
        public bool IsInitialzed { get => mbInitialized; set => mbInitialized = value; }

        //이벤트
        #region [-캐쉬용]
        GameObject clone = null;
        #endregion

        //private void Awake()
        //{
        //    cameraShaker = Shaker.GetComponent<ICameraShaker>();
        //}

        #region [-TestCode]
        //private void Start()
        //{
        //    Initialize();
        //}

        /// <summary>
        /// 초기화
        /// 실제 캐릭터를 생성함.
        /// </summary>
        public void Initialize()
        {
            //초기화 체크
            //캐릭터의 갯수와 상관없이 Initialize 가 한번이라도 호출됐다면 체크됨.
            IsInitialzed = true;

            //플레어 포인트를 루트에서 가져옴
            PlayerPoints = PlayerPointRoot.GetChilds("Point");

            PlayersController.Players.Add( Make(PlayerPoints[0], playerConfigs[0]).GetComponent<Player>() );
            //2명의 플레이어를 더 만들수 있는 구조로 되어 있음
            //PlayersController.Players.Add( Make(PlayerPoints[1], playerConfigs[1]).GetComponent<Player>() );
            //PlayersController.Players.Add( Make(PlayerPoints[2], playerConfigs[1]).GetComponent<Player>() );

            //강제로 캐릭터의 조준점 마추는 코드 실행
            //임시 임으로 반드시 삭제가 필요.
            Invoke("FocusAim", 1f);
        }

        void FocusAim()
        {
            WoosanStudio.ZombieShooter.Test.TestCode01.Instance.Swap();
        }
        #endregion


        /// 플레이어 생성
        public GameObject Make(Transform parent, PlayerConfig playerConfig)
        {
            clone = Instantiate(playerConfig.prefab) as GameObject;
            player = clone.GetComponent<Player>();

            //조이스틱 사용 값 받아오기
            UseJoystick = playerConfig.useJoystick;

            //좌우 움직임용 인풋 인터페이스 세팅
            userInput = UseJoystick ? (IInput)JoystickInput : (IInput)Accelerometer;

            //AI 미 사용시 = 터치스크린 사용
            //자동 조
            ILookPoint lookPoint = playerConfig.useAI ? (ILookPoint)MoveScreenPointToRayPosition :
                (ILookPoint)AutoAim;

            //실제 처다보게 하는 스크립트
            //에르고가 높다면 회전이 좋게 만들기
            lookAction = clone.GetComponent<LookAhead>().SmoothLook;

            //AI사용시
            if (playerConfig.useAI)
            {
                //자동 조준 action 연결
                AutoAim.UpdatePositionEvent.AddListener(lookAction);
                
            } else //유저 직접 조정시
            {
                //마우스 터치 조준 action 연결
                MoveScreenPointToRayPosition.UpdatePositionEvent.AddListener(lookAction);
                
            }

            //구르기 사용시
            if(playerConfig.useRoll)
            {
                //덤블링 스크립트.
                DoRoll doRoll = clone.GetComponentInChildren<DoRoll>();
                //덤블링 스크립트와 좌우 화면 터치 이벤트 연결
                doRoll.ConnectEvents(FindObjectOfType<TouchController>());
            }


            //그림자 사용여부에 따라 그림자 활성화
            if(playerConfig.useShadow)
            {
                GameObject shadowProjector = Instantiate(playerConfig.ShadowProjector);
                shadowProjector.transform.parent = clone.transform;
            }

            //플레이어 초기화
            //player.Initialize(WeaponFactory, cameraShaker ,ref lookAction ,ref lookPoint , !playerConfig.useAI);
            player.Initialize(WeaponFactory, cameraShaker, ref lookAction, ref lookPoint, playerConfig , userInput);
            clone.transform.parent = parent;
            clone.transform.Reset(Quaternion.Euler(0,270,0));

            return clone;
        }
    }
}
