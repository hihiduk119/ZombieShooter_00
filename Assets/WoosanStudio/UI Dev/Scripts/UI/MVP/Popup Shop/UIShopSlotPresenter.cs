using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 샵 슬롯 프리젠트
    /// *MVP 모델
    /// </summary>
    public class UIShopSlotPresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UIShopSlotView View;

        [Header("[MVP Model]")]
        public UIShopSlotModel Model;

        /// <summary>
        /// 활성화시 바로 업데이트
        /// </summary>
        private void OnEnable()
        {
            UpdateInfo();
        }

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        /// <param name="data"></param>
        public void UpdateInfo()
        {
            string description = Model.data.Description;
            string oldPrice = "$" + Model.data.OldPrice.ToString();
            string price = "$" + Model.data.Price.ToString();
            bool useOldPrice = true;
            bool useAds = false;
            //할인가격이 0이면 사용 안함
            if(Model.data.OldPrice == 0) { useOldPrice = false; }

            //획득 방식이 ADS 라면
            if (Model.data.gain == UIShopSlotModel.Gain.ADS) { useAds = true; }

            View.UpdateInfo(Model.data.Image,Model.data.GainValueImage,
                Model.data.GainValue, description,oldPrice,price, useOldPrice , useAds);
        }
    }
}
