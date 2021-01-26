using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter.UI.MVP
{
    /// <summary>
    /// 카드 선택 아이템
    /// *MVP 모델
    /// </summary>
    public class UIWaveCountView : MonoBehaviour
    {
        [Header("[카드 퍼센트")]
        public Text Count;

        /// <summary>
        /// 화면 업데이트
        /// </summary>
        /// <param name="count">카운팅 값</param>
        public void UpdateView(string count)
        {
            Count.text = count;
        }
    }
}
