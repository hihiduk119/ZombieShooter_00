using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 버튼 활성 비활성 컨트롤
    /// </summary>
    public class UIStartButtonPresenter : MonoBehaviour
    {
        [Header("[MVP 뷰]")]
        public UIStartButtonView View;

        //캐릭터 사용 가능
        bool useAbleCharacter = false;
        //탄야 사용 가능
        bool useAbleAmmo = false;
        //총 사용 가능
        bool useAbleGun = false;

        /// <summary>
        /// 캐릭터 사용 가능 여부 업데이트
        /// </summary>
        /// <param name="value"></param>
        public void UpdateValueFromCharacter(bool value)
        {
            useAbleCharacter = value;
            //실제 뷰를 업데이트
            UpdateValue();
        }

        /// <summary>
        /// 탄약 사용 가능 여부 업데이트
        /// </summary>
        /// <param name="value"></param>
        public void UpdateValueFromAmmo(bool value)
        {
            useAbleAmmo = value;
            //실제 뷰를 업데이트
            UpdateValue();
        }

        /// <summary>
        /// 총 사용 가능 여부 업데이트
        /// </summary>
        /// <param name="value"></param>
        public void UpdateValueFromGun(bool value)
        {
            useAbleGun = value;
            //실제 뷰를 업데이트
            UpdateValue();
        }

        /// <summary>
        /// 실제 뷰 업데이트
        /// </summary>
        private void UpdateValue()
        {
            //캐릭터 , 탄약, 총 3개 모두 true 일때만 최종 true전달
            if(useAbleCharacter && useAbleAmmo && useAbleGun)
            {
                View.UpdateValue(true);
            } else//아니면 false
            {
                View.UpdateValue(false);
            }
        }
    }
}
