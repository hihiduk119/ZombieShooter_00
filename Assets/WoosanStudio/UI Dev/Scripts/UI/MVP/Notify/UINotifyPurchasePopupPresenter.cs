using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 구매 최종 통지
    /// *MVP 모델
    /// </summary>
    public class UINotifyPurchasePopupPresenter : MonoBehaviour
    {
        [Header ("[MVP View]")]
        public UINotifyPurchasePopupView View;

        [Header("[[Auto] Linker를 통해 데이터 전달 받음]")]
        public UIShopSlotModel.Data data;

        /// <summary>
        /// 팝업 활성화시 바로 실행하기 위해
        /// </summary>
        private void OnEnable()
        {
            //활성화시 바로 실행
            UpdateInfo(this.data);
        }

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        public void UpdateInfo(UIShopSlotModel.Data data)
        {
            string price = "$" + data.Price.ToString();

            string gainValue;
            if (data.GainValue < 10) { gainValue = data.GainValue.ToString();
            } else { gainValue = string.Format("{0:0,0}", data.GainValue);}
            View.UpdateView(data.Image, data.GainValueImage, gainValue, price);
        }
    }
}
