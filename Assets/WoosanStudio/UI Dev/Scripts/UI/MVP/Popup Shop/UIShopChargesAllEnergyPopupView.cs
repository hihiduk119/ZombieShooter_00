using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 에너지 샵 뷰
    /// *MVP 모델
    /// </summary>
    public class UIShopChargesAllEnergyPopupView : MonoBehaviour
    {
        [Header("[설명]")]
        public Text Description;

        [Header("[버튼 설명]")]
        public Text Button;

        /// <summary>
        /// 화면 업데이트
        /// </summary>
        public void UpdateView(string description,string button)
        {
            Description.text = description;
            Button.text = button;
        }
    }
}
