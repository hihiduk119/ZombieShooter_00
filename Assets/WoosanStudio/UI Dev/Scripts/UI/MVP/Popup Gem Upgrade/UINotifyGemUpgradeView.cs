using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 젬 업글 최종 알림 통지
    /// *MVP 모델
    /// </summary>
    public class UINotifyGemUpgradeView : MonoBehaviour
    {
        [Header("[목표 레벨]")]
        public Text TargetLevel;

        [Header("[요구 젬]")]
        public Text RequireGem;

        //[Header("[실행 이벤트]")]
        //public UnityEvent YesEvent = new UnityEvent();

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        /// <param name="successLevel"></param>
        /// <param name="requireGem"></param>
        /// <param name="successRate"></param>
        public void UpdateView(string targetLevel, string requireGem)
        {
            TargetLevel.text = targetLevel;
            RequireGem.text = requireGem;
        }
    }
}
