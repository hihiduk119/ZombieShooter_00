using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ricimi;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드 슬롯과 카드 정보 
    /// </summary>
    public class UICardSlotInfoLinkerAboutUpgradeBtn : MonoBehaviour
    {
        [Header("[모델 역활 프리젠트]")]
        public UICardSlotInfoPresenter ToModel;

        [Header("[전달받는 역활 프리젠트를 추출 하는 주체]")]
        public PopupOpener openPopup;

        [Header("[[Auto->Awake()] 전달받는 역활 프리젠트]")]
        public UICardResearchInfoPopupPresenter FromPresenter;

        [Header("[Card-All-Infomation-Popup 의 Popup]")]
        public Popup popup;

        /// <summary>
        /// 연결된 두 프리젠트의 데이터를 전달
        /// </summary>
        public void Send()
        {
            //사용 가능 슬롯 갯수
            int useAbleSlotCount = GlobalDataController.Instance.UseUpgradeAbleSlotCount;
            //사용 중인 슬롯 갯수
            int usingSlotCount = GameObject.FindObjectOfType<UIGlobalMesssageQueueVewModel>().UpgradingCardList.Count;

            Debug.Log("useAbleSlotCount = " + useAbleSlotCount + "  usingSlotCount = " + usingSlotCount);

            //사용 중인 슬롯이 사용가능 슬롯 보다 같거나 크다
            if (usingSlotCount >= useAbleSlotCount)
            {
                Debug.Log("1");
                //슬롯이 다 찾다는 메시지 보내기
                NotifyPopupController.Instance.OpenResult(UINotifyPopupModel.Type.SlotIsFull);
                return;
            }

            Debug.Log("2");
            //openPopup에서 가져옴
            FromPresenter = openPopup.popupPrefab.GetComponent<UICardResearchInfoPopupPresenter>();
            Debug.Log("전달 완료");
            if (ToModel.CardSetting == null) { Debug.Log("카드 데이터가 NULL 이다"); }
            //카드 세팅 데이터 전달
            FromPresenter.CardSetting = ToModel.CardSetting;

            //실제 팝업을 여는 부분
            openPopup.OpenPopup();

            //Card-All-Infomation-Popup 닫기.
            popup.Close();
        }
    }
}
