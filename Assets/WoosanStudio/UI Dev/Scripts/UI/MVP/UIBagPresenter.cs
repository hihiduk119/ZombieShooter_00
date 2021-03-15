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

        private void Awake()
        {
            this.UpdateInfo();
        }

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        public void UpdateInfo()
        {
            Debug.Log("exp = " + GlobalDataController.StageGainedXP + " coin = " + GlobalDataController.StageGainedCoin);

            View.UpdateInfo(GlobalDataController.StageGainedXP.ToString(), GlobalDataController.StageGainedCoin.ToString(), "");
        }
    }
}
