using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ricimi;

namespace WoosanStudio.ZombieShooter.UI
{
    /// <summary>
    /// 스테이지 내에서 버튼이 아닌 모든 팝업 메니징
    /// </summary>
    public class PopupsManager : MonoBehaviour
    {
        static public PopupsManager Instance;

        [Header("[어게인 팝업 오프너]")]
        public PopupOpener InGameStageAgainPopupOpener;

        [Header("[결과창 팝업 오프너]")]
        public PopupOpener InGameStageResultPopupOpener;

        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// 한번더 팝업 오픈
        /// </summary>
        public void OpenAgainPopup()
        {
            InGameStageAgainPopupOpener.OpenPopup();
            Debug.Log("AAA");
        }

        /// <summary>
        /// 결과 팝업 오픈
        /// </summary>
        public void OpenResultPopup()
        {
            InGameStageResultPopupOpener.OpenPopup();
            Debug.Log("BBB");
        }

        //private void Update()
        //{
        //    //한번더 팝업 오픈
        //    if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        OpenAgainPopup();
        //    }

        //    //결과 팝업 오픈
        //    if (Input.GetKeyDown(KeyCode.S))
        //    {
        //        OpenResultPopup();
        //    }
        //}
    }
}
