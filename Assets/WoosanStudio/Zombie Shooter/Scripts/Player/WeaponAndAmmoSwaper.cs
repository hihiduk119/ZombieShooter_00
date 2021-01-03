using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter.Player
{
    /// <summary>
    /// 플레이어 무기 변경
    /// </summary>
    public class WeaponAndAmmoSwaper : MonoBehaviour
    {
        [Header("[무기 변경 컨트롤러]")]
        public WeaponRequester WeaponRequester;

        [Header("[[Auto] 무기 카드 확인용]")]
        public CardSetting WeanponCardSetting;

        [Header("[[Auto] 탄약 카드 확인용]")]
        public CardSetting AmmoCardSetting;
        /// <summary>
        /// 탄약 변경 명령
        /// </summary>
        public void Swap(CardSetting weanponCardSetting, CardSetting ammoCardSetting)
        {
            //기존 무기 제거
            WeaponRequester.Remove();
            //무기, 탄약 새로 할당
            WeaponRequester.Anchor(weanponCardSetting, ammoCardSetting);
        }


        #region [-TestCode] 
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                //캐릭터 세팅
                WeaponRequester.Remove();
                WeaponRequester.Anchor(0, 0);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                //캐릭터 세팅
                WeaponRequester.Remove();
                WeaponRequester.Anchor(2, 0);
            }
        }
        #endregion
    }
}
