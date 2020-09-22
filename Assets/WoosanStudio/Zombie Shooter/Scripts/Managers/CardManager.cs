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

        [Header("[테스트용을 복사한 카드 => [삭제예정]]")]
        public List<CardSetting> CardsPlayerOnClicked;

        [Header("[로비에서 선택한 카드]")]
        public List<CardSetting> CardsSelectedInRobby;

        [Header("[가지고 있는 카드]")]
        public List<CardSetting> HasCards = new List<CardSetting>();

        [Header("[선택한 카드의 프로퍼티 리스트]")]
        //카드 스택 카운터에 따라 추가 프로퍼티 생성됨
        public List<CardProperty> ActivatedCardProperties = new List<CardProperty>();


        //실제 사용은 이걸로 해야함.
        //*테스트할 때는 눈으로 봐야하기 때문에 CardSettings 사용
        public List<ICard> cards;

        private void Awake()
        {
            //싱글톤 패턴
            Instance = this;
            //복제해서 사용
            CardSettingsReference.ForEach(value => CardsPlayerOnClicked.Add((CardSetting)Instantiate<CardSetting>(value)));
        }


        /// <summary>
        /// 해당 범위의 카드를 비활성화 시킴
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="cardRange"></param>
        void DeactiveByCardType (List<CardSetting> cards,int[] cardRange)
        {
            //cardRange 범위의 카드라면 모두 비활성화 시킴
            cards.ForEach(value => {
                if (cardRange[0] <= (int)value.Type && (int)value.Type < cardRange[1])
                {
                    value.IsActivate = false;
                    Debug.Log("value name = " + value.name);
                }
            });
        }

        /// <summary>
        /// 카드 추가
        /// * 카드 추가시 중첩 확인 및 없을시 추가.
        /// * 이미 존재 한다면 중첩 카운트 증가
        /// * 활성 카드의 프로퍼티는 따로 모음.
        /// </summary>
        public void AddCard(CardSetting cardSetting)
        {
            //Debug.Log("1 카드이름 = " + cardSetting.name + " ["+ cardSetting.IsActivate + "]");
            //추가시 해당 카드는 무조건 활성 상태
            cardSetting.IsActivate = true;

            //카드 추가 와 상관없이 프로퍼티는 무조건 추가
            //cardSetting.Properties.ForEach(value => SelectedCardProperties.Add(Instantiate<CardProperty>(value)));

            //분류가 안돼 이부분 재 정의 필요.
            //추가 카드의 100-200 사이의 카드 - GunType
            if(100 <= (int)cardSetting.Type && (int)cardSetting.Type < 200)
            {
                //100-200 사이의 카드 타입 모두 비활성
                DeactiveByCardType(this.HasCards, new int[] { 100, 200 });
            }

            //추가 카드의 200-300 사이의 카드라면 - AmmoType
            if (200 <= (int)cardSetting.Type && (int)cardSetting.Type < 300)
            {
                //추가 카드가 탄약 타입 변환이라면 기존 탄약 타입 모두 비활성
                DeactiveByCardType(this.HasCards, new int[] { 200, 300 });
            }

            //추가 카드의 300-400 사이의 카드라면 - CharacterType
            if (300 <= (int)cardSetting.Type && (int)cardSetting.Type < 400)
            {
                //추가 카드가 캐릭터 타입 변환이라면 기존 캐릭터 타입 모두 비활성
                DeactiveByCardType(this.HasCards, new int[] { 300, 400 });
            }

            //기존 가지고 있는 카드와 비교하여 없으면 stack 카운트올리고 없으면 추가함
            bool ableAdd = true;
            for (int i = 0; i < HasCards.Count; i++)
            {
                //총 타입 이라면
                //if ( 100 <= (int)cardSetting.Type && (int)cardSetting.Type < 200 )

                //이미 카드가 존재 한다.
                //카드 타입으로 확인
                if (HasCards[i].Type == cardSetting.Type)
                {
                    //스택카운터 증가
                    HasCards[i].StackCount++;
                    //추가 불가 플래그 
                    ableAdd = false;
                    //해당 카드 활성화
                    HasCards[i].IsActivate = true;
                }
            }

            //추가 가능 플래그라면 
            if (ableAdd) {
                //해당 카드 활성화
                cardSetting.IsActivate = true;
                //해당 카드 추가
                HasCards.Add(cardSetting);
                //Debug.Log("추가됨 = " + cardSetting.TypeByCharacter.ToString());
            }

            //IsActivate 카드의 프로퍼티만 중첩횟수만큼 ActivatedCardProperties에 추가함
            //기존 프로퍼티 모두 삭제 다시 넣기
            if (ActivatedCardProperties == null) ActivatedCardProperties = new List<CardProperty>();

            //안에 있던 모든 프로퍼티 삭제
            if ( 0 < ActivatedCardProperties.Count )
            {
                ActivatedCardProperties.ForEach(value => Destroy(value));
                //널로 초기화
                ActivatedCardProperties = new List<CardProperty>();
            }

            //활성 상태인 카드의 프로퍼티만 넣기
            //*중첩(stack)이면 중첩만큼 넣어야함
            HasCards.ForEach(value => {
                //Debug.Log("HasCard = " + value.name + " [" + value.IsActivate + "]");
                if (value.IsActivate)//활성 상태인 것만 넣기
                {
                    for (int stack = 0; stack <= value.StackCount; stack++) //중첩 만큼 추가로 넣음
                    {
                        value.Properties.ForEach(value2 =>
                        {
                            ActivatedCardProperties.Add(Instantiate<CardProperty>(value2));
                            //Debug.Log("stack = " + stack);
                            //Debug.Log("외 안 넎음??");
                        });
                    }
                }
            });
        }


        #region [-TestCode]
        void Update()
        {
            //중복 체크하면 카드 중첩 잘되는지 테스트
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                for (int i = 0; i < CardsPlayerOnClicked.Count; i++)
                {
                    Debug.Log("===============> 추가할 카드 = " + CardsPlayerOnClicked[i].name +" <==============");
                    AddCard(CardsPlayerOnClicked[i]);

                    //CardsPlayerOnClicked.Clear();
                }
            }
        }
        #endregion
    }
}
