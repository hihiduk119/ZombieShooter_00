using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 모든 카드 데이터
    /// 카드 세팅 데이터와 동기화도 진행
    /// *MVP 모델
    /// </summary>
    public class UICardModel : MonoBehaviour
    {
        //UICardInfoPopupPresenter-> 에서 카드 세팅을 가져와 사용하기 때문에 업데이트 필수
        [Header("[모든 카드 리스트 세팅 => 세이브 로드시 반드시 동기화 해야함]")]
        public List<CardSetting> cardSettings = new List<CardSetting>();

        /// <summary>
        /// 저장 데이터와 실제 CardSetting 데이터간 동기화 시킴
        /// * 이때 언락 제한이 풀링는 레벨이 있다면 풀어야 함
        /// ?????
        /// </summary>
        /*public void Synchronization()
        {
            Debug.Log("================> 카드 데이터와 카드 세팅 데이터 동기화 진행 완료 <================");

            //데이터 동기화전 바뀌어야 하는 부분 반영
            
            //이미 언락이 됬다면 역쉬 언락

            //데이터 데입
            for (int i = 0; i < cardSettings.Count; i++)
            {
                //일단 로드부터해서 데이터 초기화
                cardSettings[i].Load();

                //아직 언락 안된상태
                if (cardSettings[i].UseAble == false)
                {
                    //언락 레벨 보다 현재 레벨이 높다면 언락.
                    if (cardSettings[i].UnlockLevel <= cardSettings[i].Level)
                    {
                        //카드 언락 여부
                        cardSettings[i].UseAble = true;
                    }
                }   
            }

            FirstStart();
        }*/

        /// <summary>
        /// 카드 데이터의 불러오기
        /// * 반드시 CardSetting 과 씽크 마추는 작업 필요
        /// </summary>
        public void Load()
        {
            cardSettings.ForEach(value => {
                //카드데이터 로드
                value.Load();
                //카드 업글 완료 확인 및 완료 카드 큐에 넣기
                value.CheckTheCardToUpgradeComplated();
            });
        }

        /// <summary>
        /// 
        /// 게임 첫 시작
        /// </summary>
        public void FirstStart()
        {
            //데이터 최초 생성시
            if (cardSettings.Count == 0)
            {
                for (int i = 0; i < cardSettings.Count; i++)
                {
                    //첫번째케릭터는 무조건 언락
                    //첫 캐릭터는 16이며 SelectedCharacter와 같아서 통일
                    if (i == GlobalDataController.Instance.SelectedCharacter) { cardSettings[i].cardData = new CardSetting.CardData(true); }
                    //외 나머지 케릭터 락
                    else { cardSettings[i].cardData = new CardSetting.CardData(); }
                }
            }

            //PlayerPrefs.SetString("UICard", JsonUtility.ToJson(data));
            //data.Print("[Save]");
        }

        /// <summary>
        /// Test용 이며 데이터 초기화 필요할때 호출
        /// </summary>
        public void Reset()
        {
            //저장 데이터 초기화
            PlayerPrefs.DeleteAll();

            //카드 세팅에 저장된 런타임 데이터 초기화
            for (int i = 0; i < cardSettings.Count; i++)
            {
                cardSettings[i].Reset();
            }
        }

        /*
        #region [-TestCode]
        void Update()
        {
            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    Load();
            //}
            //if (Input.GetKeyDown(KeyCode.S))
            //{
            //    Save();
            //}
            if (Input.GetKeyDown(KeyCode.D))
            {
                //모든 데이터 초기화
                Reset();
            }
        }
        #endregion
        */
    }
}
