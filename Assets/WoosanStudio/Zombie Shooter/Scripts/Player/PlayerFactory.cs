using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WoosanStudio.Extension;
using WoosanStudio.Common;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 플레이어를 생성하는 부분
    /// </summary>
    public class PlayerFactory : MonoBehaviour
    {
        [Header("[무기를 만들어주는 팩토리 패턴 적용.]")]
        public WeaponFactory WeaponFactory;
        
        [Header("[조이스틱에 의해 화면 움직임 담당 [Auto-Awake()]]")]
        public FollowCameraTarget FollowCameraTarget;

        [Header("[(AI 미 사용시)[플레이어 터치 위치에 따른 회전]")]
        public MoveTouchPointToRayPosition MoveTouchPointToRayPosition;

        [Header("[플레이어 오브젝트 정보]")]
        public PlayerConfig playerConfig;

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
            //하나만 존제 하기에 파인드 세팅
            if(FollowCameraTarget == null) FollowCameraTarget = GameObject.FindObjectOfType<FollowCameraTarget>();
        }

        #region [-TestCode]
        /// <summary>
        /// 초기화
        /// 실제 캐릭터를 생성함.
        /// </summary>
        public GameObject Initialize()
        {
            //초기화 체크
            //캐릭터의 갯수와 상관없이 Initialize 가 한번이라도 호출됐다면 체크됨.
            IsInitialzed = true;

            return Make(null, playerConfig);
            //강제로 캐릭터의 조준점 마추는 코드 실행
            //임시 임으로 반드시 삭제가 필요.
            //Invoke("FocusAim", 1f);
        }
        #endregion


        /// 플레이어 생성
        public GameObject Make(Transform parent, PlayerConfig playerConfig)
        {
            clone = Instantiate(playerConfig.prefab) as GameObject;

            //플레이어 포지셔너 와 스테이지 메니져 연결
            //스테이지 메니저는 매 스테이지의 컨트롤을 담당하기에 필요.
            StageManager.Instance.PlayerPositioner = clone.GetComponent<Positioner>();

            //FollowCamTarget.cs 와 플레이어 연결.
            //플레이어 중심으로 화면 고정 시킴
            FollowCameraTarget.player = clone;


            //그림자 사용여부에 따라 그림자 활성화
            //*사용 안하는거 같은데 제거할지 말지 결정 해야함.
            if (playerConfig.useShadow)
            {
                GameObject shadowProjector = Instantiate(playerConfig.ShadowProjector);
                shadowProjector.transform.parent = clone.transform;
            }

            //부모지
            clone.transform.parent = parent;
            clone.transform.Reset(Quaternion.Euler(0,270,0));

            return clone;
        }
    }
}
