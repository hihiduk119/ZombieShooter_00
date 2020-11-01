using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드 업그레이드 결과 컨트롤러
    /// *MPV 모델
    /// </summary>
    public class CardUpgradeResaultController : MonoBehaviour
    {
        [Header("UI 팝업 오프너 -> 카드 업그레이드 결과 출력]")]
        public Ricimi.PopupOpener popupOpener;

        /// <summary>
        /// 결과 출력 실행
        /// </summary>
        /// <param name="result"></param>
        public void OpenResult(string strResult, bool bResult)
        {
            UICardUpgradeResualtPopupView view = popupOpener.popupPrefab.GetComponent<UICardUpgradeResualtPopupView>();
            view.strResult = strResult;
            view.bResult = bResult;

            popupOpener.OpenPopup();
        }

        #region [-TestCode]
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                OpenResult("SUCCESS", true);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                OpenResult("FAIL", false);
            }
        }
        #endregion
    }
}
