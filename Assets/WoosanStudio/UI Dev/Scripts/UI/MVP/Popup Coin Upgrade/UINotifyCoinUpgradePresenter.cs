using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 코인 업글 최종 알림 통지
    /// *MVP 모델
    /// </summary>
    public class UINotifyCoinUpgradePresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UINotifyCoinUpgradeView View;

        [Header("[Auto -> 데이터 전달받음]")]
        public CardSetting CardSetting;

        /// <summary>
        /// 활성화시 바로 업데이트
        /// </summary>
        private void OnEnable()
        {
            UpdateView();
        }

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        private void UpdateView()
        {
            View.UpdateView(this.CardSetting.Sprite,this.CardSetting.IconColor
                , CardSetting.PredictUpgradeComplateLevelToString(CardSetting)
                , CardSetting.RequireCoinToString(CardSetting)
                , CardSetting.UpgradeRemainTimeToString(CardSetting));
        }

        /// <summary>
        /// Ok 버튼 클릭
        /// </summary>
        public void ClickYes()
        {
            //코인 소비
            CoinPresenter coinPresenter = GameObject.FindObjectOfType<CoinPresenter>();

            //사용 가능 슬롯 갯수
            int useAbleSlotCount = GlobalDataController.Instance.UseUpgradeAbleSlotCount;
            //사용 중인 슬롯 갯수
            int usingSlotCount = GameObject.FindObjectOfType<UIGlobalMesssageQueueVewModel>().UpgradingCardList.Count;

            //사용 중인 슬롯이 사용가능 슬롯 보다 같거나 크다
            if(usingSlotCount >= useAbleSlotCount)
            {
                //슬롯이 다 찾다는 메시지 보내기
                NotifyPopupController.Instance.OpenResult(UINotifyPopupModel.Type.SlotIsFull);
                return;
            }

            //업그레이드 하려는 레벨
            int upgradeLevel = CardSetting.Level + 1;
            int requireCoin = CardSetting.RequireCoin(this.CardSetting);
            //요구코인보다 현재 코인이 적다.
            if (coinPresenter.GetCoin() < requireCoin)
            {
                //코인이 부족하면 알림 표시 및 종료
                NotifyPopupController.Instance.OpenResult(UINotifyPopupModel.Type.NotEnoughCoin);
                return;
            }

            //코인 소비
            coinPresenter.AddCoin(-requireCoin);

            //모든 카드 컨롤러
            UICardPresenter cardPresenter = GameObject.FindObjectOfType<UICardPresenter>();

            //코인에 의한 업그레이드 호출로 세팅
            CardSetting.WhoCallToUpgrade = CardSetting.CallToUpgrade.Coin;

            //등록외 나머지 카드 컨트롤러 호출해서 처리 맏김
            cardPresenter.CardUpgradeStart(this.CardSetting);
        }
    }
}
