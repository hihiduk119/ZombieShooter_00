using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 코인 업글 최종 알림 통지
    /// *MVP 모델
    /// </summary>
    public class UINotifyCoinUpgradeView : MonoBehaviour
    {
        [Header("[목표 레벨]")]
        public Text TargetLevel;

        [Header("[요구 코인]")]
        public Text RequireCoin;

        [Header("[요구 시간]")]
        public Text RequireTime;

        //[Header("[실행 이벤트]")]
        //public UnityEvent YesEvent = new UnityEvent();

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        /// <param name="successLevel"></param>
        /// <param name="requireGem"></param>
        /// <param name="successRate"></param>
        private void UpdateView(string targetLevel, string requireCoin, string requireTime)
        {
            TargetLevel.text = targetLevel;
            RequireCoin.text = requireCoin;
            RequireTime.text = requireTime;
        }
    }
}
