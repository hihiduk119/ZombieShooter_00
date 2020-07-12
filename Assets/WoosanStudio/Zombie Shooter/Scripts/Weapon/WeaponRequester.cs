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
        [Header("[무기를 생성]")]
        public WeaponFactory WeaponFactory;
        [Header("[레이저포인터 생성]")]
        public LaserPointerFactory LaserPointerFactory;
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

        /// <summary>
        /// 실제 연결
        /// </summary>
        public void Anchor()
        {
            //제대로 넣기
            //WeaponFactory.MakeWeapon(start, end, cameraShaker, reloadActionList,ref gun, WeaponAnchor, 0, false, null);
            //야매로 넣기
            WeaponFactory.MakeWeapon((IStart)FireController, (IEnd)FireController, null, null,ref gun, WeaponAnchor, WeaponIndex, false, null);
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
