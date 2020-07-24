using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        [Header("[무기&레이저 포인터를 생성시킬 앵커]")]
        public Transform WeaponAnchor;

        IStart start;
        IEnd end;
        List<IReloadAction> reloadActionList;
        ICameraShaker cameraShaker;
        IGun gun;

        //IStart,IEnd 를 가지고 있음
        public FireController FireController;

        //ICameraShaker cameraShaker;
        //IGun gun;

        private void Awake()
        {
            //최초 세팅이 안되어 있다면 파인드 오브젝트로 가져옴.
            //*나중에 싱글톤으로 바꿀지 말지 결정.
            if (WeaponFactory == null) WeaponFactory = GameObject.FindObjectOfType<WeaponFactory>();
            if (LaserPointerFactory == null) LaserPointerFactory = GameObject.FindObjectOfType<LaserPointerFactory>();
            if (MuzzleFlashFactory == null) MuzzleFlashFactory = GameObject.FindObjectOfType<MuzzleFlashFactory>();
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
            WeaponFactory.MakeWeapon((IStart)FireController, (IEnd)FireController, null, null,ref gun, WeaponAnchor, WeaponIndex, false, MuzzleFlashFactory.Make(WeaponAnchor));
            //레이저 포인터 생성전에 로컬좌표 설정
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
