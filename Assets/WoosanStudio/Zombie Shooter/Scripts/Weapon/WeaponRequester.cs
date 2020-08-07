using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 웨폰 팩토리에 무기를 요청함
    /// </summary>
    public class WeaponRequester : MonoBehaviour
    {
        [Header("[생성할 무기 인덱스]")]
        public int WeaponIndex = 0;
        [Header("[무기를 생성 (Auto-Awake())]")]
        public WeaponFactory WeaponFactory;
        [Header("[레이저포인터 생성 (Auto-Awake())]")]
        public LaserPointerFactory LaserPointerFactory;
        [Header("[머즐 플래쉬 생성 (Auto-Awake())]")]
        public MuzzleFlashFactory MuzzleFlashFactory;
        [Header("[사격시 카메라 쉐이킹 (Auto-Awake())]")]
        public CameraShakeProjectile CameraShakeProjectile;
        [Header("[리로드 애니메이션 수행 (Auto-Awake())]")]
        public PlayAnimation PlayAnimation;

        [Header("[무기&레이저 포인터를 생성시킬 앵커]")]
        public Transform WeaponAnchor;

        IStart start;
        IEnd end;
        //List<IReloadAction> reloadActionList;
        ICameraShaker cameraShaker;
        IGun gun;

        //IStart,IEnd 를 가지고 있음
        public FireController FireController;
        //IStart,IEnd 를 가지고 있음 => 기존 FireController을 이걸로 교체
        [Header("[사격시 컨트롤러 (Auto-Awake())]")]
        public AutoFireControlInputBasedOnGunSetting AutoFireControlInputBasedOnGunSetting;

        private IStart fireStartEvent;
        private IEnd fireEndEvent;
        private IReload reloadEvent;

        private void Awake()
        {
            //최초 세팅이 안되어 있다면 파인드 오브젝트로 가져옴.
            //*나중에 싱글톤으로 바꿀지 말지 결정.
            if (WeaponFactory == null) WeaponFactory = GameObject.FindObjectOfType<WeaponFactory>();
            if (LaserPointerFactory == null) LaserPointerFactory = GameObject.FindObjectOfType<LaserPointerFactory>();
            if (MuzzleFlashFactory == null) MuzzleFlashFactory = GameObject.FindObjectOfType<MuzzleFlashFactory>();
            if (CameraShakeProjectile == null) CameraShakeProjectile = GameObject.FindObjectOfType<CameraShakeProjectile>();
            PlayAnimation = GetComponent<PlayAnimation>();

            //사격 통제 인터페이스 가져옴
            //fireStartEvent = (IStart)FireController;
            //fireEndEvent = (IEnd)FireController;
            AutoFireControlInputBasedOnGunSetting = GetComponent<AutoFireControlInputBasedOnGunSetting>();
            fireStartEvent = (IStart)AutoFireControlInputBasedOnGunSetting;
            fireEndEvent = (IEnd)AutoFireControlInputBasedOnGunSetting;
            reloadEvent = (IReload)AutoFireControlInputBasedOnGunSetting;
        }

        /// <summary>
        /// 실제 연결
        /// </summary>
        public void Anchor()
        {
            //제대로 넣기
            //WeaponFactory.MakeWeapon(start, end, cameraShaker, reloadActionList,ref gun, WeaponAnchor, 0, false, null);
            //야매로 넣기
            // * MuzzleFlashFactory에서 생성 및 앵커에 연결하고 초기화까지 함

            //재장전 시작시 작동할 액션 리스트
            //List<IReloadAction> startReloadActionList = new List<IReloadAction>();
            List<UnityAction> startReloadActionList = new List<UnityAction>();
            startReloadActionList.Add(()=> { Debug.Log("Start Reload");});
            //자동 사격 시스템의 리로드 액션 넣기
            startReloadActionList.Add(AutoFireControlInputBasedOnGunSetting.ReloadAction);
            //리로드시 에니메이션 플레이
            startReloadActionList.Add(PlayAnimation.Player);
            //startReloadActionList.Add();
            // 해당 리로드에 자동 사젹에서 리로딩 넣어야

            //재장전 끝났을때 작동할 액션 리스트
            //List<IReloadAction> endReloadActionList = new List<IReloadAction>();
            List<UnityAction> endReloadActionList = new List<UnityAction>();

            //FireController 의 사격 시작 및 중지 이벤트연
            //*(IStart)FireController => 사격 시작 인터페이스 연결
            //*(IEnd)FireController => 사격 중지 인터페이스 연결
            //*(ICameraShaker)CameraShakeProjectile => 화면 흔들림 연출 이벤트 연결
            WeaponFactory.MakeWeapon(fireStartEvent, fireEndEvent, (ICameraShaker)CameraShakeProjectile,
                startReloadActionList, endReloadActionList, ref gun, WeaponAnchor, WeaponIndex, false, MuzzleFlashFactory.Make(WeaponAnchor));
            //레이저 포인터 생성전에 로컬좌표 설정 & 엔커 설정
            LaserPointerFactory.Anchor = WeaponAnchor;
            LaserPointerFactory.InitPosition = WeaponFactory._gunSettings[WeaponIndex].InitLaserPointerPosition;
            LaserPointerFactory.Make();
        }

        #region [-TestCode]
        
        void Update()
        {
            //웨폰 팩토리에서 만든 무기와 인터페이스 연결
            if (Input.GetKeyDown(KeyCode.M))
            {
                Anchor();
            }
        }
        #endregion
    }
}
