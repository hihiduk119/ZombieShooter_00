using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WoosanStudio.Extension;

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
        [Header("[플레이어 생성 위치]")]
        public List<Transform> PlayerPoints;
        [Header("[(AI 미 사용시)[플레이어 터치 위치에 따른 회전]")]
        public MoveScreenPointToRayPosition MoveScreenPointToRayPosition;
        [Header("[(AI 사용시) 가장 가까운 몬스터 자동 회전]")]
        public AutoAim AutoAim;

        //카메라 쉐이커
        private ICameraShaker cameraShaker;

        [Header("[플레이어의 모든 정보를 가지고 있는 프리팹]")]
        public PlayerConfig[] playerConfigs;

        //플레이어
        private Player player;

        public delegate void Look(Vector3 pos);
        public Look lookDelegate;

        public UnityAction<Vector3> lookAction;

        //이벤트
        #region [-캐쉬용]
        GameObject clone = null;
        #endregion

        private void Awake()
        {
            //
            //cameraShaker = Shaker.GetComponent<ICameraShaker>();
        }

        #region [-TestCode]
        //private void Start()
        //{
        //    Initialize();
        //}

        public void Initialize()
        {
            Make(PlayerPoints[0], playerConfigs[0]);
            //Make(PlayerPoints[1], playerConfigs[1]);
            //Make(PlayerPoints[2], playerConfigs[1]);
        }
        #endregion


        /// 플레이어 생성

        public GameObject Make(Transform parent, PlayerConfig playerConfig)
        {
            clone = Instantiate(playerConfig.prefab) as GameObject;
            player = clone.GetComponent<Player>();

            //AI 사용시 = 자동 몬스터 조준 사용
            //AI 미 사용시 = 터치스크린 사용
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
            player.Initialize(WeaponFactory, cameraShaker, ref lookAction, ref lookPoint, playerConfig);
            clone.transform.parent = parent;
            clone.transform.Reset(Quaternion.Euler(0,270,0));

            return clone;
        }
    }
}
