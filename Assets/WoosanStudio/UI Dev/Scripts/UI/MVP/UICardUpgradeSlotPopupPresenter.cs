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
        //[Header("[모든 카드 정보 슬롯]")]
        //public List<UICardSlotInfoPresenterOnAllUpgradePopup> cardSlotInfos = new List<UICardSlotInfoPresenterOnAllUpgradePopup>();

        //팝업을 2개 사용
        //1 Yes or No 팝업
        //2 단순 통지 업그레이드 최대 도달 안내.

        [Header("[MVP Model]")]
        public UICardUpgradeSlotPopupModel Model;

        [Header("[MVP View]")]
        public UICardUpgradeSlotPopupView View;

        [Header("[업그레이드 슬롯 추가 팝업]")]
        public PopupOpener openPopupForAddUpgradeSlot;

        //[Header("[알림 통지 팝업]")]
        //public PopupOpener openPopupForNotify;

        //캐쉬용
        private WaitForSeconds WFS = new WaitForSeconds(1f);
        private UIGlobalMesssageQueueVewModel messsageQueueVewModel;

        IEnumerator Start()
        {
            messsageQueueVewModel = GameObject.FindObjectOfType<UIGlobalMesssageQueueVewModel>();
            //자동으로 현재 업글 상태 업데이트
            while (true)
            {
                //화면 업데이트
                UpdateInfo();

                yield return WFS;
            }
        }

        /// <summary>
        /// 화면 업데이트
        /// </summary>
        public void UpdateInfo()
        {
            //현재 업글중인 슬롯 갯수
            int usingSlotCount = messsageQueueVewModel.UpgradingCardList.Count;
            //현재 정보 업데이트
            View.UpdateInfo(usingSlotCount, GlobalDataController.Instance.UseUpgradeAbleSlotCount);
        }

        /// <summary>
        /// 팝업을 열기
        /// </summary>
        public void OpenPopup()
        {
            //사용가능한 슬롯이 없다.
            if(GlobalDataController.CanSlot())
            {
                //슬롯이 최대 임을 알리는 메시지 보내기
                NotifyPopupController.Instance.OpenResult(UINotifyPopupModel.Type.SlotIsMax);
            }
            else
            {
                UINotifyUpgradeSlotAddPresenter addUpgradeSlotPopup = openPopupForAddUpgradeSlot.popupPrefab.GetComponent<UINotifyUpgradeSlotAddPresenter>();

                //업그레이드 가격 세팅
                addUpgradeSlotPopup.price = Model.data.Prices[GlobalDataController.Instance.UseUpgradeAbleSlotCount-1];

                //업그레이드 술롯 구매 팝업
                openPopupForAddUpgradeSlot.OpenPopup();
            }
        }

        /// <summary>
        /// UINotifyUpgradeSlotAddPresenter.ClickYes()에서 호출.
        /// </summary>
        public void ClickYes(int price)
        {
            CoinPresenter coinPresenter = GameObject.FindObjectOfType<CoinPresenter>();

            //돈 부족
            if (price > coinPresenter.GetCoin())
            {
                //돈부족 알리는 메시지 보내기
                NotifyPopupController.Instance.OpenResult(UINotifyPopupModel.Type.NotEnoughCoin);

                return;
            }

            //코인 사용
            coinPresenter.SubtractCoin(price);

            if(GlobalDataController.Instance.UseUpgradeAbleSlotCount < GlobalDataController.MaxUpgradeSlotCount)
            {
                //실제 슬롯 증가
                GlobalDataController.Instance.UseUpgradeAbleSlotCount++;
            }

            //화면 업데이트
            this.UpdateInfo();
        }
    }
}
