using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Ricimi;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 알림 통지
    /// *MVP 모델
    /// </summary>
    public class UINotifyPopupPresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UINotifyPopupView View;
        [Header("[설명]")]
        public UINotifyPopupModel Model;

        [Header("[[Auto -> 전달받음] 알림 타입]")]
        public UINotifyPopupModel.Type Type = UINotifyPopupModel.Type.NotEnoughCoin;

        private Popup popup;

        /// <summary>
        /// 팝업 활성화시 바로 실행하기 위해
        /// </summary>
        private void OnEnable()
        {
            //팝업 가져오기
            popup = GetComponent<Popup>();

            //타입에 딸 메시지 업데이트
            switch (this.Type)
            {
                case UINotifyPopupModel.Type.NotEnoughCoin: //코인 부족 상태 메시지 업데이트
                    UpdateInfo(Model.data.Descriptions[0]);
                    break;
                case UINotifyPopupModel.Type.NotEnoughGem: //젬 부족 상태 메시지 업데이트
                    UpdateInfo(Model.data.Descriptions[1]);
                    break;
                case UINotifyPopupModel.Type.NotEnoughEnergy://에너지 부족 상태 메시지 업데이트
                    UpdateInfo(Model.data.Descriptions[2]);
                    break;
            }

            

            //2초 대기후 자동 닫기
            Invoke("Close", 2f);
        }

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        public void UpdateInfo(string description)
        {
            View.UpdateView(description);
        }

        /// <summary>
        /// 팝업 닫기
        /// </summary>
        private void Close()
        {
            popup.Close();
        }
    }
}
