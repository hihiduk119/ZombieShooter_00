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
            //바꾸려는 실제 저장 카드 인덱스
            int index = -1;

            for (int i = 0; i < Model.cardSettings.Count; i++)
            {
                //찾으려는 카드 세팅 데이터와 모델이 가지고 있는 데이터의 인덱스 알아오기
                if(Model.cardSettings[i].Name.Equals(cardSetting.Name)) { index = i; }
            }

            //여기에 실제 저장 데이터가 있다
            //UICardModel.CardData cardData = Model.data.CardDatas[cardIndex];
            //NextValueCalculator.GetUpgradeTimeByLevel()
            //cardData.UpgardeTimeset = new Timeset();
        }

        /// <summary>
        /// 완료 통지가 끝나고 업그레이드 데이터 실제 반영
        /// </summary>
        public void CardUpgradeComplate(CardSetting cardSetting)
        {
            
        }


        /// <summary>
        /// Presenter에서 업그레이드 완료 통지시 부분
        /// </summary>
        /// <param name="cardSetting"></param>
        public void UpdateCardUpgrade(CardSetting cardSetting)
        {
            //카드 완료 큐에 넣기
            UIGlobalMesssageQueueVewModel.UpgradeComplateEvent.Invoke(cardSetting);
        }
    }
}
