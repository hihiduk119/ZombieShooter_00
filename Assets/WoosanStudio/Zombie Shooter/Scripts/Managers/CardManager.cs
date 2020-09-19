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
        /// 추가 카드에 따라 해당 카드와 같은 타입 모든 카드 비활성화
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="compareCard"></param>
        /// <param name="cardType"></param>
        void DeactiveByCardType (List<CardSetting> cards,CardSetting compareCard ,CardSetting.CardType cardType)
        {
            //추가 카드가 총 타입 변환이라면
            if (compareCard.Type == cardType)
            {
                //기존 모든 총카드 찾아서 비활성화 시킴
                cards.ForEach(value => {
                    if (value.Type == cardType)
                    {
                        value.IsActivate = false;
                    }
                });
            }
        }

        /// <summary>
        /// 카드 추가
        /// * 카드 추가시 중첩 확인 및 없을시 추가.
        /// * 이미 존재 한다면 중첩 카운트 증가
        /// </summary>
        public void AddCard(CardSetting cardSetting)
        {
            //카드 추가 와 상관없이 프로퍼티는 무조건 추가
            //cardSetting.Properties.ForEach(value => SelectedCardProperties.Add(Instantiate<CardProperty>(value)));

            //분류가 안돼 이부분 재 정의 필요.

            //추가 카드가 총 타입 변환이라면 기존 총 타입 모두 비활성
            DeactiveByCardType(this.HasCards, cardSetting, CardSetting.CardType.Weapon);

            //추가 카드가 탄약 타입 변환이라면 기존 탄약 타입 모두 비활성
            DeactiveByCardType(this.HasCards, cardSetting, CardSetting.CardType.Ammo);

            //추가 카드가 캐릭터 타입 변환이라면 기존 캐릭터 타입 모두 비활성
            DeactiveByCardType(this.HasCards, cardSetting, CardSetting.CardType.Character);

            Debug.Log("추가중 = " + cardSetting.TypeByCharacter.ToString());

            bool ableAdd = true;
            for (int i = 0; i < HasCards.Count; i++)
            {
                //이미 카드가 존재 한다.
                //카드 타입으로 확인
                if (HasCards[i].Type == cardSetting.Type)
                {
                    //스택카운터 증가
                    HasCards[i].StackCount++;
                    //추가 불가 플래그 
                    ableAdd = false;
                }
            }

            //추가 가능 플래그라면 해당 카드 추가
            if (ableAdd) {
                HasCards.Add(cardSetting);
                Debug.Log("추가됨 = " + cardSetting.TypeByCharacter.ToString());
            }

            //기존 프로퍼티 모두 삭제 다시 넣기
            //*이때 isActivate만 넣음

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
                for(int stack = 0; stack <= value.StackCount;stack++) //중첩 만큼 추가로 넣음
                {
                    value.Properties.ForEach(value2 =>
                    {
                        ActivatedCardProperties.Add(Instantiate<CardProperty>(value2));
                        //Debug.Log("stack = " + stack);
                        //Debug.Log("외 안 넎음??");
                    });
                }  
            });

            Debug.Log("끝??");
        }


        #region [-TestCode]
        void Update()
        {
            //중복 체크하면 카드 중첩 잘되는지 테스트
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                for (int i = 0; i < CardsPlayerOnClicked.Count; i++)
                {
                    AddCard(CardsPlayerOnClicked[i]);
                }
            }
        }
        #endregion
    }
}
