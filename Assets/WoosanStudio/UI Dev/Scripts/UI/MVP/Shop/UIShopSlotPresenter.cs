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
        /// 정보 업데이트
        /// </summary>
        /// <param name="data"></param>
        public void UpdateInfo()
        {
            string gainValue = Model.data.GainValue.ToString();
            string description = Model.data.Description;
            string oldPrice = "$" + Model.data.OldPrice.ToString();
            string price = "$" + Model.data.Price.ToString();

            View.UpdateInfo(Model.data.Image,Model.data.GainValueImage,gainValue,description,oldPrice,price);
        }
    }
}
