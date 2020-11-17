using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드 연구 완료 팝업
    ///  *MVP패턴
    /// </summary>
    public class UICardUpgradeResultPopupView : MonoBehaviour
    {
        [Header("[업그레이드 결과 내용]")]
        public Text Result;
        [Header("[레벨]")]
        public Text Level;

        /// <summary>
        /// 결과 정보 업데이트
        /// </summary>
        /// <param name="value"></param>
        public void UpdateResult(string value, string level,Color levelColor)
        {
            Result.text = value;
            Level.text = level;
            Level.color = levelColor;
        }
    }
}
