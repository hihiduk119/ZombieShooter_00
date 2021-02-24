using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WoosanStudio.ZombieShooter.UI;

namespace WoosanStudio.ZombieShooter.UI.MVP
{
    /// <summary>
    /// 스테이지 결과 한번더 팝업
    /// *부활 부분 미처리 -> 완성되면 연결 해야함.
    /// *MVP 모델
    /// </summary>
    public class InGameStageAgainPopupPresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public InGameStageAgainPopupView View;

        //인 게임네 비버튼 모든 팝업 제어
        private PopupsManager popupsManager;

        private int gem = 10;

        private void Awake()
        {
            //광고 후출에 필요 리스너 등록
            View.ClickADS_Event.AddListener(CallADS);
            //보석 사용호출 에 리스너 등록
            View.ClickGemEvent.AddListener(UseGem);
            //결과 팝업 호출 리스너에 등록
            View.ClickResultEvent.AddListener(CallResultPopup);
            //팝업 메니저 가져오기
            popupsManager = GameObject.FindObjectOfType<PopupsManager>();
        }

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        public void UpdateInfo(string price)
        {
            //View.UpdateView()
        }

        /// <summary>
        /// 광고 호출
        /// </summary>
        public void CallADS()
        {
            Debug.Log("[미구현] 보석 사용");
        }

        /// <summary>
        /// 보석 사용
        /// </summary>
        public void UseGem()
        {
            Debug.Log("[미구현] 보석 사용");
        }

        /// <summary>
        /// 결과 팝업 호출
        /// </summary>
        public void CallResultPopup()
        {
            //결과 팝업 오픈
            popupsManager.OpenResultPopup();
        }
    }
}
