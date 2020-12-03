using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 모든 연구 슬롯
    /// MVP 모델
    /// </summary>
    public class UICardUpgradeSlotPopupView : MonoBehaviour
    {
        [Header("[업그레이드 정보]")]
        public Text text;

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        public void UpdateInfo(int current,int max)
        {
            text.text = string.Format("{0} / {1}", current, max);
        }
    }
}
