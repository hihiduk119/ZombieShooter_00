using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 확인 최종 통지
    /// *MVP 모델
    /// </summary>
    public class UINotifyPurchaseEnergyPopupView : MonoBehaviour
    {
        [Header("[설명]")]
        public Text Description;

        [Header("[가격")]
        public Text Price;

        /// <summary>
        /// 화면 업데이트
        /// </summary>
        public void UpdateView(string description, string price)
        {
            Description.text = description;
            Price.text = price;
        }
    }
}
