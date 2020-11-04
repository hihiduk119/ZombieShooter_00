using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 도박 최종 알림 통지
    /// *MVP 모델
    /// </summary>
    public class UINotifyGambleView : MonoBehaviour
    {
        [Header("[성공 겜블 레벨]")]
        public Text SuccessLevel;

        [Header("[요구 젬]")]
        public Text RequireGem;

        [Header("[겜블 성공률]")]
        public Text SuccessRate;

        //[Header("[실행 이벤트]")]
        //public UnityEvent YesEvent = new UnityEvent();

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        /// <param name="successLevel"></param>
        /// <param name="requireGem"></param>
        /// <param name="successRate"></param>
        public void UpdateView(string successLevel,string requireGem, string successRate)
        {
            SuccessLevel.text = successLevel;
            RequireGem.text = requireGem;
            SuccessRate.text = successRate;
        }
    }
}
