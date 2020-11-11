using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 확인 최종 통지
    /// *MVP 모델
    /// </summary>
    public class UINotifyPurchaseEnergyPopupPresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UINotifyPurchaseEnergyPopupView View;
        [Header("[[Auto => Linker에서 전달받음]]")]
        public UIShopChargesAllEnergyPopupModel.Data data;

        /// <summary>
        /// 팝업 활성화시 바로 실행하기 위해
        /// </summary>
        private void OnEnable()
        {
            string price = "[$" + data.Price.ToString() + "]";
            //활성화시 바로 실행
            UpdateInfo(data.Description_1, price);
        }

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        public void UpdateInfo(string description, string price)
        {
            View.UpdateView(description, price);
        }

        /// <summary>
        /// Yes 버튼 클릭
        /// </summary>
        public void Send()
        {
            //실제 구매 컨트롤에 연결.
            PurchaseController.Instance.BuyEnergy(data);
        }
    }
}
