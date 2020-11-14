using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ricimi;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// Yes or No 확인 최종 통지
    /// *MVP 모델
    /// </summary>
    public class UINotifyYesOrNoPopupPresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UINotifyYesOrNoPopupView View;

        [Header("[Yes 버튼]")]
        public BasicButton YesButton;
        
        [Header("[[Auto => 전달받은 데이터]")]
        public string Desicription;

        [Header("[버튼 클릭시 연결될 액션]")]
        public UnityAction ClickYesAction;

        /// <summary>
        /// 팝업 활성화시 바로 실행하기 위해
        /// </summary>
        private void OnEnable()
        {
            //활성화시 바로 실행
            UpdateInfo(Desicription);
            //Yes버튼 클릭 이벤트 발생시 호출될 액션 연결
            YesButton.OnClicked.AddListener(ClickYesAction);
        }

        /// <summary>
        /// 삭제시 모든 리스너 등록 해제
        /// </summary>
        private void OnDestroy()
        {
            YesButton.OnClicked.RemoveAllListeners();
        }

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        public void UpdateInfo(string description)
        {
            View.UpdateView(description);
        }
    }
}
