using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드들이 담긴 리스트
    /// </summary>
    public class CardList : MonoBehaviour
    {
        [Header("[로비 선택 카드 => 테스트 용이며 직접 사용하지 않는다]")]
        public List<CardSetting> CardSettingsReference = new List<CardSetting>();

        [Header("[cardSettings의 복제 => 실제 사용됨]")]
        public List<CardSetting> CardSettingsClone;

        //실제 사용은 이걸로 해야함.
        //*테스트할 때는 눈으로 봐야하기 때문에 CardSettings 사용
        public List<ICard> cards;

        private void Awake()
        {
            //복제해서 사용
            CardSettingsReference.ForEach(value => CardSettingsClone.Add((CardSetting)Instantiate<CardSetting>(value)));
        }

        /// <summary>
        /// 카드 추가
        /// * 카드 추가시 중첩 확인 및 없을시 추가.
        /// * 이미 존재 한다면 중첩 카운트 증가
        /// </summary>
        public void AddCard(CardSetting cardSetting)
        {
            bool ableAdd = true;
            for (int i = 0; i < CardSettingsClone.Count; i++)
            {
                //이미 카드가 존재 한다.
                //카드 타입으로 확인
                if (CardSettingsClone[i].Type == cardSetting.Type)
                {
                    //스택카운터 증가
                    CardSettingsClone[i].StackCount++;
                    //추가 불가 플래그 
                    ableAdd = false;
                }
            }

            //추가 가능 플래그라면 해당 카드 추가
            if (ableAdd) { CardSettingsClone.Add(cardSetting); }
        }


        #region [-TestCode]
        void Update()
        {
            //중복 체크하면 카드 중첩 잘되는지 테스트
            //if (Input.GetKeyDown(KeyCode.Alpha8))
            //{
            //    for (int i = 0; i < testCardSettings.Count; i++)
            //    {
            //        AddCard(testCardSettings[i]);
            //    }
            //}

            //플레이어의 카드 리스트에 강제로 연결
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                Player.Instance.CardList = this;
            }
        }
        #endregion
    }
}
