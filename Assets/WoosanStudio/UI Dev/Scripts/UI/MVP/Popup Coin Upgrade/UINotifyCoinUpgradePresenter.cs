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
                , CardSetting.UpgradeComplateLevelToString(CardSetting)
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

            //일반 업글 시작
            this.CardSetting.StartTheUpgrade();

            //카드 연구 정보 팝업 가져오기
            UICardResearchInfoPopupPresenter cardResearchInfoPopupPresenter = GameObject.FindObjectOfType<UICardResearchInfoPopupPresenter>();
            //화면 다시 표시
            cardResearchInfoPopupPresenter.UpdateCardInfo();


            //코인이 충분하면 UICardPresenter에서 해당 카드 업글 호출
            //카드 데이터 및 카드 레벨 및 업그레이드 시작알림

        }
    }
}
