using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter.UI.MVP
{
    /// <summary>
    /// 스테이지 결과 팝업
    /// *MVP 모델
    /// </summary>
    public class InGameStageResultPopupPresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public InGameStageResultPopupView View;

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        public void UpdateInfo(string description, string price)
        {
            //View.UpdateView()
        }
    }
}
