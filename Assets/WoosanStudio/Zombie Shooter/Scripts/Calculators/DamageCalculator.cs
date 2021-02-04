using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 모든 데미지를 무기 타입과 레벨에 맞게 계산
    /// CardManager에서 하는 데미지 계산을 여기로 이전해야함
    /// </summary>
    public class DamageCalculator : MonoBehaviour
    {
        static public DamageCalculator Instance;

        //[Header("[[테스트용 삭제예정] 로비 선택 카드 => 테스트 용이며 직접 사용하지 않는다]")]
        //public List<CardSetting> CardSettingsReference = new List<CardSetting>();

        //[Header("[테스트용을 복사한 카드 => [삭제예정]]")]
        //public List<CardSetting> CardsPlayerOnClicked;

        //[Header("[로비에서 선택한 카드]")]
        //public List<CardSetting> CardsSelectedInRobby;

        //[Header("[[테스트용]몬스터의 프로퍼티]")]
        //public List<CardProperty> monstersProperties;

        //[Header("[[테스트용]건데이터")]
        //public GunSettings TestGunSetting;

        //[Header("[선택되어 가지고 있는 카드]")]
        //public List<CardSetting> HasCards;

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

        //[Header("[현재 플레이어의 무기]")]
        //public CardProperty.PropertyType PlayerWeapon = CardProperty.PropertyType.PistolDamage;
        //[Header("[현재 플레이어의 탄약]")]
        //public CardProperty.PropertyType PlayerAmmo = CardProperty.PropertyType.BulletAmmoDamage;
        [Header("[현재 플레이어의 무기 데미지 => 계산용]")]
        public float WeaponDamage = 0;
        [Header("[현재 플레이어의 탄약 데미지 => 계산용]")]
        public float AmmoDamage = 0;
        [Header("[현재 플레이어의 체력 => 계산용]")]
        public float HealthPoint = 0;

        //[Header("[기본 크리티컬 데미지 비율 => 총괄하는 하나의 데이터로 리팩토링 필요]")]
        //public int DefaultCriticalDamageRate = GlobalDataController.DefaultCriticalDamage;
        //[Header("[기본 크리티컬 확률 비율 => 총괄하는 하나의 데이터로 리팩토링 필요]")]
        //public float DefaultCriticalProbabilityRate = GlobalDataController.DefaultCriticalChance;

        //실제 사용은 이걸로 해야함.
        //*테스트할 때는 눈으로 봐야하기 때문에 CardSettings 사용
        public List<ICard> cards;

        [Header("[테스트용 몬스터 -> 나중에 삭제필요]")]
        public MonsterSettings TestMonsterSettings;

        [Header("[테스트용 반영된 총 -> 나중에 삭제필요]")]
        public CardSetting TestWeaponCard;

        [Header("[테스트용 반영된 탄약 -> 나중에 삭제필요]")]
        public CardSetting TestAmmoCard;

        [Header("[테스트용 반영된 모든 카드 -> 나중에 삭제필요]")]
        public List<CardSetting> TestSelectedCard = new List<CardSetting>();

        //캐쉬ㅇ
        private List<CardSetting> hasCards;

        private void Awake()
        {
            if (null == Instance)
            {
                //싱글톤 패턴
                Instance = this;

                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// 게임에 영향을 미치는 모든카드
        /// * StackCount가 1이상인 모든 카드
        /// </summary>
        /// <returns></returns>
        private List<CardSetting> GetHasCards(List<CardSetting> origin)
        {
            List<CardSetting> cardSettings = new List<CardSetting>();
            origin.ForEach(value =>
            {
                if (0 < value.StackCount)
                {
                    cardSettings.Add(value);
                }
            });

            return cardSettings; 
        }

        /// <summary>
        /// 데미지, 체력, 치명데미지 공식
        /// </summary>
        /// <param name="value">무기와 탄약을 더한 값</param>
        /// <param name="level">카드 레밸</param>
        /// <param name="property">카드 프로퍼티 값</param>
        /// <param name="percentage">기본 100% 이며 크리티컬 값을 구할시 200%으로 변경</param>
        /// <returns></returns>
        public float CalculateValue(float value, int level, CardProperty property,int stack, int percentage = 100)
        {
            //카드 레벨에 의해 증감된 수치
            //(1레벨당 증가 값 * 레벨) + 기본벨류+[퍼센트 100 (200이면 크리티컬)]
            float percent = ((property.IncreasedValuePerLevelUp * level) + property.Value) * stack + percentage;

            //Debug.Log("증가 퍼센트 = [" + percent + "]");
            
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
        public float CalculateValue2(float value, int level, CardProperty property,int stack ,int percentage = 100)
        {
            //카드 레벨에 의해 증감된 수치
            //(1레벨당 증가 값 * 레벨) + 기본벨류+[퍼센트 100 (200이면 크리티컬)]

            //Debug.Log("((" + property.IncreasedValuePerLevelUp + "*" + level + ") + " + property.Value + ") * " + stack + "+" + percentage);
            float percent = ((property.IncreasedValuePerLevelUp * level) + property.Value) * stack + percentage;
            //Debug.Log("증가 퍼센트 = " + percent);
            //퍼센트 값을 정상 값으로 바꾸려면 0.01f 곱해야함.
            float returnValue = value / (percent * 0.01f);
            //Debug.Log("리턴 값 = " + returnValue);

            return returnValue;
        }

        /// <summary>
        /// 크리티컬 확률 공식
        /// 1-100사이 값 공식
        /// Random.Range(0,100);
        /// * 스택 곱셈이 이상해 보이지만 테스트 완료한 것
        /// </summary>
        /// <param name="value"></param>
        /// <param name="level"></param>
        /// <param name="property"></param>
        /// <param name="percentage"></param>
        /// <returns></returns>
        public float CalculateValue3(float value, int level,int stack, CardProperty property)
        {
            float returnValue = ((property.IncreasedValuePerLevelUp * level) + property.Value + value) * stack;

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
        public float CalculateValue4(float percentage, int level, int stack,CardProperty property)
        {
            //카드 레벨에 의해 증감된 수치
            float percent = ((property.IncreasedValuePerLevelUp * level) + property.Value) * stack + percentage;
            return percent;
        }


        /// <summary>
        /// 공습 채움 속도
        /// 1~ 그이상 1이 기본
        /// *증가되는 값은 100을 넘을수 없다
        /// </summary>
        /// <param name="level"></param>
        /// <param name="property"></param>
        /// <param name="percentage"></param>
        /// <returns></returns>
        public float CalculateValue5(int level, int stack, CardProperty property)
        {
            //기본 값 = 1 , 레벨 = 0-25 ,스택 = 1-5,프로퍼티 기본 값 = 2 ,프로퍼티 기본 값 = 10

            //카드 레벨에 의해 증감된 수치
            //ex) 45% 증가 라면 100-45 = 55
            float percent = 100 - ((property.IncreasedValuePerLevelUp * level) + property.Value) * stack;

            //1이 Max 임으로 50이면 0.5로 변경
            percent *= 0.01f;

            return percent;
        }

        /// <summary>
        /// 최대 탄약 공식
        /// </summary>
        /// <param name="value"></param>
        /// <param name="level"></param>
        /// <param name="property"></param>
        /// <param name="percentage"></param>
        /// <returns></returns>
        public int CalculateValue6(int value, int level, CardProperty property, int stack, int percentage = 100)
        {
            //카드 레벨에 의해 증감된 수치
            //(1레벨당 증가 값 * 레벨) + 기본벨류+[퍼센트 100 (200이면 크리티컬)]

            //Debug.Log("((" + property.IncreasedValuePerLevelUp + "*" + level + ") + " + property.Value + ") * " + stack + "+" + percentage);
            float percent = ((property.IncreasedValuePerLevelUp * level) + property.Value) * stack + percentage;
            //Debug.Log("증가 퍼센트 = " + percent);
            //퍼센트 값을 정상 값으로 바꾸려면 0.01f 곱해야함.
            int returnValue = Mathf.FloorToInt(value * (percent * 0.01f));
            //Debug.Log("리턴 값 = " + returnValue);

            return returnValue;
        }

        /// <summary>
        /// 공습 데미지 가져오기
        /// </summary>
        public float GetAirStrikeDamage(MonsterSettings monster)
        {
            //임시 저장용
            CardProperty.PropertyType type;
            int level = 0;
            int stackCount = 0;

            //공습 기본 데미지
            //*나중에 바꿔야함
            float damage = 1000f;

            //적용된 모든 카드
            //*중첩 된 카드 가져오기 -> 데미지에 영향을 미치는 모든 카드
            hasCards = GetHasCards(GlobalDataController.Instance.SelectAbleAllCard);

            //추가 데미지 반영 부분
            for (int i = 0; i < hasCards.Count; i++)
            {
                //프로퍼티 검사
                for (int j = 0; j < hasCards[i].Properties.Count; j++)
                {
                    //현재 카드의 타입
                    type = hasCards[i].Properties[j].Type;

                    if (type == CardProperty.PropertyType.AirStrikeDamage)
                    {
                        //카드 레벨 가져옴
                        level = hasCards[i].Level;
                        //카드 스택 가져옴
                        stackCount = hasCards[i].StackCount;
                        //프로퍼티 값이 음수이면 스택없이 1번만 계산
                        if (hasCards[i].Properties[j].Value < 0) { stackCount = 1; }

                        //테스트용 -> 나중에 삭제
                        float origin = damage;
                        //레벨,스택 반영 데미지 계산
                        damage = CalculateValue(damage, level, hasCards[i].Properties[j], stackCount);

                        //테스트용 -> 나중에 삭제
                        int totalRate = (((int)hasCards[i].Properties[j].IncreasedValuePerLevelUp * level) + hasCards[i].Properties[j].Value);
                        //Debug.Log("최초 데미지 = [" + origin + "] 중간 계산 데미지 = [" + damage + "]  Total Rate = [" + totalRate + "] level = [" + level + "] Stack = [" + stackCount + "]  card = [" + hasCards[i].Type.ToString() + "] type = [" + type.ToString() + "]");
                    }
                }
            }

            //저항에 의한 데미지 감소 반영 부분
            for (int i = 0; i < monster.Propertys.Count; i++)
            {
                //몬스터에 저항이 있는지 확인
                if (monster.Propertys[i].Type == CardProperty.PropertyType.AirStrikeDamageResistance )
                {
                    //레벨 반영 데미지 계산
                    damage = CalculateValue(damage, monster.Level, monster.Propertys[i], 1);
                    //Debug.Log("저항 타입 = " + monster.Propertys[i].Type.ToString() + " 중간 계산 토탈 = " + damage + "  level = " + monster.Level);
                }
            }

            //테스트 조건 모두 통과
            //1. 스택 증가 데미지 확인
            //2. 저항에 의한 데미지 감소 확인

            return damage;
        }

        /// <summary>
        /// 공습 채움 값 가져오기
        /// </summary>
        public float GetAirStrikeRechargeValue()
        {
            //임시 저장용
            CardProperty.PropertyType type;
            int level = 0;
            int stackCount = 0;
            //기본 1로 계산 -> 0-1사이 값
            float recharge = 1;

            //적용된 모든 카드
            //*중첩 된 카드 가져오기 -> 데미지에 영향을 미치는 모든 카드
            hasCards = GetHasCards(GlobalDataController.Instance.SelectAbleAllCard);

            //추가 데미지 반영 부분
            for (int i = 0; i < hasCards.Count; i++)
            {
                //프로퍼티 검사
                for (int j = 0; j < hasCards[i].Properties.Count; j++)
                {
                    //현재 카드의 타입
                    type = hasCards[i].Properties[j].Type;

                    if (type == CardProperty.PropertyType.AngerRecharge)
                    {
                        //카드 레벨 가져옴
                        level = hasCards[i].Level;
                        //카드 스택 가져옴
                        stackCount = hasCards[i].StackCount;
                        //프로퍼티 값이 음수이면 스택없이 1번만 계산
                        if (hasCards[i].Properties[j].Value < 0) { stackCount = 1; }
                        //레벨,스택 반영 데미지 계산
                        recharge = CalculateValue5( level, stackCount, hasCards[i].Properties[j]);

                        //테스트용 -> 나중에 삭제
                        //int totalRate = (((int)hasCards[i].Properties[j].IncreasedValuePerLevelUp * level) + hasCards[i].Properties[j].Value);
                        //Debug.Log("최종 리차징 값 = [" + recharge + "]  Total Rate = [" + totalRate + "] level = [" + level + "] Stack = [" + stackCount + "]  card = [" + hasCards[i].Type.ToString() + "] type = [" + type.ToString() + "]");
                    }
                }
            }

            //테스트 조건 모두 통과
            //1. 증가 할수록 리차징 값이 감소 여부 확인

            return recharge;
        } 

        /// <summary>
        //좀비가 플레이어에게 받은 모든 데미지 계산
        //*크리티컬은 제외된 퓨어 데미지
        //-계산범위
        // 무기 종류별
        // 탄약 종류별
        // 모든 무기 데미지
        // 모든 탄약 데미지
        public float GetHitDamageFromPlayer(MonsterSettings monster)
        {
            //임시 저장용
            CardProperty.PropertyType type;
            int level = 0;
            int stackCount = 0;
            bool isNamedZombie = false;

            //총 데미지와 탄약 데미지 두개를 더한 값이 데미지.
            //*실제 탄약의 데미지는 0이라 의미없긴 하다.
            float damage = GlobalDataController.SelectedWeaponCard.Value + GlobalDataController.SelectedAmmoCard.Value;

            //테스트용-> 삭제필요
            TestWeaponCard = GlobalDataController.SelectedWeaponCard;
            TestAmmoCard = GlobalDataController.SelectedAmmoCard;

            //적용된 모든 카드
            //*중첩 된 카드 가져오기 -> 데미지에 영향을 미치는 모든 카드
            hasCards = GetHasCards(GlobalDataController.Instance.SelectAbleAllCard);

            //테스트용-> 삭제필요
            TestSelectedCard = hasCards;

            //카드 타입을 프로퍼티 타입으로 강제 변환후 넣음
            CardProperty.PropertyType weaponType = (CardProperty.PropertyType)(int)GlobalDataController.SelectedWeaponCard.Type;

            //카드 타입을 프로퍼티 타입으로 강제 변환후 넣음
            CardProperty.PropertyType ammoType = (CardProperty.PropertyType)(int)GlobalDataController.SelectedAmmoCard.Type;

            //네임드 좀비 확인
            //저항 데미지 계산이 아닌 프로퍼티 속성 반영때문에 사용
            //*1000 번 이상은 네임드 
            if (1000 <= (int)monster.MonsterId) { isNamedZombie = true; }

            //Debug.Log("=> 현재 선택된 무기 = [" + weaponType.ToString()+ "]  탄약 = [" + ammoType.ToString()+"]");

            //카드 검사
            for (int i = 0; i < hasCards.Count; i++)
            {
                //프로퍼티 검사
                for (int j = 0; j < hasCards[i].Properties.Count; j++)
                {
                    //현재 카드의 타입
                    type = hasCards[i].Properties[j].Type;

                    if (weaponType == type                       //현재 플레이어의 무기와 같은가 
                            || ammoType == type                      //현재 플레이어의 탄약과 같은가 
                            || type == CardProperty.PropertyType.AllAmmoDamage      //모든 탄약 데미지 인가
                            || type == CardProperty.PropertyType.AllWeaponDamage   //모든 무기 데미지 인가
                            || (type == CardProperty.PropertyType.NamedZombieDamage && isNamedZombie)   //네임드 좀비 데미지 카드일때
                            || (type == CardProperty.PropertyType.GeneralZombieDamage && !isNamedZombie)//일반 좀비 카드 데미지 일때
                            )
                    {
                        //카드 레벨 가져옴
                        level = hasCards[i].Level;
                        //카드 스택 가져옴
                        stackCount = hasCards[i].StackCount;
                        //프로퍼티 값이 음수이면 스택없이 1번만 계산
                        if (hasCards[i].Properties[j].Value < 0) { stackCount = 1; }

                        //테스트용 -> 나중에 삭제
                        float origin = damage;
                        //레벨,스택 반영 데미지 계산
                        damage = CalculateValue(damage, level, hasCards[i].Properties[j], stackCount);

                        //테스트용 -> 나중에 삭제
                        //int totalRate = (((int)hasCards[i].Properties[j].IncreasedValuePerLevelUp * level) + hasCards[i].Properties[j].Value);
                        //Debug.Log("최초 데미지 = ["+ origin + "] 중간 계산 데미지 = [" + damage + "]  Total Rate = ["+ totalRate + "] level = [" + level + "] Stack = [" + stackCount + "]  card = ["+ hasCards[i].Type.ToString()+ "] type = [" + type.ToString()+"]");

                        //테스트 조건 모두 통과
                        //1. 스택 증가
                        //2. 데미지 변경
                        //3. 레벨 변경
                        //4. 1틱당 증가 레벨 변경
                    }
                }
            }
            return damage;
        }

        /// <summary>
        /// 치명 기회
        /// 크리티컬 기본 확률은 5이며 카드를 확인해서 추가적으로 확율을
        /// 증감해서 크리인지 아닌지 안려줌
        /// </summary>
        /// <param name="defaultSpeed"></param>
        /// <returns></returns>
        public bool IsCriticalDamage()
        {
            bool isCriticalDamage = false;
            float defaultCriticalRate = GlobalDataController.DefaultCriticalChance;
            int level = 0;
            int stackCount = 0;

            //선택된 카드 가져오기
            hasCards = GetHasCards(GlobalDataController.Instance.SelectAbleAllCard);

            //가진 카드에서 루프
            for (int i = 0; i < hasCards.Count; i++)
            {
                //활성 상태 카드만 검사 && MaxHp타입인가
                if (hasCards[i].Type == CardSetting.CardType.CriticalChance)
                {
                    //프로퍼티 검사
                    for (int j = 0; j < hasCards[i].Properties.Count; j++)
                    {
                        //카드레벨 가져오기
                        level = hasCards[i].Level;

                        //카드 스택 가져옴
                        stackCount = hasCards[i].StackCount;
                        //프로퍼티 값이 음수이면 스택없이 1번만 계산
                        if (hasCards[i].Properties[j].Value < 0) { stackCount = 1; }

                        //스택 카운트 만큼 추가
                        //for (int n = 0; n < stackCount; n++)
                        //{
                        //레벨 반영 데미지 계산
                        defaultCriticalRate = CalculateValue3(defaultCriticalRate, level, stackCount, hasCards[i].Properties[j]);
                        //Debug.Log("크리 중간 비율 = [" + defaultCriticalRate + "]  level = [" + level + "] Stack Count = [" + stackCount + "] type = [" + hasCards[i].Properties[j].Type.ToString()+"]");
                        //}
                    }
                }
            }

            int rand = Random.Range(0, 1000);

            //랜덤 1000에서 구하기 위해 10을 곱셈해서 계산
            defaultCriticalRate *= 10;

            //랜덤값이 크리 확률값에 포함 된다면 크리 발생.            
            if (rand <= defaultCriticalRate) { isCriticalDamage = true; }

            //Debug.Log("(1-1000)크리 발생 비율 = " + defaultCriticalRate + " 랜덤값 = " + rand + " 크리발생 " + isCriticalDamage.ToString());

            //테스트 조건
            //1 스택 증가에 따른 크리 발생률 증가 테스트

            return isCriticalDamage;
        }

        /// <summary>
        /// 크리티컬 데미지 계산시 사용
        /// </summary>
        /// <returns></returns>
        public float GetCriticalDamage(float damage)
        {
            CardProperty.PropertyType type;
            //크리 기본 데미지
            float rate = GlobalDataController.DefaultCriticalDamage;
            int level = 0;
            int stackCount = 0;

            //선택된 카드 가져오기
            hasCards = GetHasCards(GlobalDataController.Instance.SelectAbleAllCard);

            //가진 카드에서 루프
            for (int i = 0; i < hasCards.Count; i++)
            {
                //프로퍼티 검사
                for (int j = 0; j < hasCards[i].Properties.Count; j++)
                {
                    //현재 카드의 타입
                    type = hasCards[i].Properties[j].Type;

                    if (type == CardProperty.PropertyType.CriticalDamage)
                    {
                        //카드레벨 가져오기
                        level = hasCards[i].Level;
                        //카드 스택 가져옴
                        stackCount = hasCards[i].StackCount;
                        //프로퍼티 값이 음수이면 스택없이 1번만 계산
                        if (hasCards[i].Properties[j].Value < 0) { stackCount = 1; }

                        //스택 카운트 만큼 추가
                        //for (int n = 0; n < stackCount; n++)
                        //{
                            //레벨 반영 크리데미지 향상 비율만 계산
                        rate = CalculateValue4(rate, level, stackCount, hasCards[i].Properties[j]);
                        //Debug.Log("중간 계산 토탈 = " + rate + "  level = " + level + " type = " + type.ToString());
                        //}
                    }
                }
            }

            //최종 데미지에 향상된 크리데미지를 더해서 값을 돌려줌
            damage = damage * rate * 0.01f;

            //Debug.Log("크리티컬 데미지 최종 = [" + damage +"] 적용 비율 = ["+rate+"]");

            //테스트 조건
            //1 스택 증가에 따른 크리 데미지 증가 테스트

            return damage;
        }

        /// <summary>
        /// 몬스터 저항이 반영된 데미지 가져오기
        /// - 계산범위
        /// - 모든 종류 총기 저항
        /// - 모든 종류 탄약 저항
        /// - 각 총기 저항
        /// - 각 탄약 저항
        /// 
        /// </summary>
        /// <returns>계산 반여아여 데미지 리턴</returns>
        public float GetDamageThatReflectsMonsterResistance(float damage, List<CardProperty> properties, int monsterLevel)
        {
            int count = 0;

            //무기 100~ -> 무기 저항 400~
            //총알 200~ -> 총알 저항 500~
            //비교를 위해 int로 변환 * +300필요
            int weaponValue = (int)GlobalDataController.SelectedWeaponCard.Type + 300;
            //비교를 위해 int로 변환 * +300필요
            int ammoValue = (int)GlobalDataController.SelectedAmmoCard.Type + 300;

            for (int i = 0; i < properties.Count; i++)
            {
                //현재 무기와 같은 타입의 저항이 있는지 찾음
                if (weaponValue == (int)properties[i].Type  //현재 플레이어의 무기와 카드의 저항 무기가 같은가
                   || ammoValue == (int)properties[i].Type    //현재 플레이어의 탄약과 카드의 저항 탄약이 같은가
                   || properties[i].Type == CardProperty.PropertyType.AllAmmoDamageResistance  //모든 탄약 저항 인가
                   || properties[i].Type == CardProperty.PropertyType.AllWeaponDamageResistance //모든 무기 저항 인가
                   )
                {
                    //레벨 반영 데미지 계산
                    damage = CalculateValue(damage, monsterLevel, properties[i],1);
                    //Debug.Log("저항 타입 = " + properties[i].Type.ToString() + " 중간 계산 토탈 = " + damage + "  level = " + monsterLevel);
                    //반영된 프로퍼티가 있는지 확인용 카운트
                    count++;
                }
            }

            //테스트 조건
            //1.모든 종류 총기 저항 확인
            //2.모든 종류 탄약 저항 확인
            //3.각 총기 저항 확인
            //4.각 탄약 저항 확인.
            //5.몬스터 레벨에 영향 확인

            //저항 계산 확인용
            //if (count == 0) { Debug.Log("반영할 몬스터 저항이 없습니다."); }

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

            //선택된 카드 가져오기
            hasCards = GetHasCards(GlobalDataController.Instance.SelectAbleAllCard);

            //가진 카드에서 루프
            for (int i = 0; i < hasCards.Count; i++)
            {
                //활성 상태 카드만 검사 && MaxHp타입인가
                if (hasCards[i].Type == cardType)
                {
                    //MaxHP value에 기본값이 들어 있음
                    total = hasCards[i].Value;

                    //프로퍼티 검사
                    for (int j = 0; j < hasCards[i].Properties.Count; j++)
                    {
                        //카드레벨 가져오기
                        level = hasCards[i].Level;

                        //스택 카운터 만큼 루프
                        stackCount = hasCards[i].StackCount + 1;

                        //스택 카운트 만큼 추가
                        for (int n = 0; n < stackCount; n++)
                        {
                            //레벨 반영 데미지 계산
                            total = CalculateValue(total, level, hasCards[i].Properties[j],1);
                            //Debug.Log("중간 계산 토탈 = " + total + "  level = " + level + " type = " + hasCards[i].Properties[j].Type.ToString());
                        }
                    }
                }
            }

            return total;
        }


        /// <summary>
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

            //선택된 카드 가져오기
            hasCards = GetHasCards(GlobalDataController.Instance.SelectAbleAllCard);

            //Debug.Log("카드수  = " + hasCards.Count);

            //가진 카드에서 루프
            for (int i = 0; i < hasCards.Count; i++)
            {
                //활성 상태 카드만 검사 && MaxHp타입인가
                if ( hasCards[i].Type == CardSetting.CardType.AttackSpeed)
                {
                    //프로퍼티 검사
                    for (int j = 0; j < hasCards[i].Properties.Count; j++)
                    {
                        //카드레벨 가져오기
                        level = hasCards[i].Level;

                        //카드 스택 가져옴
                        stackCount = hasCards[i].StackCount;
                        //프로퍼티 값이 음수이면 스택없이 1번만 계산
                        if (hasCards[i].Properties[j].Value < 0) { stackCount = 1; }

                        //스택 카운트 만큼 추가
                        //for (int n = 0; n < stackCount; n++)
                        //{
                        //레벨 반영 데미지 계산
                        speed = CalculateValue2(speed, level ,hasCards[i].Properties[j], stackCount);
                        //Debug.Log("중간 계산 토탈 = " + speed + "  level = " + level + " type = " + hasCards[i].Properties[j].Type.ToString());
                        //}
                    }
                }
            }

            return speed;
        }

        /// <summary>
        /// 카드 반영된 최대 탄약값 가져오기
        /// </summary>
        /// <param name="defaultSpeed">선택된 총의 기본 스피드</param>
        /// <returns></returns>
        public int GetMaxAmmo(int defaultAmmo)
        {
            int maxAmmo = defaultAmmo;
            int level = 0;
            int stackCount = 0;

            //선택된 카드 가져오기
            hasCards = GetHasCards(GlobalDataController.Instance.SelectAbleAllCard);

            //Debug.Log("카드수  = " + hasCards.Count);

            //가진 카드에서 루프
            for (int i = 0; i < hasCards.Count; i++)
            {
                //활성 상태 카드만 검사 && MaxHp타입인가
                if (hasCards[i].Type == CardSetting.CardType.MagazineCapacity)
                {
                    //프로퍼티 검사
                    for (int j = 0; j < hasCards[i].Properties.Count; j++)
                    {
                        //카드레벨 가져오기
                        level = hasCards[i].Level;

                        //카드 스택 가져옴
                        stackCount = hasCards[i].StackCount;
                        //프로퍼티 값이 음수이면 스택없이 1번만 계산
                        if (hasCards[i].Properties[j].Value < 0) { stackCount = 1; }

                        //스택 카운트 만큼 추가
                        //for (int n = 0; n < stackCount; n++)
                        //{
                        //레벨 반영 데미지 계산
                        maxAmmo = CalculateValue6(maxAmmo, level, hasCards[i].Properties[j], stackCount);
                        //Debug.Log("중간 계산 토탈 = " + maxAmmo + "  level = " + level + " type = " + hasCards[i].Properties[j].Type.ToString());
                        //}
                    }
                }
            }

            return maxAmmo;
        }

        #region [-TestCode]
        //void Update()
        //{
        /*
        //테스트용 데미지 반영
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //몬스터에게 주는 데미지
            float damage = GetHitDamageFromPlayer(TestMonsterSettings);
            //크리 인가?
            if (IsCriticalDamage())
            {
                //크리이면 크리계산
                damage = GetCriticalDamage(damage);
            }

            //저항이 존제하면 저항 계산
            //if(0 < TestMonsterSettings.Propertys.Count)
            //{
            //    GetDamageThatReflectsMonsterResistance(damage, TestMonsterSettings.Propertys, TestMonsterSettings.Level);
            //}
        }
        */

        //공습 데미지 계산
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    GetAirStrikeDamage(TestMonsterSettings);
        //}

        //공습 채움 값 가져오기
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    GetAirStrikeRechargeValue();
        //}
        //}
        #endregion

    }
}
