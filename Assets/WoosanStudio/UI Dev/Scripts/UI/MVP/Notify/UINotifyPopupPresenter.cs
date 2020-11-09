using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 확인 최종 통지
    /// *MVP 모델
    /// </summary>
    public class UINotifyPopupPresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UINotifyPopupView View;
        [Header("[설명]")]
        public UIShopChargesAllEnergyPopupModel.Data data;

        /// <summary>
        /// 팝업 활성화시 바로 실행하기 위해
        /// </summary>
        private void OnEnable()
        {
            //활성화시 바로 실행
            UpdateInfo(data.Description_1);
        }

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        public void UpdateInfo(string description)
        {
            View.UpdateView(description);
        }
    }
}
