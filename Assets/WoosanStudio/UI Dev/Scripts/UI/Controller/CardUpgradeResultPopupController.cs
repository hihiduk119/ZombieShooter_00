﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DarkTonic.MasterAudio;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드 업그레이드 결과 UI 컨트롤러
    /// *MVP 모델
    /// </summary>
    public class CardUpgradeResultPopupController : MonoBehaviour
    {
        [Header("UI 팝업 오프너 -> 카드 업그레이드 결과 출력]")]
        public Ricimi.PopupOpener popupOpener;

        /// <summary>
        /// 결과 출력 실행
        /// </summary>
        /// <param name="result"></param>
        public void OpenResult(Sprite sprite, Color spriteColor, string name, string strResult,string strLevel, bool bResult)
        {
            UICardUpgradeResultPopupPresenter presenter = popupOpener.popupPrefab.GetComponent<UICardUpgradeResultPopupPresenter>();

            presenter.sprite = sprite;
            presenter.spriteColor = spriteColor;
            presenter.cardName = name;
            presenter.strResult = strResult;
            presenter.strLevel = strLevel;
            presenter.bResult = bResult;

            popupOpener.OpenPopup();
        }

        /*
        #region [-TestCode]
        void Update()
        {
            //업그레이드 성공 창 열기
            if (Input.GetKeyDown(KeyCode.A))
            {
                OpenResult("SUCCESS", "2", true);

                MasterAudio.FireCustomEvent("GUI_Positive", this.transform);
            }

            //업그레이드 실패 창 열기
            if (Input.GetKeyDown(KeyCode.S))
            {
                OpenResult("FAIL", "1", false);

                MasterAudio.FireCustomEvent("GUI_Negative", this.transform);
                //MasterAudio.FireCustomEvent("GUI_Positive", this.transform);
            }
        }
        #endregion
        */
    }
}
