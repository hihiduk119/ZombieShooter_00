using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter.UI
{
    /// <summary>
    /// 백 프리젠트
    /// *MVP 모델
    /// </summary>
    public class UIBagPresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UIBagView View;

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        public void UpdateInfo()
        {
            View.UpdateInfo("0", "0", "0");
        }
    }
}
