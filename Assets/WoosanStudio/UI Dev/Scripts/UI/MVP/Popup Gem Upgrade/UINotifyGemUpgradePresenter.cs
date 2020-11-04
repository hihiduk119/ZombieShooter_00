using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 젬 업글 최종 알림 통지
    /// *MVP 모델
    /// </summary>
    public class UINotifyGemUpgradePresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UINotifyGemUpgradeView View;

        [Header("[Auto -> 데이터 전달받음]")]
        public CardSetting CardSetting;

        /// <summary>
        /// 활성화시 바로 업데이트
        /// </summary>
        private void OnEnable()
        {
            UpdateView();
        }

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        private void UpdateView()
        {
            View.UpdateView(CardSetting.UpgradeComplateLevelToString(CardSetting)
                , CardSetting.RequireGemToString(CardSetting));
        }
    }
}
