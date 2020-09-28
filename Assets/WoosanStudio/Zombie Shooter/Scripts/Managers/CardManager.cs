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

        [Header("[[테스트용]몬스터의 프로퍼티]")]
        public List<CardProperty> monstersProperties;

        [Header("[[테스트용]건데이터")]
        public GunSettings TestGunSetting;

        [Header("[가지고 있는 카드]")]
        public List<CardSetting> HasCards = new List<CardSetting>();

        //[Header("[활성화된 카드의 프로퍼티 리스트]")]
        ////카드 스택 카운터에 따라 추가 프로퍼티 생성됨
        //public List<CardProperty> ActivatedCardProperties = new List<CardProperty>();

        //[Header("[활성화된 카드의 긍정 갑 프로퍼티 리스트]")]
        ////카드 스택 카운터에 따라 추가 프로퍼티 생성됨
        //public List<CardProperty> ActivatedCardPositiveProperties = new List<CardProperty>();

        //[Header("[활성화된 카드의 부정 갑 프로퍼티 리스트]")]
        //public List<CardProperty> ActivatedCardNegativeProperties = new List<CardProperty>();

        //[Header("[같은 값으로 정렬된 긍정 값]")]
        //public List<List<CardProperty>> sameProperties;

        [Header("[현재 플레이어의 무기]")]
        public CardProperty.PropertyType PlayerWeapon = CardProperty.PropertyType.PistolDamage;
        [Header("[현재 플레이어의 탄약]")]
        public CardProperty.PropertyType PlayerAmmo = CardProperty.PropertyType.BulletAmmoDamage;
        [Header("[현재 플레이어의 무기 데미지 => 계산용]")]
        public float WeaponDamage = 0;
        [Header("[현재 플레이어의 탄약 데미지 => 계산용]")]
        public float AmmoDamage = 0;
        [Header("[현재 플레이어의 체력 => 계산용]")]
        public float HealthPoint = 0;




        //프로퍼티의 중첩을 계산하기 위해 사용
        /*private class PropertyCalculateData
        {
            //중첩을 모두 더한 값.
            public float TotalValue;
            //프로퍼티 타입.
            public CardProperty.PropertyType Type;

            public PropertyCalculateData(float totalValue,CardProperty.PropertyType type)
            {
                this.TotalValue = totalValue;
                this.Type = type;
            }
        }*/

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
        /// 현재 플레이어의 무기와 탄약을 세팅시킴
        /// </summary>
        /// <param name="weapon"></param>
        /// <param name="ammo"></param>
        /// <param name="type"></param>
        private void SetWeaponAndAmmo(ref CardProperty.PropertyType weapon , ref CardProperty.PropertyType ammo, CardSetting card)
        {
            //해당 카드가 무기 타입 이라면
            if (100 <= (int)card.Type && (int)card.Type < 200)
            {
                //강제 캐스트
                weapon = (CardProperty.PropertyType)(int)card.Type;
                //무기 데미지 넣기
                WeaponDamage = (float)card.Value;
            }

            //해당 카드가 탄약타입 이라면
            if (200 <= (int)card.Type && (int)card.Type < 300)
            {
                //강제 캐스트
                ammo = (CardProperty.PropertyType)(int)card.Type;
                //탄약 데미지 넣기
                AmmoDamage = (float)card.Value;
            }
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

            //건 타입이면 가지고 있는 모든 건카드 비활성화
            //추가 카드의 100-200 사이의 카드 - GunType
            if(100 <= (int)cardSetting.Type && (int)cardSetting.Type < 200)
            {
                //100-200 사이의 카드 타입 모두 비활성
                DeactiveByCardType(this.HasCards, new int[] { 100, 200 });
            }

            //탄약 타입이면 가지고 있는 모든 탄약카드 비활성화
            //추가 카드의 200-300 사이의 카드라면 - AmmoType
            if (200 <= (int)cardSetting.Type && (int)cardSetting.Type < 300)
            {
                //추가 카드가 탄약 타입 변환이라면 기존 탄약 타입 모두 비활성
                DeactiveByCardType(this.HasCards, new int[] { 200, 300 });
            }

            //캐릭터 타입이면 가지고 있는 모든 캐릭터카드 비활성화
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

                    Debug.Log("1 활성 카드 = " + cardSetting.Type.ToString());

                    //활성화된 카드의 무기와 탄약을 플레이어에게 세팅시킴
                    SetWeaponAndAmmo(ref PlayerWeapon, ref PlayerAmmo, cardSetting);
                }
            }

            //추가 가능 플래그라면 
            if (ableAdd) {
                //해당 카드 활성화
                cardSetting.IsActivate = true;
                //해당 카드 추가
                HasCards.Add(cardSetting);

                Debug.Log("2 활성 카드 = " + cardSetting.Type.ToString());

                //활성화된 카드의 무기와 탄약을 플레이어에게 세팅시킴
                SetWeaponAndAmmo(ref PlayerWeapon, ref PlayerAmmo, cardSetting);
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
            //ActivatedCardProperties = GetActivatedCardProperties(this.HasCards);
            //긍정 프로퍼티 가져오기
            //ActivatedCardPositiveProperties = GetActivatedCardPositiveProperties(this.HasCards);
            //부정 프로퍼티 가져오기
            //ActivatedCardNegativeProperties = GetActivatedCardNegativeProperties(this.HasCards);


            #endregion
        }

        /// <summary>
        /// 데미지, 체력, 치명데미지 공식
        /// </summary>
        /// <param name="value">무기와 탄약을 더한 값</param>
        /// <param name="level">카드 레밸</param>
        /// <param name="property">카드 프로퍼티 값</param>
        /// <param name="percentage">기본 100% 이며 크리티컬 값을 구할시 200%으로 변경</param>
        /// <returns></returns>
        public float CalculateValue(float value, int level, CardProperty property, int percentage = 100)
        {
            //카드 레벨에 의해 증감된 수치
            //(1레벨당 증가 값 * 레벨) + 기본벨류+[퍼센트 100 (200이면 크리티컬)]
            float percent = (property.IncreasedValuePerLevelUp * level) + property.Value + percentage;
            //Debug.Log("증가 퍼센트 = " + percent);
            //퍼센트 값을 정상 값으로 바꾸려면 0.01f 곱해야함.
            float returnValue = value * percent * 0.01f;

            return returnValue;
        }

        /// <summary>
        /// 공격속도 공식
        /// </summary>
        /// <param name="value"></param>
        /// <param name="level"></param>
        /// <param name="property"></param>
        /// <param name="percentage"></param>
        /// <returns></returns>
        public float CalculateValue2(float value, int level, CardProperty property, int percentage = 100)
        {
            //카드 레벨에 의해 증감된 수치
            //(1레벨당 증가 값 * 레벨) + 기본벨류+[퍼센트 100 (200이면 크리티컬)]
            float percent = (property.IncreasedValuePerLevelUp * level) + property.Value + percentage;
            //Debug.Log("증가 퍼센트 = " + percent);
            //퍼센트 값을 정상 값으로 바꾸려면 0.01f 곱해야함.
            float returnValue = value / (percent * 0.01f);

            return returnValue;
        }

        /// <summary>
        /// 크리티컬 확률 공식
        /// 1-100사이 값 공식
        /// Random.Range(0,100);
        /// </summary>
        /// <param name="value"></param>
        /// <param name="level"></param>
        /// <param name="property"></param>
        /// <param name="percentage"></param>
        /// <returns></returns>
        public float CalculateValue3(float value, int level, CardProperty property)
        {
            float returnValue = (property.IncreasedValuePerLevelUp * level) + property.Value + value;   

            return returnValue;
        }

        /// <summary>
        /// 크리티컬 데미지 공식
        /// 크리 비율만 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="property"></param>
        /// <param name="percentage"></param>
        /// <returns></returns>
        public float CalculateValue4(float percentage,int level, CardProperty property)
        {
            //카드 레벨에 의해 증감된 수치
            float percent = (property.IncreasedValuePerLevelUp * level) + property.Value + percentage;
            return percent;
        }

        //같은 종류의 카드는 중첩 되지 않지만 다른 종류는 중첩된다
        /*public float DecreaseDamage(float damage, int level, CardProperty property)
        {
            //100을 기준으로 감소 퍼센트 계산
            //100기준값 - ((1 레벨당 추가 갑소 값 * level) + 기본 감소 수치)
            int percent = 100 - ((property.IncreasedValuePerLevelUp * level) + property.Value);

            //퍼센트 값을 정상 값으로 바꾸려면 0.01f 곱해야함.
            float returnValue = damage * percent * 0.01f;

            return returnValue;
        }*/

        /// <summary>
        /// 같은 프로퍼티를 추출
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /*public List<CardProperty> GetSameProperties(ref List<CardProperty> value)
        {
            List<CardProperty> returnValue = new List<CardProperty>();

            //returnValue 첫번째 프로퍼티가 기준
            returnValue.Add(value[0]);
            //기준을 잡은 후 리스트에서 제거
            value.RemoveAt(0);

            for (int i = 0; i < value.Count; i++)
            {
                //기준과 같은 타입 찾음 => 기준 returnValue[0]
                if (returnValue[0].Type == value[i].Type)
                {
                    returnValue.Add(value[i]);
                    value.RemoveAt(i);
                    //삭제 되었으니 i-1
                    i--;
                }
            }

            return returnValue;
        }*/

        /// <summary>
        //좀비가 영향을 받는 모든 데미지 계산
        //-계산범위
        // 무기 종류별
        // 탄약 종류별
        // 모든 무기 데미지
        // 모든 탄약 데미지
        // 네임드 몬스터
        // 일반 몬스터
        public float DamageCalculationTakenFromPlayer(bool isNamedZombie = false)
        { 
            //최대한 단순하게 계산하자-> 복잡하면 나중에 못알아 본다
            //카드 하나 하나 돌면서 증가, 감소 시킴
            CardProperty.PropertyType type;
            float damage = 0;
            int level = 0;
            int stackCount = 0;

            //계산전 데미지
            Debug.Log("처음 데미지   total  = " + (WeaponDamage + AmmoDamage) + "  weapon = " + WeaponDamage + " ammo = " + AmmoDamage);


            //무기 데미지와 탄약데미지 더한 값
            //*실제는 무기 데미지만 존재
            damage = WeaponDamage + AmmoDamage;

            //가진 카드에서 루프
            for (int i = 0; i < this.HasCards.Count; i++)
            {
                //활성 상태 카드만 검사
                if(this.HasCards[i].IsActivate)
                {
                    //프로퍼티 검사
                    for (int j = 0; j < this.HasCards[i].Properties.Count; j++)
                    {
                        //현재 카드의 타입
                        type = this.HasCards[i].Properties[j].Type;

                        if (this.PlayerWeapon == type                       //현재 플레이어의 무기와 같은가 
                            || this.PlayerAmmo == type                      //현재 플레이어의 탄약과 같은가 
                            || type == CardProperty.PropertyType.AllAmmoDamage      //모든 탄약 데미지 인가
                            || type == CardProperty.PropertyType.AllWeaponDamage   //모든 무기 데미지 인가
                            || (type == CardProperty.PropertyType.NamedZombieDamage && isNamedZombie)   //네임드 좀비 데미지 카드일때
                            || (type == CardProperty.PropertyType.GeneralZombieDamage && !isNamedZombie)//일반 좀비 카드 데미지 일때
                            )
                        {
                            //카드레벨 가져오기
                            level = this.HasCards[i].Level;

                            //프로퍼티 값이 음수이면 스택없이 1번만 계산
                            if(this.HasCards[i].Properties[j].Value < 0)
                            {
                                stackCount = 1;
                            } else//프로퍼티 값이 양수이면 스택 카운터 만큼 루프 
                            {
                                stackCount = this.HasCards[i].StackCount + 1;
                            }

                            //스택 카운트 만큼 추가
                            for (int n = 0; n < stackCount; n++)
                            {
                                //레벨 반영 데미지 계산
                                damage = CalculateValue(damage, level, this.HasCards[i].Properties[j]);
                                Debug.Log("중간 계산 토탈 = " + damage + "  level = " + level + " type = " + type.ToString());
                            }
                        }
                    }
                }
            }

            Debug.Log("최종 데미지 = " + damage);
            return damage;
        }

        /// <summary>
        /// 크리티컬 데미지 계산시 사용
        /// </summary>
        /// <returns></returns>
        public float DamageCalculationByCritical(float damage)
        {
            CardProperty.PropertyType type;
            float rate = 100;
            int level = 0;
            int stackCount = 0;

            //가진 카드에서 루프
            for (int i = 0; i < this.HasCards.Count; i++)
            {
                //활성 상태 카드만 검사
                if (this.HasCards[i].IsActivate)
                {
                    //프로퍼티 검사
                    for (int j = 0; j < this.HasCards[i].Properties.Count; j++)
                    {
                        //현재 카드의 타입
                        type = this.HasCards[i].Properties[j].Type;

                        if (type == CardProperty.PropertyType.CriticalDamage)
                        {
                            //카드레벨 가져오기
                            level = this.HasCards[i].Level;

                            //프로퍼티 값이 음수이면 스택없이 1번만 계산
                            if (this.HasCards[i].Properties[j].Value < 0)
                            {
                                stackCount = 1;
                            }
                            else//프로퍼티 값이 양수이면 스택 카운터 만큼 루프 
                            {
                                stackCount = this.HasCards[i].StackCount + 1;
                            }

                            //스택 카운트 만큼 추가
                            for (int n = 0; n < stackCount; n++)
                            {
                                //레벨 반영 크리데미지 향상 비율만 계산
                                rate = CalculateValue4(rate,level, this.HasCards[i].Properties[j]);
                                Debug.Log("중간 계산 토탈 = " + rate + "  level = " + level + " type = " + type.ToString());
                            }
                        }
                    }
                }
            }

            //최종 데미지에 향상된 크리데미지를 더해서 값을 돌려줌
            damage = damage * rate * 0.01f;

            Debug.Log("크리티컬 데미지 최종 = " + damage);

            return damage;
        }

        /// <summary>
        /// 몬스터 세팅에서 프로퍼티에서 저항 을 찾아서 데미지 계산
        /// - 계산범위
        /// - 모든 종류 총기 저항
        /// - 모든 종류 탄약 저항
        /// - 모든 총기 저항
        /// - 모든 탄약 저항
        /// 
        /// </summary>
        /// <returns>계산 반여아여 데미지 리턴</returns>
        public float DamageCalculationReflectingMonsterResistance(float damage,List<CardProperty> properties, int monsterLevel)
        {
            int count = 0;

            for (int i = 0; i < properties.Count; i++)
            {
                if(((int)this.PlayerWeapon + 300) == (int)properties[i].Type  //현재 플레이어의 무기와 카드의 저항 무기가 같은가
                   || ((int)this.PlayerAmmo + 300) == (int)properties[i].Type    //현재 플레이어의 탄약과 카드의 저항 탄약이 같은가
                   || properties[i].Type == CardProperty.PropertyType.AllAmmoDamageResistance  //모든 탄약 저항 인가
                   || properties[i].Type == CardProperty.PropertyType.AllWeaponDamageResistance //모든 무기 저항 인가
                   )
                {
                    //레벨 반영 데미지 계산
                    damage = CalculateValue(damage, monsterLevel, properties[i]);
                    Debug.Log("저항 타입 = " + properties[i].Type.ToString() + " 중간 계산 토탈 = " + damage + "  level = " + monsterLevel );
                    //반영된 프로퍼티가 있는지 확인용 카운트
                    count++;
                }
            }

            if(count == 0) { Debug.Log("반영할 몬스터 저항이 없습니다."); }

            return damage;
        }

        //*테스트 필요
        //최대 체력 증가 -> 테스트 필요
        //카드 타입만 바꾸면 종류별로 테스트 가능
        //-체력 포인트      => CardSetting.CardType.MaxHP
        //-치명타 데미지     => CardSetting.CardType.CriticalDamage
        //-공습 데미지      => CardSetting.CardType.AirStrikeDamage
        //-돈             => CardSetting.CardType.Coin
        //-경험치          => CardSetting.CardType.Exp
        public float GetValue(CardSetting.CardType cardType)
        {
            int level = 0;
            float total = 0;
            int stackCount = 0;

            //가진 카드에서 루프
            for (int i = 0; i < this.HasCards.Count; i++)
            {
                //활성 상태 카드만 검사 && MaxHp타입인가
                if (this.HasCards[i].IsActivate && this.HasCards[i].Type == cardType)
                {
                    //MaxHP value에 기본값이 들어 있음
                    total = this.HasCards[i].Value;

                    //프로퍼티 검사
                    for (int j = 0; j < this.HasCards[i].Properties.Count; j++)
                    {
                        //카드레벨 가져오기
                        level = this.HasCards[i].Level;

                        //스택 카운터 만큼 루프
                        stackCount = this.HasCards[i].StackCount + 1;

                        //스택 카운트 만큼 추가
                        for (int n = 0; n < stackCount; n++)
                        {
                            //레벨 반영 데미지 계산
                            total = CalculateValue(total, level, this.HasCards[i].Properties[j]);
                            Debug.Log("중간 계산 토탈 = " + total + "  level = " + level + " type = " + this.HasCards[i].Properties[j].Type.ToString());
                        }
                    }
                }
            }

            return total;
        }


        /// <summary>
        ///*테스트 필요
        /// 공격속도
        /// 사격간 딜레이 수치로
        /// GunSetting.cs RapidFireCooldown이며 0.4 - 1.5로 낮을수로 빨리 사격
        /// 사전에 선택한 총이 뭔지 알고 있다는 전제가 필요 
        /// </summary>
        /// <param name="defaultSpeed">선택된 총의 기본 스피드</param>
        /// <returns></returns>
        public float GetAttackSpeed(float defaultSpeed)
        {
            float speed = defaultSpeed;
            int level = 0;
            int stackCount = 0;

            //가진 카드에서 루프
            for (int i = 0; i < this.HasCards.Count; i++)
            {
                //활성 상태 카드만 검사 && MaxHp타입인가
                if (this.HasCards[i].IsActivate && this.HasCards[i].Type == CardSetting.CardType.AttackSpeed)
                {
                    //프로퍼티 검사
                    for (int j = 0; j < this.HasCards[i].Properties.Count; j++)
                    {
                        //카드레벨 가져오기
                        level = this.HasCards[i].Level;

                        //스택 카운터 만큼 루프
                        stackCount = this.HasCards[i].StackCount + 1;

                        //스택 카운트 만큼 추가
                        for (int n = 0; n < stackCount; n++)
                        {
                            //레벨 반영 데미지 계산
                            speed = CalculateValue2(speed, level, this.HasCards[i].Properties[j]);
                            Debug.Log("중간 계산 토탈 = " + speed + "  level = " + level + " type = " + this.HasCards[i].Properties[j].Type.ToString());
                        }
                    }
                }
            }

            return speed;
        }

        /// <summary>
        /// 치명 기회
        /// 크리티컬 기본 확률은 10이며 카드를 확인해서 추가적으로 확율을
        /// 증감해서 크리인지 아닌지 안려줌
        /// </summary>
        /// <param name="defaultSpeed"></param>
        /// <returns></returns>
        public bool IsCriticalDamage(float defaultCriticalRate = 10f)
        {
            bool isCriticalDamage = false;
            int level = 0;
            int stackCount = 0;

            //Debug.Log("기본 크리티컬 defaultCriticalRate = " + defaultCriticalRate);

            //가진 카드에서 루프
            for (int i = 0; i < this.HasCards.Count; i++) 
            {
                //활성 상태 카드만 검사 && MaxHp타입인가
                if (this.HasCards[i].IsActivate && this.HasCards[i].Type == CardSetting.CardType.CriticalChance)
                {
                    //프로퍼티 검사
                    for (int j = 0; j < this.HasCards[i].Properties.Count; j++)
                    {
                        //카드레벨 가져오기
                        level = this.HasCards[i].Level;

                        //스택 카운터 만큼 루프
                        stackCount = this.HasCards[i].StackCount + 1;

                        //스택 카운트 만큼 추가
                        for (int n = 0; n < stackCount; n++)
                        {
                            //레벨 반영 데미지 계산
                            defaultCriticalRate = CalculateValue3(defaultCriticalRate, level, this.HasCards[i].Properties[j]);
                            //Debug.Log("중간 계산 토탈 = " + defaultCriticalRate + "  level = " + level + " type = " + this.HasCards[i].Properties[j].Type.ToString());
                        }
                    }
                }
            }

            int rand = Random.Range(0, 1000);

            //랜덤 1000에서 구하기 위해 10을 곱셈해서 계산
            defaultCriticalRate *= 10;

            //랜덤값이 크리 확률값에 포함 된다면 크리 발생.            
            if (rand <= defaultCriticalRate) { isCriticalDamage = true; }

            //Debug.Log("기본 크리티컬 발생 비기본 크리티컬 발생 비율 = " + defaultCriticalRate + " 랜덤값 = " + rand + " 크리발생 " + isCriticalDamage.ToString());

            return isCriticalDamage;
        }

        //공습 체움 속도
        //모든 데미지에 대한 저항


        /// <summary>
        /// 활성카드의 프로퍼티만 가져옴
        /// </summary>
        /// <param name="hasCards"></param>
        /// <returns></returns>
        /*public List<CardProperty> GetActivatedCardProperties(List<CardSetting> hasCards)
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
        }*/


        /// <summary>
        /// 활성카드의 긍정 프로퍼티만 가져옴
        /// </summary>
        /// <param name="hasCards"></param>
        /// <returns></returns>
        /*public List<CardProperty> GetActivatedCardPositiveProperties(List<CardSetting> hasCards)
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
        }*/

        /// <summary>
        /// 활성카드의 부정 프로퍼티만 가져옴
        /// </summary>
        /// <param name="hasCards"></param>
        /// <returns></returns>
        /*public List<CardProperty> GetActivatedCardNegativeProperties(List<CardSetting> hasCards)
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
        }*/



        
        #region [-TestCode]
        void Update()
        {
            //중복 체크하면 카드 중첩 잘되는지 테스트
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                for (int i = 0; i < CardsPlayerOnClicked.Count; i++)
                {
                    Debug.Log("===============> 추가할 카드 = " + CardsPlayerOnClicked[i].name + " <==============");
                    AddCard(CardsPlayerOnClicked[i]);

                    //CardsPlayerOnClicked.Clear();
                }
            }

            //프로퍼티의 값들을 더하기 
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                float total = 0;
                //몬스터가 받는 기본 데미지 테스트
                //데미지를 받는 대상이 네임드 좀비 라면 true 필요
                Debug.Log("===============> 몬스터가 받는 기본 데미지 계산 <===============");
                total = DamageCalculationTakenFromPlayer(true);

                Debug.Log("===============> 몬스터가 받는 크리티컬 데미지 계산 <===============");
                DamageCalculationByCritical(total);

                //몬스터의 저항에 의한 데미지 테스트
                //Debug.Log("===============> 몬스터 저항 계산 <===============");
                //DamageCalculationReflectingMonsterResistance(total,monstersProperties, 0);

                //스피드 테스트 -> TestGunSetting필요.
                //GetAttackSpeed(TestGunSetting.rapidFireCooldown);

                //크리티컬 테스트
                //IsCriticalDamage();
            }
        }
        #endregion
        
    }
}
