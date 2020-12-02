using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 업그레이드 슬롯 추가 팝업
    /// *MVP 모델
    /// </summary>
    public class UINotifyUpgradeSlotAddView : MonoBehaviour
    {
        [Header("[요구 코인]")]
        public Text RequireCoin;

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        /// <param name="requireCoin"></param>
        public void UpdateView(string requireCoin)
        {
            RequireCoin.text = requireCoin;
        }
    }
}
