using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 해당 스테이지에서 보여줄수 있는 카드 및 선택된 카드를 메니징
    /// 1. 로비에서 선택한 카드를 가지고 있음
    /// 2. 매 라운드 끝마다 보여줄 카드 결정
    /// 3. 매 라운드 선택된 카드를 가지고 있음.
    /// </summary>
    public class CardManager : MonoBehaviour
    {
        //싱글톤 패턴
        static public CardManager Instance;

        [Header("[[테스트용 삭제예정] 로비 선택 카드 => 테스트 용이며 직접 사용하지 않는다]")]
        public List<CardSetting> CardSettingsReference = new List<CardSetting>();

        //[Header("[[테스트용 삭제예정] cardSettings의 복제 => 실제 사용됨]")]
        //public List<CardSetting> CardSettingsClone;

        [Header("[로비에서 선택한 카드]")]
        public List<CardSetting> AllCards;

        [Header("[라운드별 선택한 카드]")]
        public List<CardSetting> SelectedCards;


        //실제 사용은 이걸로 해야함.
        //*테스트할 때는 눈으로 봐야하기 때문에 CardSettings 사용
        public List<ICard> cards;

        private void Awake()
        {
            //싱글톤 패턴
            Instance = this;
            //복제해서 사용
            CardSettingsReference.ForEach(value => SelectedCards.Add((CardSetting)Instantiate<CardSetting>(value)));
        }

        /// <summary>
        /// 카드 추가
        /// * 카드 추가시 중첩 확인 및 없을시 추가.
        /// * 이미 존재 한다면 중첩 카운트 증가
        /// </summary>
        public void AddCard(CardSetting cardSetting)
        {
            bool ableAdd = true;
            for (int i = 0; i < SelectedCards.Count; i++)
            {
                //이미 카드가 존재 한다.
                //카드 타입으로 확인
                if (SelectedCards[i].Type == cardSetting.Type)
                {
                    //스택카운터 증가
                    SelectedCards[i].StackCount++;
                    //추가 불가 플래그 
                    ableAdd = false;
                }
            }

            //추가 가능 플래그라면 해당 카드 추가
            if (ableAdd) { SelectedCards.Add(cardSetting); }
        }


        #region [-TestCode]
        //void Update()
        //{
        //    //중복 체크하면 카드 중첩 잘되는지 테스트
        //    if (Input.GetKeyDown(KeyCode.Alpha8))
        //    {
        //        for (int i = 0; i < testCardSettings.Count; i++)
        //        {
        //            AddCard(testCardSettings[i]);
        //        }
        //    }
        //}
        #endregion
    }
}
