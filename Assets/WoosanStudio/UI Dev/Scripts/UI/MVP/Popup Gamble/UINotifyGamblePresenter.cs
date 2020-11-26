using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 도박 최종 알림 통지
    /// *MVP 모델
    /// </summary>
    public class UINotifyGamblePresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UINotifyGambleView View;

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
            View.UpdateView(CardSetting.PredictGambleSuccessLevelToString(CardSetting)
                , GlobalDataController.GambleGem.ToString()
                , GlobalDataController.Instance.GambleCurrentSuccessRate.ToString() + "%");
        }


        /// <summary>
        /// Ok 버튼 클릭
        /// </summary>
        public void ClickYes()
        {
            //코인 소비
            GemPresenter gemPresenter = GameObject.FindObjectOfType<GemPresenter>();

            //업그레이드 하려는 레벨
            int upgradeLevel = CardSetting.Level + 1;
            int requireGem = GlobalDataController.GambleGem;
            //요구코인보다 현재 코인이 적다.
            if (gemPresenter.GetGem() < requireGem)
            {
                //코인이 부족하면 알림 표시 및 종료
                NotifyPopupController.Instance.OpenResult(UINotifyPopupModel.Type.NotEnoughCoin);
                return;
            }

            //코인 소비
            gemPresenter.AddGem(-requireGem);

            //모든 카드 컨롤러
            UICardPresenter cardPresenter = GameObject.FindObjectOfType<UICardPresenter>();

            //코인에 의한 업그레이드 호출로 세팅
            CardSetting.WhoCallToUpgrade = CardSetting.CallToUpgrade.Gamble;

            //등록외 나머지 카드 컨트롤러 호출해서 처리 맏김
            cardPresenter.CardUpgradeStart(this.CardSetting);
        }
    }
}
