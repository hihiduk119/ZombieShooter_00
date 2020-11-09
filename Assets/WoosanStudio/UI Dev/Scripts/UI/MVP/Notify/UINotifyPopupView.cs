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
    public class UINotifyPopupView : MonoBehaviour
    {
        [Header("[설명]")]
        public Text Description;

        /// <summary>
        /// 화면 업데이트
        /// </summary>
        public void UpdateView(string description)
        {
            Description.text = description;   
        }
    }
}
