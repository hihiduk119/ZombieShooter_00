using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 에너지 샵 프리젠트
    /// *MVP 모델
    /// </summary>
    public class UIShopChargesAllEnergyPopupPresenter : MonoBehaviour
    {
        [Header("[MVP Model]")]
        public UIShopChargesAllEnergyPopupModel Model;
        [Header("[MVP View]")]
        public UIShopChargesAllEnergyPopupView View;

        /// <summary>
        /// 팝업 활성화시 바로 실행하기 위해
        /// </summary>
        private void OnEnable()
        {
            //활성화시 바로 실행
            UpdateInfo();
        }

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        public void UpdateInfo()
        {
            string price = "BUY $" + Model.data.Price.ToString();

            View.UpdateView(Model.data.Description_0, price);
        }
    }
}
