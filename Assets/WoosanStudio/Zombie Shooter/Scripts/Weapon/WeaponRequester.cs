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
        [Header("[카메라 줌 수행 (Auto-Awake())]")]
        public CameraZoom CameraZoom;
        [Header("[무기&레이저 포인터를 생성시킬 앵커]")]
        public Transform WeaponAnchor;

        IStart start;
        IEnd end;
        //List<IReloadAction> reloadActionList;
        ICameraShaker cameraShaker;
        IGun gun;

        //IStart,IEnd 를 가지고 있음
        //public FireController FireController;
        //IStart,IEnd 를 가지고 있음 => 기존 FireController을 이걸로 교체
        [Header("[사격시 컨트롤러 (Auto-Awake())]")]
        public AutoFireControlInputBasedOnGunSetting AutoFireControlInputBasedOnGunSetting;

        private IStart fireStartEvent;
        private IEnd fireEndEvent;
        private IReload reloadEvent;

        //무기 삭제를 위해 작업

        //재장전 시작시 작동할 액션 리스트
        //* 무기가 변경 될때마다 삭제
        List<UnityAction> startReloadActionList = new List<UnityAction>();

        //재장전 끝났을때 작동할 액션 리스트
        //* 무기가 변경 될때마다 삭제
        List<UnityAction> endReloadActionList = new List<UnityAction>();

        //현재 무기
        IWeapon currentWeapon;

        //현재 무기의 레이저 포인터
        GameObject currentLaserPointer;

        //현재 머즐 플레쉬
        GameObject currentMuzzleFlash;

        private void Awake()
        {
            //최초 세팅이 안되어 있다면 파인드 오브젝트로 가져옴.
            //*나중에 싱글톤으로 바꿀지 말지 결정.
            if (WeaponFactory == null) WeaponFactory = GameObject.FindObjectOfType<WeaponFactory>();
            if (LaserPointerFactory == null) LaserPointerFactory = GameObject.FindObjectOfType<LaserPointerFactory>();
            if (MuzzleFlashFactory == null) MuzzleFlashFactory = GameObject.FindObjectOfType<MuzzleFlashFactory>();
            if (CameraShakeProjectile == null) CameraShakeProjectile = GameObject.FindObjectOfType<CameraShakeProjectile>();
            PlayAnimation = GetComponent<PlayAnimation>();
            CameraZoom = GameObject.FindObjectOfType<CameraZoom>();

            //사격 통제 인터페이스 가져옴
            //fireStartEvent = (IStart)FireController;
            //fireEndEvent = (IEnd)FireController;
            AutoFireControlInputBasedOnGunSetting = GetComponent<AutoFireControlInputBasedOnGunSetting>();
            fireStartEvent = (IStart)AutoFireControlInputBasedOnGunSetting;
            fireEndEvent = (IEnd)AutoFireControlInputBasedOnGunSetting;
            reloadEvent = (IReload)AutoFireControlInputBasedOnGunSetting;
        }

        /// <summary>
        /// 무기 실제 연결
        /// </summary>
        public void Anchor(int WeaponIndex)
        {
            //제대로 넣기
            //WeaponFactory.MakeWeapon(start, end, cameraShaker, reloadActionList,ref gun, WeaponAnchor, 0, false, null);
            //야매로 넣기
            // * MuzzleFlashFactory에서 생성 및 앵커에 연결하고 초기화까지 함

            //재장전 시작시 작동할 액션 리스트
            //List<UnityAction> startReloadActionList = new List<UnityAction>();

            startReloadActionList.Add(()=> { Debug.Log("Start Reload");});
            //자동 사격 시스템의 리로드 액션 넣기
            //startReloadActionList.Add(AutoFireControlInputBasedOnGunSetting.ReloadAction);
            //리로드시 에니메이션 플레이
            startReloadActionList.Add(PlayAnimation.Play);

            //리로드시 카메라 줌 수행
            // => 일단 제거
            CameraZoom.ZoomIndex = 1;
            startReloadActionList.Add(CameraZoom.AutoZoomOut);

            //자동 사격 컨트롤러의 재장전 시작 이벤트에 리로드 시작 등록
            startReloadActionList.ForEach(value => ((IReload)AutoFireControlInputBasedOnGunSetting).StartReloadEvent.AddListener(value));

            //재장전 끝났을때 작동할 액션 리스트
            //List<UnityAction> endReloadActionList = new List<UnityAction>();

            //리로드 끝시 카메라 아웃 수행
            // => 일단 제거
            endReloadActionList.Add(CameraZoom.ZoomIn);

            //자동 사격 컨트롤러의 재장전 끝 이벤트에 리로드 끝 등록
            endReloadActionList.ForEach(value => ((IReload)AutoFireControlInputBasedOnGunSetting).EndReloadEvent.AddListener(value));

            //머즐 플레쉬 생성
            //WeaponFactory.MakeWeapon() 인수로 사용
            currentMuzzleFlash = MuzzleFlashFactory.Make(WeaponAnchor);

            //FireController 의 사격 시작 및 중지 이벤트연
            //*(IStart)FireController => 사격 시작 인터페이스 연결
            //*(IEnd)FireController => 사격 중지 인터페이스 연결
            //*(ICameraShaker)CameraShakeProjectile => 화면 흔들림 연출 이벤트 연결
            //생성 후 삭제를 위해 미리 받아놓음
            currentWeapon = WeaponFactory.MakeWeapon(fireStartEvent, fireEndEvent, (ICameraShaker)CameraShakeProjectile,
                AutoFireControlInputBasedOnGunSetting.ReloadAction, ref gun, WeaponAnchor, WeaponIndex, false, currentMuzzleFlash);
            //레이저 포인터 생성전에 로컬좌표 설정 & 엔커 설정
            LaserPointerFactory.Anchor = WeaponAnchor;
            LaserPointerFactory.InitPosition = WeaponFactory._gunSettings[WeaponIndex].InitLaserPointerPosition;
            //생성 후 삭제를 위해 미리 받아놓음
            currentLaserPointer = LaserPointerFactory.Make();
        }

        /// <summary>
        /// 연결된 무기 삭제
        /// </summary>
        public void Remove()
        {
            //자동 사격 컨트롤러의 재장전 시작 이벤트에 리로드 시작 등록
            startReloadActionList.ForEach(value => ((IReload)AutoFireControlInputBasedOnGunSetting).StartReloadEvent.RemoveListener(value));

            //자동 사격 컨트롤러의 재장전 끝 이벤트에 리로드 끝 등록
            endReloadActionList.ForEach(value => ((IReload)AutoFireControlInputBasedOnGunSetting).EndReloadEvent.RemoveListener(value));

            //무기 삭제
            if(currentWeapon != null) {
                Destroy(currentWeapon.GetInstnace());
            }
            else
            {
                Debug.Log("===============================> 무기 삭제 안됨");
            }

            //레이저 포인터 삭제
            if (currentLaserPointer != null) {
                Destroy(currentLaserPointer);
            }
            else
            {
                Debug.Log("===============================> 레이저 포인터 삭제 안됨");
            }

            //머즐 플레쉬 삭제
            if(currentMuzzleFlash != null) {
                Destroy(currentMuzzleFlash);
            }
            else
            {
                Debug.Log("===============================> 머즐 플레쉬 삭제 안됨");
            }
        }

        #region [-TestCode]
        
        void Update()
        {
            //웨폰 기존 무기 삭제
            if (Input.GetKeyDown(KeyCode.M))
            {
                Remove();
                Anchor(0);
            }

            //웨폰 팩토리에서 만든 무기와 인터페이스 연결
            if (Input.GetKeyDown(KeyCode.N))
            {
                Remove();
                Anchor(1);
            }

            //웨폰 팩토리에서 만든 무기와 인터페이스 연결
            if (Input.GetKeyDown(KeyCode.B))
            {
                Remove();
                Anchor(2);
            }
        }
        #endregion
    }
}
