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
        public WeaponFactory WeaponFactory;

        [Header("[무기를 생성시킬 앵커]")]
        public Transform WeaponAnchor;

        IStart start;
        IEnd end;
        List<IReloadAction> reloadActionList;
        ICameraShaker cameraShaker;
        IGun gun;


        //IStart,IEnd 를 가지고 있음
        public Test.TestFireController TestFireController;

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
            WeaponFactory.MakeWeapon((IStart)TestFireController, (IEnd)TestFireController, null, null,ref gun, WeaponAnchor, 0, false, null);
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
