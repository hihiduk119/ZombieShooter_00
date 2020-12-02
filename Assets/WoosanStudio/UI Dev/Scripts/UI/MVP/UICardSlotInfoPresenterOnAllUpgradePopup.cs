using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;

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
        public CardSetting cardSetting;

        public enum SlotState
        {
            Empty = 0,          //연구 슬롯 비었음.
            Lock,               //구매는 순차적으로 가능하기에 아무것도 없는 상태
            PurchaseAble,       //구매 가능.
            Upgrading,          //연구중
        }

        //캐쉬용
        private WaitForSeconds WFS = new WaitForSeconds(0.33f);
        private Coroutine updateUpgradeRemainTimeCoroutine;

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

                    //업그레이드 중이라면 남은시간 및 게이지 표시 코루틴 동작 실행
                    UpdateUpgradeRemainTime();
                    break;
            }
        }

        /// <summary>
        /// 업그레이드 남은 정보 업데이트
        /// *빠른 간격으로 표시해야함
        /// </summary>
        void UpdateUpgradeRemainTime()
        {
            //코루틴 동작 중이라면 즉시 제거 -> 코루틴 중복을 막기위해
            if (updateUpgradeRemainTimeCoroutine != null) { StopCoroutine(updateUpgradeRemainTimeCoroutine); }
            //코루틴 새로생성
            updateUpgradeRemainTimeCoroutine = StartCoroutine(UpdateUpgradeRemainTimeCoroutine());
        }

        /// <summary>
        /// 0.33f간격
        /// 업그레이드 남은 정보 업데이트 코루틴
        /// </summary>
        /// <returns></returns>
        IEnumerator UpdateUpgradeRemainTimeCoroutine()
        {
            while (true)
            {
                //남은 연구 시간만 0.33f단위로 업데이트
                //View.UpdateTime(cardSetting.UpgradeTimeset.GetRemainTimeToString(),cardSetting.UpgradeTimeset.GetRemainValue());
                View.UpdateTime(CardSetting.UpgradeRemainTimeToString(cardSetting), cardSetting.UpgradeTimeset.GetRemainValue());
                yield return WFS;
            }
        } 

        /// <summary>
        /// 업그레이드 취소 호출
        /// * 업글 취소 링커에 전달
        /// </summary>
        public void CancelToUpgrade()
        {
            //업글 취소를 위해 캔슬 링커 찾음
            //*Cancel 버튼에 위치
            UICardSlotInfoLinkerAboutCancelBtn cancelToUpgradePopup = GameObject.FindObjectOfType<UICardSlotInfoLinkerAboutCancelBtn>();
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("Cancel the upgrade.");

            //링커에 해당 정보로 업그레이드 취소할수있게 전달
            cancelToUpgradePopup.Send(UINotifyYesOrNoPopupPresenter.Type.CancelUpgrade, stringBuilder.ToString(), cardSetting);
        }
    }
}
