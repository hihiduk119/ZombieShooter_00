using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 모든 카드 모델 데이터를 필요에 따라 동기화 시키는 역활을 함.
    /// *MVP 모델
    /// </summary>
    public class UICardPresenter : MonoBehaviour
    {
        [Header("[MVP Model]")]
        public UICardModel Model;

        /*IEnumerator Start()
        {
            //0.1f초 대기 이유??
            //일단 에러 발생은 확인
            yield return new WaitForSeconds(0.1f);
            Initialize();
        }*/

        private void Awake()
        {
            Model.Load();
        }

        /// <summary>
        /// 최초 사용시 기존 데이터 모두 로드
        /// </summary>
        /*public void Initialize()
        {
            //싱크로 호출하여 데이터 동기화
            //Model.Synchronization();
        }*/

        /// <summary>
        /// 카드 업그레이드 시작시 카드 데이터 반영 및 싱크 마추기
        /// </summary>
        public void CardUpgradeStart(CardSetting cardSetting)
        {
            switch (cardSetting.WhoCallToUpgrade)
            {
                case CardSetting.CallToUpgrade.Coin:
                    //업그레이드 중인 상태로 카드 등록시킴
                    UIGlobalMesssageQueueVewModel.UpgradingEvent.Invoke(cardSetting);
                    break;
                case CardSetting.CallToUpgrade.Gem:
                    //업그레이드 완료인 상태로 카드 등록시킴
                    UIGlobalMesssageQueueVewModel.UpgradeComplateEvent.Invoke(cardSetting);
                    break;
                case CardSetting.CallToUpgrade.Gamble:
                    //업그레이드 완료인 상태로 카드 등록시킴
                    UIGlobalMesssageQueueVewModel.UpgradeComplateEvent.Invoke(cardSetting);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 완료 통지가 끝나고 업그레이드 데이터 실제 반영
        /// </summary>
        public void CardUpgradeComplate(CardSetting cardSetting)
        {
            //카드 연구 정보 팝업 가져오기
            UICardResearchInfoPopupPresenter cardResearchInfoPopupPresenter = GameObject.FindObjectOfType<UICardResearchInfoPopupPresenter>();
            //화면 다시 표시
            cardResearchInfoPopupPresenter.UpdateCardInfo();
        }


        /// <summary>
        /// Presenter에서 업그레이드 완료 통지시 부분
        /// </summary>
        /// <param name="cardSetting"></param>
        public void UpdateCardUpgrade(CardSetting cardSetting)
        {
            //완료된 카드가 아닌 업글중인 카드라면 다른곳에 업데이트 해야함

            //카드 완료 큐에 넣기
            UIGlobalMesssageQueueVewModel.UpgradeComplateEvent.Invoke(cardSetting);
        }
    }
}
