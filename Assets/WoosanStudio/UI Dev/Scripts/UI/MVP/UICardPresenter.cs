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

                    //아직 완료 통지 알림 안됨
                    cardSetting.ShownUpgradeComplate = false;

                    //시간 데이터 업데이트
                    //세팅할 업글 시간 가져오기
                    int seconds = NextValueCalculator.GetUpgradeTimeByLevel(cardSetting.MaxLevel, cardSetting.Level);
                    //시간 업데이트
                    cardSetting.UpgradeTimeset = new Timeset(seconds);
                    //현재 업글 중으로 변경
                    cardSetting.IsUpgrading = true;

                    //코인의 경우 즉시 화면 갱신 필요.
                    CardUpgradeComplate(cardSetting);
                    break;
                case CardSetting.CallToUpgrade.Gem:
                    //아직 완료 통지 알림 안됨
                    cardSetting.ShownUpgradeComplate = false;
                    //업그레이드 완료인 상태로 카드 등록시킴
                    UIGlobalMesssageQueueVewModel.UpgradeComplateEvent.Invoke(cardSetting);
                    break;
                case CardSetting.CallToUpgrade.Gamble:
                    //아직 완료 통지 알림 안됨
                    cardSetting.ShownUpgradeComplate = false;
                    //업그레이드 완료인 상태로 카드 등록시킴
                    UIGlobalMesssageQueueVewModel.UpgradeComplateEvent.Invoke(cardSetting);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 카드 업그레이드 취소
        /// </summary>
        public void CancelCardUpgrade(CardSetting cardSetting)
        {
            //카드 취소
            //*데이터 모두 변경
            CardSetting.CancelToUpgrade(cardSetting);

            //취소 호출은 2군대 호출 가능 및 3군대 업데이트 필요.
            //[1].모든 카드 인포창
            //[2]. 카드 업그레이드 창
            //*[3]. 카드 업그레이드 정보 슬롯 3개 있는 창.

            //[1]모든 카드 인포창
            UICardInfoPopupPresenter cardInfoPopupPresenter = GameObject.FindObjectOfType<UICardInfoPopupPresenter>();
            if (cardInfoPopupPresenter != null)
            {
                //선택된 카드 아이템 강제 선택 이벤트 발생.
                cardInfoPopupPresenter.CardItemPresenter.Selected();
            }

            //[2]카드 연구 정보 팝업 가져오기
            UICardResearchInfoPopupPresenter cardResearchInfoPopupPresenter = GameObject.FindObjectOfType<UICardResearchInfoPopupPresenter>();
            if (cardResearchInfoPopupPresenter != null)
            {
                //[2]연구 화면 갱신
                cardResearchInfoPopupPresenter.UpdateCardInfo();
            }
        }

        /// <summary>
        /// 완료 통지가 끝나고 업그레이드 데이터 실제 반영
        /// </summary>
        public void CardUpgradeComplate(CardSetting cardSetting)
        {
            //카드 연구 정보 팝업 가져오기
            UICardResearchInfoPopupPresenter cardResearchInfoPopupPresenter = GameObject.FindObjectOfType<UICardResearchInfoPopupPresenter>();

            //연구 화면 갱신
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
