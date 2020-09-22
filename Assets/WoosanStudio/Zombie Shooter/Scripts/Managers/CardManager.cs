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

        [Header("[활성화된 카드의 프로퍼티 리스트]")]
        //카드 스택 카운터에 따라 추가 프로퍼티 생성됨
        public List<CardProperty> ActivatedCardProperties = new List<CardProperty>();

        [Header("[활성화된 카드의 긍정 갑 프로퍼티 리스트]")]
        //카드 스택 카운터에 따라 추가 프로퍼티 생성됨
        public List<CardProperty> ActivatedCardPositiveProperties = new List<CardProperty>();

        [Header("[활성화된 카드의 부정 갑 프로퍼티 리스트]")]
        public List<CardProperty> ActivatedCardNegativeProperties = new List<CardProperty>();
        

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

            #region [-Test Delete]
            //테스트용 프로퍼티
            //if (ActivatedCardProperties == null) ActivatedCardProperties = new List<CardProperty>();

            ////안에 있던 모든 프로퍼티 삭제
            //if (0 < ActivatedCardProperties.Count)
            //{
            //    ActivatedCardProperties.ForEach(value => Destroy(value));
            //    //널로 초기화
            //    ActivatedCardProperties = new List<CardProperty>();
            //}

            //모든 프로퍼티 가져오기
            ActivatedCardProperties = GetActivatedCardProperties(this.HasCards);
            //긍정 프로퍼티 가져오기
            ActivatedCardPositiveProperties = GetActivatedCardPositiveProperties(this.HasCards);
            //부정 프로퍼티 가져오기
            ActivatedCardNegativeProperties = GetActivatedCardNegativeProperties(this.HasCards);


            #endregion
        }

        /// <summary>
        /// 증가 데미지 공식
        /// </summary>
        /// <param name="damage">무기와 탄약을 더한 값</param>
        /// <param name="level">카드 레밸</param>
        /// <param name="property">카드 프로퍼티 값</param>
        /// <param name="percentage">기본 100% 이며 크리티컬 값을 구할시 200%으로 변경</param>
        /// <returns></returns>
        public float IncreaseDamage(float damage, int level, CardProperty property, int percentage = 100)
        {
            //카드 레벨에 의해 증가된 수치
            //(1레벨당 증가 값 * 레벨) + 기본벨류+[퍼센트 100 (200이면 크리티컬)]
            int percent = (property.IncreasedValuePerLevelUp * level) + property.Value + percentage;

            //퍼센트 값을 정상 값으로 바꾸려면 0.01f 곱해야함.
            float returnValue = damage * percent * 0.01f;

            return returnValue;
        }

        //같은 종류의 카드는 중첩 되지 않지만 다른 종류는 중첩된다
        public float DecreaseDamage(float damage, int level, CardProperty property)
        {
            //100을 기준으로 감소 퍼센트 계산
            //100기준값 - ((1 레벨당 추가 갑소 값 * level) + 기본 감소 수치)
            int percent = 100 - ((property.IncreasedValuePerLevelUp * level) + property.Value);

            //퍼센트 값을 정상 값으로 바꾸려면 0.01f 곱해야함.
            float returnValue = damage * percent * 0.01f;

            return returnValue;
        }

        //TestCode
        void TestCode()
        {
            List<CardProperty> properties = GetActivatedCardProperties(this.HasCards);

            for (int i = 0; i < properties.Count; i++)
            {
                Debug.Log(properties[i].Descripsion);
                Debug.Log("value by level = " + properties[i].ValueByLevel);
            }
        }

        /// <summary>
        /// 활성카드의 프로퍼티만 가져옴
        /// </summary>
        /// <param name="hasCards"></param>
        /// <returns></returns>
        public List<CardProperty> GetActivatedCardProperties(List<CardSetting> hasCards)
        {
            List<CardProperty> properties = new List<CardProperty>();
            //활성 상태인 카드의 프로퍼티만 넣기
            //*중첩(stack)이면 중첩만큼 넣어야함
            hasCards.ForEach(value => {
                //Debug.Log("HasCard = " + value.name + " [" + value.IsActivate + "]");
                if (value.IsActivate)//활성 상태인 것만 넣기
                {
                    for (int stack = 0; stack <= value.StackCount; stack++) //중첩 만큼 추가로 넣음
                    {
                        value.Properties.ForEach(value2 =>
                        {
                            //카드 레벨에 의 해 계산된 Value 넣기
                            value2.ValueByLevel = value2.Value + (value.Level * value2.IncreasedValuePerLevelUp);
                            properties.Add(Instantiate<CardProperty>(value2));
                        });
                    }
                }
            });

            return properties;
        }


        /// <summary>
        /// 활성카드의 긍정 프로퍼티만 가져옴
        /// </summary>
        /// <param name="hasCards"></param>
        /// <returns></returns>
        public List<CardProperty> GetActivatedCardPositiveProperties(List<CardSetting> hasCards)
        {
            List<CardProperty> properties = new List<CardProperty>();
            //활성 상태인 카드의 프로퍼티만 넣기
            //*중첩(stack)이면 중첩만큼 넣어야함
            hasCards.ForEach(value => {
                //Debug.Log("HasCard = " + value.name + " [" + value.IsActivate + "]");
                if (value.IsActivate)//활성 상태인 것만 넣기
                {
                    for (int stack = 0; stack <= value.StackCount; stack++) //중첩 만큼 추가로 넣음
                    {
                        value.Properties.ForEach(value2 =>
                        {
                            //카드 레벨에 의 해 계산된 Value 넣기
                            value2.ValueByLevel = value2.Value + (value.Level * value2.IncreasedValuePerLevelUp);

                            //value의 값이0 보다 큰가?
                            if( 0 <= value2.Value )
                            {
                                properties.Add(Instantiate<CardProperty>(value2));
                            }
                        });
                    }
                }
            });

            return properties;
        }

        /// <summary>
        /// 활성카드의 부정 프로퍼티만 가져옴
        /// </summary>
        /// <param name="hasCards"></param>
        /// <returns></returns>
        public List<CardProperty> GetActivatedCardNegativeProperties(List<CardSetting> hasCards)
        {
            List<CardProperty> properties = new List<CardProperty>();
            //활성 상태인 카드의 프로퍼티만 넣기
            //*중첩(stack)이면 중첩만큼 넣어야함
            hasCards.ForEach(value => {
                //Debug.Log("HasCard = " + value.name + " [" + value.IsActivate + "]");
                if (value.IsActivate)//활성 상태인 것만 넣기
                {
                    for (int stack = 0; stack <= value.StackCount; stack++) //중첩 만큼 추가로 넣음
                    {
                        value.Properties.ForEach(value2 =>
                        {
                            //카드 레벨에 의 해 계산된 Value 넣기
                            value2.ValueByLevel = value2.Value + (value.Level * value2.IncreasedValuePerLevelUp);

                            //value의 값이0 보다 작은가?
                            if ( value2.Value <= 0 )
                            {
                                bool isExist = false;
                                //중복 검사 하여 중복 안되게 부정 프로퍼티 넣음
                                properties.ForEach(myProperty => {
                                    if(myProperty.Type == value2.Type)
                                    {
                                        isExist = true;
                                    }
                                });

                                if (isExist == false) { properties.Add(Instantiate<CardProperty>(value2)); }
                            }
                        });
                    }
                }
            });

            return properties;
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

            //프로퍼티의 값들을 더하기 
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                TestCode();
            }
        }
        #endregion
    }
}
