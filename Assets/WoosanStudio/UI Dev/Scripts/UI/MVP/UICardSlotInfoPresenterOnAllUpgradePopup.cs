using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드연구 팝업의 컨텐츠
    /// </summary>
    public class UICardSlotInfoPresenterOnAllUpgradePopup : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UICardSlotInfoViewOnAllUpgradePopup View;

        [Header("[업글레이딩 카드]")]
        public List<CardSetting> cardSettings = new List<CardSetting>();

        public enum SlotState
        {
            Empty = 0,          //연구 슬롯 비었음.
            Lock,               //구매는 순차적으로 가능하기에 아무것도 없는 상태
            PurchaseAble,       //구매 가능.
            Upgrading,          //연구중
        }

        /// <summary>
        /// 해당 슬롯을 업데이트 명령
        /// </summary>
        public void UpdateInfo(CardSetting cardSetting  ,SlotState state, int price)
        {
            switch (state)
            {
                case SlotState.Empty:
                    View.UpdateInfo(null, state, -1);
                    break;
                case SlotState.Lock:
                    View.UpdateInfo(null, state, -1);
                    break;
                case SlotState.PurchaseAble:
                    View.UpdateInfo(null, state, price);
                    break;
                case SlotState.Upgrading:
                    View.UpdateInfo(cardSetting, state,- 1);
                    break;
            }
        }
    }
}
