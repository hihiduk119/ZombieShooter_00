using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 젬 업글 최종 알림 통지
    /// *MVP 모델
    /// </summary>
    public class UINotifyGemUpgradePresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UINotifyGemUpgradeView View;

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
            View.UpdateView(CardSetting.PredictUpgradeComplateLevelToString(CardSetting)
                , CardSetting.RequireGemToString(CardSetting));
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
            int requireGem = CardSetting.RequireGem(this.CardSetting);
            //요구코인보다 현재 코인이 적다.
            if (gemPresenter.GetGem() < requireGem)
            {
                //코인이 부족하면 알림 표시 및 종료
                NotifyPopupController.Instance.OpenResult(UINotifyPopupModel.Type.NotEnoughGem);
                return;
            }

            //코인 소비
            gemPresenter.AddGem(-requireGem);

            //모든 카드 컨롤러
            UICardPresenter cardPresenter = GameObject.FindObjectOfType<UICardPresenter>();

            //코인에 의한 업그레이드 호출로 세팅
            CardSetting.WhoCallToUpgrade = CardSetting.CallToUpgrade.Gem;

            //등록외 나머지 카드 컨트롤러 호출해서 처리 맏김
            cardPresenter.CardUpgradeStart(this.CardSetting);
        }
    }
}
