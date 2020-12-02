using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ricimi;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 모든 연구 슬롯
    /// MVP 모델
    /// </summary>
    public class UICardUpgradeSlotPopupPresenter : MonoBehaviour
    {
        [Header("[MVP Model]")]
        public UICardUpgradeSlotPopupModel Model;

        [Header("[모든 카드 정보 슬롯]")]
        public List<UICardSlotInfoPresenterOnAllUpgradePopup> cardSlotInfos = new List<UICardSlotInfoPresenterOnAllUpgradePopup>();

        //팝업을 2개 사용
        //1 Yes or No 팝업
        //2 단순 통지 업그레이드 최대 도달 안내.

        [Header("[업그레이드 슬롯 추가 팝업]")]
        public PopupOpener openPopupForAddUpgradeSlot;

        [Header("[알림 통지 팝업]")]
        public PopupOpener openPopupForNotify;

        /// <summary>
        /// 팝업을 열기
        /// </summary>
        public void OpenPopup()
        {
            //사용 가능 슬롯이 최대 슬롯과 같거나 크다면
            if(GlobalDataController.MaxUpgradeSlotCount <= GlobalDataController.Instance.UseUpgradeAbleSlotCount)
            {
                //최대 임을 알리는 통지
                openPopupForNotify.OpenPopup();
            }
            else
            {
                //업그레이드 술롯 구매 팝업
                openPopupForAddUpgradeSlot.OpenPopup();
            }
        }

        /*
        /// <summary>
        /// 모든 슬롯의 정보를 업데이트 함
        /// </summary>
        void UpdateAllSlotInfo()
        {
            UIGlobalMesssageQueueVewModel messageQueue = GameObject.FindObjectOfType<UIGlobalMesssageQueueVewModel>();

            //업그레이딩 중인 슬롯
            int currentUpgradingCardCount = messageQueue.UpgradingCardList.Count;

            //현재 Empty인 슬롯

            //현재 구매된 슬롯
            int upgradeAbleSlot = GlobalDataController.Instance.UseUpgradeAbleSlotCount;

            //아무것도 없을때
            //[0:Empty][1:Purchase][2:Lock]


            //현재 Lock인 슬롯 = (모든슬롯 - (모든슬롯 - 구매된 슬롯)) + 1
            //ex>
            //3-(3-1) + 1 = 2
            //3-(3-2) + 1 = 3
            //3-(3-3) + 1 = 4
            //*Lock 표시하는 슬롯 인덱스가 됨
            int lockSlot = GlobalDataController.MaxUpgradeSlotCount - (GlobalDataController.MaxUpgradeSlotCount - GlobalDataController.Instance.UseUpgradeAbleSlotCount) + 1;

            //구매슬롯은 무조건 (lockSlot - 1)
            int purchaseSlot = lockSlot - 1;

            //카드에 업데이트 명령
            for (int i = 0; i < cardSlotInfos.Count; i++)
            {
                if (i < currentUpgradingCardCount)  //업글중인 슬롯
                {
                    Debug.Log("업글 중 슬롯 [" + i + "]");
                    cardSlotInfos[i].UpdateInfo(messageQueue.UpgradingCardList[i], UICardSlotInfoPresenterOnAllUpgradePopup.SlotState.Upgrading, -1);
                }
                else if(i < purchaseSlot)           //빈 슬롯 (구매 슬롯 전까지)
                {
                    Debug.Log("빈 슬롯 [" + i+"]");
                    cardSlotInfos[i].UpdateInfo(null, UICardSlotInfoPresenterOnAllUpgradePopup.SlotState.Empty, -1);
                }
                
                if (i == purchaseSlot)              //구매 슬롯
                {
                    Debug.Log("구매 슬롯 [" + i + "]");
                    //Model.data.Prices[i-1] 이유는 카드 인덱스와 가격 인덱스 차이가 -1이라서 이다.
                    cardSlotInfos[i].UpdateInfo(null, UICardSlotInfoPresenterOnAllUpgradePopup.SlotState.PurchaseAble, Model.data.Prices[i-1]);
                }
                
                if(i >= lockSlot)                   //잠긴 슬롯
                {
                    Debug.Log("잠긴 슬롯 [" + i + "]");
                    cardSlotInfos[i].UpdateInfo(null, UICardSlotInfoPresenterOnAllUpgradePopup.SlotState.Lock,-1);
                }
            }
        }
        */
    }
}
