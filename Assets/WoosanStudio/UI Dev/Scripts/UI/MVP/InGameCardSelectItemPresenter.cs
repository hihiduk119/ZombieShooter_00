using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter.UI.MVP
{
    /// <summary>
    /// 카드 선택 아이템
    /// *MVP 모델
    /// </summary>
    public class InGameCardSelectItemPresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public InGameCardSelectPopupView View;

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        public void UpdateInfo(string description, string price)
        {
            //View.UpdateView()
        }
    }
}
