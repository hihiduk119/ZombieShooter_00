using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;
using System;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드의 정보와 고유 아이디 및 성능
    /// *그대로 사용하면 안돼고 Instantiate해서 사용해야함.
    /// </summary>
    [CreateAssetMenu(menuName = "ZombieShooter/CardSettings/Make Setting", fileName = "CardData")]
    public class CardSetting : ScriptableObject , IEquatable<CardSetting>// , ICard , ICardData
    {
        [Header("[카드 활성상태 => 무기,탄약,캐릭터는 활성상태에 따라 활서]")]
        [SerializeField]
        //[HideInInspector]
        private bool isActivate = false;
        public bool IsActivate { get => isActivate; set => isActivate = value; }

        [Header("[Gem 구매시 가격]")]
        [SerializeField]
        private int gemPrice = -1;
        public int GemPrice { get => gemPrice; set => gemPrice = value; }

        /// <summary>
        /// 업글레이드 성공 or 실패 리턴용 데이터
        /// </summary>
        public class UpgradeData
        {
            //업그레이드 성공한 레벨
            public int SuccessLevel = -1;
            //업그레이드 성공 여부
            public bool IsSuccess = false;

            public UpgradeData(int successLevel,bool isSuccess = false)
            {
                this.SuccessLevel = successLevel;
                this.IsSuccess = isSuccess;
            }
        }

        //========================= [Moved CardData] =========================

        /// <summary>
        /// 어떤 방식으로 업그레이드 호출됬는지 확인용
        /// </summary>
        [System.Serializable]
        public enum CallToUpgrade
        {
            Coin,   //돈으로 업글
            Gem,    //보석으로 업글
            Gamble, //도박으로 업글
            None,   //아무것도 아님
        }


        [System.Serializable]
        public class CardData
        {
            //캐릭터가 사용 가능 한지 아닌지
            //* 구매 또는 레벨 달성시 언락됨.
            public bool UseAble = false;

            //카드의 레벨
            public int Level = 0;

            //카드 내구도 [현재 사용 안함]
            public int Durability = 100;

            //<===== 이하 추가 한 데이터

            //UI에서 데이터의 순서
            public int SortIndex;

            //연구 중이었다면 UI데이터의 순서
            public int ResearchSlotIndex = -1;

            //해당 카드를 착용한 채로 플레이한 시간
            public int PlayTime = 0;
            
            //해당 카드를 착용한 채로 사냥한 몬스터
            public int HuntedMonster = 0;

            //어떤 방식으로 업그레이드 호출됬는지 확인용
            public CallToUpgrade WhoCallToUpgrade = CallToUpgrade.None;

            //남은 업글 시간
            //public long UpgradeStartedTime = 0;
            //업글중 이었는지 아닌지
            //public bool IsUpgrading = false;
            //남은 업글 시간 및 업글 중인지 아닌지 까지 모두 알수 있음
            public Timeset UpgardeTimeset;

            //현재 업글 상태
            public bool IsUpgrading = false;
            //업글 완료 통지 완료 상태 (초기에는 모든 게 완료된 상태)
            public bool ShownUpgradeComplate = true;
            //코인 업그레이드 전용으로 완료 체크
            //public bool DoneCoinUpgrade = false;

            public CardData(bool useAble = false) { UseAble = useAble; }


            public void Print()
            {
                Debug.Log("사용 가능 여부 = [" + UseAble.ToString() + "] 레벨 = ["
                    + Level+"] 내구도 = [" + Durability+"] 정렬 순서 = ");
            }
        }

        /// <summary>
        /// Test용 코드로 데이터 초기화
        /// </summary>
        public void Reset()
        {
            //언락 레벨에 의해 사용 가능 해제
            if(this.unlockLevel <= this.level)
            {
                this.UseAble = cardData.UseAble = true;
            }

            this.level = cardData.Level = 0;
            this.durability = cardData.Durability = 100;
            this.sortIndex = cardData.SortIndex = 0;
            this.researchSlotIndex = cardData.ResearchSlotIndex = -1;
            this.upgradeTimeset = cardData.UpgardeTimeset = null;
            this.whoCallToUpgrade = cardData.WhoCallToUpgrade = CallToUpgrade.None;
            this.isUpgrading = cardData.IsUpgrading = false;
            this.shownUpgradeComplate = cardData.ShownUpgradeComplate = true;
            //this.doneCoinUpgrade = cardData.DoneCoinUpgrade = false;

            this.playTime = cardData.PlayTime = 0;
            this.huntedMonster = cardData.HuntedMonster = 0;

            Save();
        }

        /// <summary>
        /// 데이터 싱크 마추기
        /// </summary>
        public void Synchronization()
        {
            //이떼 불필요한 Save발생을 피하기 위해 로컬 데이터 사용
            this.useAble = cardData.UseAble;

            this.level = cardData.Level;
            this.durability = cardData.Durability;
            this.sortIndex = cardData.SortIndex;
            this.researchSlotIndex = cardData.ResearchSlotIndex;
            this.upgradeTimeset = cardData.UpgardeTimeset;
            this.whoCallToUpgrade = cardData.WhoCallToUpgrade;
            this.isUpgrading = cardData.IsUpgrading;
            this.shownUpgradeComplate = cardData.ShownUpgradeComplate;
            //this.doneCoinUpgrade = cardData.DoneCoinUpgrade;

            this.playTime = cardData.PlayTime;
            this.huntedMonster = cardData.HuntedMonster;
        }

        [Header("[데이터 확인용으로 열어놓지만 나중에 막아야함]")]
        public CardData cardData = new CardData();

        //========================= [CardData] =========================

        [Header("[사용 가능 => 레벨 언락 또는 구매 언락이 됬음]")]
        //저장 필요
        [SerializeField]
        private bool useAble = false;
        public bool UseAble { get => useAble; set {
                cardData.UseAble = useAble = value;

                Debug.Log("===========> 사용 가능 언락 및 저장");
                Save();
            }
        }

        [Header("[스킬 레벨]")]
        //[HideInInspector]
        [SerializeField]
        //저장 필요
        private int level = 0;
        public int Level { get => level; set {
                cardData.Level = level = value;
                Save();
            }
        }

        [Header("[현재 내구도]")]
        [SerializeField]
        //저장 필요
        private int durability = 10;
        public int Durability { get => durability; set {
                cardData.Durability = durability = value;
                Save();
            }
        }

        //UI에서 데이터의 순서
        private int sortIndex;
        public int SortIndex { get => sortIndex; set {
                cardData.SortIndex = sortIndex = value;
                Save();
            }
        }

        //연구 중이었다면 UI데이터의 순서
        private int researchSlotIndex = -1;
        public int ResearchSlotIndex { get => researchSlotIndex; set {
                cardData.ResearchSlotIndex = researchSlotIndex = value;
                Save();
            }
        }

        
        private int playTime = 0;
        public int PlayTime
        {
            get => playTime; set
            {
                cardData.PlayTime = playTime = value;
                Save();
            }
        }

        private int huntedMonster = 0;
        public int HuntedMonster
        {
            get => huntedMonster; set
            {
                cardData.HuntedMonster = huntedMonster = value;
                Save();
            }
        }

        private CallToUpgrade whoCallToUpgrade = CallToUpgrade.None;
        public CallToUpgrade WhoCallToUpgrade
        {
            get => whoCallToUpgrade; set
            {
                cardData.WhoCallToUpgrade = whoCallToUpgrade = value;
                Save();
            }
        }

        //업글중 이었는지 아닌지
        private bool isUpgrading = false;
        public bool IsUpgrading { get => isUpgrading; set {
                cardData.IsUpgrading = isUpgrading = value;
                Debug.Log("===========> 업글 중인지 아닌지 저장 = [" + isUpgrading+"]");
                Save();
            }
        }

        //업글확인 창 보여줫는지
        private bool shownUpgradeComplate = false;
        public bool ShownUpgradeComplate
        {
            get => shownUpgradeComplate; set
            {
                cardData.ShownUpgradeComplate = shownUpgradeComplate = value;
                Debug.Log("===========> 업글 중인지 아닌지 저장 = [" + shownUpgradeComplate + "]");
                Save();
            }
        }

        //업글확인 창 보여줫는지
        //private bool doneCoinUpgrade = false;
        //public bool DoneCoinUpgrade
        //{
        //    get => doneCoinUpgrade; set
        //    {
        //        cardData.DoneCoinUpgrade = doneCoinUpgrade = value;
        //        Debug.Log("===========> 업글 중인지 아닌지 저장 = [" + doneCoinUpgrade + "]");
        //        Save();
        //    }
        //}

        [Header("[업글 시간을 가지고 있는 데이터]")]
        [SerializeField]
        private Timeset upgradeTimeset;
        public Timeset UpgradeTimeset { get => upgradeTimeset; set {
                cardData.UpgardeTimeset = upgradeTimeset = value;
                Debug.Log("===========> 업그레이드 시간 저장 = " + upgradeTimeset.GetRemainTimeToString()+"]");
                Save();
            }
        }

        //==============================================================

        [Header("[해당 카드 ID]")]
        [SerializeField] private CardType type;
        public CardType Type { get => type; }

        [Header("[카드이름]")]
        [SerializeField]
        //저장 필요??
        private new string name = "";
        public string Name { get => name; }

        [Header("[기본 값이며 무기,탄약의 경우 기본 데미지]")]
        [SerializeField]
        //저장 필요
        private int value = -1;
        public int Value { get => value; }

        [Header("[최대 중첩 => 0부터 계산]")]
        [SerializeField]
        //저장 불필요
        private int maxStack = 2;
        public int MaxStack { get => maxStack; }

        [Header("[중첩 카운트]")]
        //[HideInInspector]
        [SerializeField]
        //저장 불필요 하지만 런타임중 필요
        private int stackCount = 0;
        public int StackCount { get => stackCount; set => stackCount = value; }

        [Header("[최대 내구도]")]
        [SerializeField]
        //저장 불필요
        private int maxDurability = 100;
        public int MaxDurability { get => maxDurability; }

        [Header("[최대 레벨]")]
        [SerializeField] private int maxLevel = 25;
        public int MaxLevel { get => maxLevel; }
        
        [Header("[언락에 필요한 Exp 레벨]")]
        [SerializeField] private int unlockLevel = 0;
        public int UnlockLevel { get => unlockLevel; }

        [Header("[레벨업시 필요한 시간 공식 메서드 이름]")]
        [SerializeField]
        private string researchTimeFormula = "ResearchTime";
        public string ResearchTimeFormula { get => researchTimeFormula; }

        [Header("[레벨업시 필요한 시간 공식 메서드 이름]")]
        [SerializeField]
        private string coinFormula = "Coin";
        public string CoinFormula { get => coinFormula; }

        [Header("[Icon Sprite]")]
        [SerializeField] private Sprite sprite;
        public Sprite Sprite { get => sprite; }

        [Header("[Icon Scale]")]
        [SerializeField] private float scale = 1f;
        public float Scale { get => scale; }

        [Header("[Icon Color]")]
        [SerializeField] private Color iconColor = Color.white;
        public Color IconColor { get => iconColor; }

        [Header("[표시 이름]")]
        [SerializeField] private string title;
        public string Title { get => title; }

        [Header("[프로퍼티 값 리스트[순서대로 넣음]]")]
        [SerializeField]
        public List<CardProperty> Properties = new List<CardProperty>();

        [SerializeField]
        public enum CardType   //PropertyType와 같게 마춰야 한다
        {
            AttackSpeed = 0,    //공격 속도 25% 증가
            MagazineCapacity,   //탄창 용량 25% 증가
            CriticalDamage,     //치명 데미지 25% 증가
            CriticalChance,     //치명 기회 10% 증가
            MaxHP,              //최대 체력 25% 증가
            AirStrikeDamage,    //공습 데미지 25% 증가
            AngerRecharge,     //공습 체움 속도 25% 증가

            Coin,               //획득 돈 25% 증가
            Exp,                //획득 경험치 25% 증가

            //*연구에 의한 보스 몬스터의 속성 확인 및 데미지 침투 연구

            //Weapon,
            //Ammo,
            //Character,

            //추가 enum타입으로 이동
            
            //*아래 4개는 하나의 그룹 토글 => *[Property 와 일치 해야함]
            Pistol = 100,       //무기를 권총으로 변경 및 해당 무기 데미지 25%증가
            Shotgun,            //무기를 샷건으로 변경 및 해당 무기 데미지 25%증가   lv 4 unlock
            AssaultRifle,       //무기를 돌격소총으로 변경 및 무기 데미지 25%증가    lv 8 unlock  
            SniperRifle,        //무기를 스나이퍼소총으로 변경 및 무기 데미지 25%증가 lv 12 unlock

            //아래 3개는 하나의 구룹 토글 => *[Property 와 일치 해야함]
            BulletAmmo = 200,         //총알타입탄약 변경 및 총알타입탄약 데미지 25% 증가
            LaserAmmo,          //레이저타입탄약 변경 및 레이저타입탄약 데미지 25% 증가        lv 10 unlock
            PlasmaAmmo,         //플라즈마타입탄약 변경 및 플라즈마타입탄약 데미지 25% 증가       lv 14 unlock

            //캐릭터들은 토글 그룹 [장점 단점이 동시 존재]
            BusinessMan = 300,        //캐릭터 변경 및 권총 데미지 10% 증가 & 레이저타입탄약 데미지 -10% 감소                      
            FireFighter,        //캐릭터 변경 및 샷건 데미지 10% 증가 & 탄챵 용량 -10% 감소                                 lv 2 unlock
            Hobo,               //캐릭터 변경 및 돌격소총 데미지 10% 증가 & 공격 속도 -10% 감소                             lv 3 unlock
            Pimp,               //캐릭터 변경 및 치명 데미지 10% 증가 & 치명타 기회 -10% 감소                               lv 5 unlock
            Policeman,          //캐릭터 변경 및 총알타입탄약 데미지 10% 증가 & 돌격소총 데미지 -10%감소 & & 스나이퍼 라이플 데미지 -10%감소 lv 7 unlock
            Prostitute,         //캐릭터 변경 및 치명타 데미지 10% 증가 & 총알타입탄약 데미지 -10%감소                       lv 9 unlock
            Punk,               //캐릭터 변경 및 레이저타입 탄약 데미지 10% 증가 & 모든타입탄약 데미지 -10%감소                 lv 12 unlock
            RiotCop,            //캐릭터 변경 및 돌격소총 데미지 10% 증가 & 공습 데미지 -10% 감소                          lv 15 unlock
            Roadworker,         //캐릭터 변경 및 네임드 좀비 데미지 10% 증가 & 일반 좀비 데미지 -20% 감소                     lv 18 unlock
            Robber,             //캐릭터 변경 및 스나이퍼 소총 데미지 10% 증가 & 레이저타입탄약 데미지 -10% 감소                    lv 21 unlock
            Sheriff,            //캐릭터 변경 및 플라즈마타입탄약 데미지 10% 증가 & 탄약 용량 -10% 감소                       lv 24 unlock
            StreetMan,          //캐릭터 변경 및 모든 무기 데미지 5% 증가 & 최대 체력 -10%감소                            lv 27 unlock
            Trucker,            //캐릭터 변경 및 공습 데미지 25% 증가 & 공습 체움 속도 25% 증가 & 모든 무기 데미지 -10% 감소     lv 30 unlock
            Woman,              //캐릭터 변경 및 최대 체력 25% 증가 & 모든 타입의 탄약 데미지 10 감소                      lv 34 unlock
        }

        /// <summary>
        /// Json데이터 저장
        /// </summary>
        void Save()
        {
            string strJson = JsonUtility.ToJson(this.cardData);
            PlayerPrefs.SetString("Card_" + Name, strJson);
        }

        /// <summary>
        /// Json데이터 로드
        /// </summary>
        public void Load()
        {
            if(!PlayerPrefs.HasKey("Card_" + Name)) { Save(); }

            this.cardData = JsonUtility.FromJson<CardData>(PlayerPrefs.GetString("Card_" + Name));
            //로드한 데이터와 현재 데이터 싱크 마추기
            Synchronization();
        }

        /// <summary>
        /// 업그레이드 완료된 카드 확인.
        /// </summary>
        public void CheckTheCardToUpgradeComplated()
        {
            //만약 업그레이드 중이었다면
            if(this.IsUpgrading)
            {
                //현재 시간이 업그레이드 끝난는지 아닌지 확인
                if(this.upgradeTimeset.IsUpgrading())
                {
                    //카드 완료 큐에 넣기
                    UIGlobalMesssageQueueVewModel.UpgradeComplateEvent.Invoke(this);
                }
            }
        }

        /// <summary>
        /// 프로퍼티의 모든 디스크립션 합쳐서 가져오기
        /// </summary>
        /// <returns></returns>
        public string AllDescription()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < this.Properties.Count; i++)
            {
                stringBuilder.Append(this.Properties[i].GetCompletedDescripsion(this.Level));
                //마지막 줄이 아니면 라인 개행 추가
                if (i < this.Properties.Count) { stringBuilder.AppendLine(); }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 업그레이드 정보용 프로퍼티의 디스크립션 합쳐서 가져오기
        /// </summary>
        /// <returns></returns>
        public string AllDescriptionForUpgradeInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < this.Properties.Count; i++)
            {
                stringBuilder.Append(this.Properties[i].GetCompletedDescripsionForUpgradeInfo(this.Level));
                //마지막 줄이 아니면 라인 개행 추가
                if (i < this.Properties.Count) { stringBuilder.AppendLine(); }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 라운드 끝 팝업 카드 선택 정보용 프로퍼티의 디스크립션 합쳐서 가져오기
        /// </summary>
        /// <returns></returns>
        public string AllDescriptionForSelectedCardInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < this.Properties.Count; i++)
            {
                stringBuilder.Append(this.Properties[i].GetSelectedCardDescripsion(this.Level,this.StackCount));
                //마지막 줄이 아니면 라인 개행 추가
                if (i < this.Properties.Count) { stringBuilder.AppendLine(); }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 업그레이드 완료시 예측 레벨을 스트링으로 가져옴
        /// </summary>
        /// <param name="cardSetting">해당 카드 세팅</param>
        /// <returns></returns>
        static public string PredictUpgradeComplateLevelToString(CardSetting cardSetting)
        {
            //업그레이드 완료 레벨 -> 최고 레벨일때는 MAX 표시
            int iUpgradeComplateLevel = cardSetting.Level + 1;//기본 연구는 +1 업글
            string upgradeComplateLevel;
            if (iUpgradeComplateLevel >= cardSetting.MaxLevel) { upgradeComplateLevel = "MAX"; }
            else { upgradeComplateLevel = (iUpgradeComplateLevel + 1).ToString(); }//레벨은 표시상 +1

            return upgradeComplateLevel;
        }

        /// <summary>
        /// 도박 성공시 예측된 레벨을 스트링으로 가져옴
        /// </summary>
        /// <param name="cardSetting"></param>
        /// <returns></returns>
        static public string PredictGambleSuccessLevelToString(CardSetting cardSetting)
        {
            //도박 성공 목표 레벨
            int iGambleSuccessLevel = cardSetting.Level + 1;//도박은 +1 업글
            string gambleSuccessLevel;
            if (iGambleSuccessLevel >= cardSetting.MaxLevel) { gambleSuccessLevel = "MAX"; }
            else { gambleSuccessLevel = (iGambleSuccessLevel + 1).ToString(); }//레벨은 표시상 +1

            return gambleSuccessLevel;
        }

        /// <summary>
        /// 카드 업그레이드 취소
        /// </summary>
        /// <param name="cardSetting"></param>
        static public void CancelToUpgrade(CardSetting cardSetting)
        {
            //시간 업데이트
            //->UpgradeTimeset.bUpgrading = false
            cardSetting.UpgradeTimeset.Reset();
            cardSetting.cardData.UpgardeTimeset.Reset();
            //현재 업글 중으로 변경
            cardSetting.IsUpgrading = false;
            //보여주기 보여줌으로 초기화
            cardSetting.ShownUpgradeComplate = true;

            //UpgradeTimeset.bUpgrading = false
            //IsUpgrading = false
            //ShownUpgrade = true;
        }

        /// <summary>
        /// 실제 업그레이드 결과를 만듬
        /// 카드 업글 최종 허가 팝업에서 호출.
        /// *여기에서 실제 CardSetting데이터 변경 작업 다 함
        /// *리턴된 데이터는 단순히 확인용임.
        /// </summary>
        /// <param name="cardSetting"></param>
        /// <param name="successRate"></param>
        /// <returns></returns>
        static public UpgradeData GetUpgradeResult(CardSetting cardSetting)
        {
            UpgradeData upgradeData = null;

            int iUpgradeComplateLevel;

            switch (cardSetting.WhoCallToUpgrade)
            {
                case CardSetting.CallToUpgrade.Coin://업글세팅만 함

                    //업그레이드 완료 레벨 -> 최고 레벨일때는 증가 안함
                    //실제 카드 레벨 증가 시킴
                    iUpgradeComplateLevel = cardSetting.Level + 1;

                    //어짜피 최고 레벨에서는 업글 안되게 막을거라 필없는 코드임
                    if (iUpgradeComplateLevel >= cardSetting.MaxLevel)
                    {
                        cardSetting.Level = cardSetting.MaxLevel;
                    }
                    else
                    {
                        cardSetting.Level = cardSetting.Level + 1;
                    }

                    //무조건 성공
                    upgradeData = new UpgradeData(cardSetting.Level, true);

                    break;
                case CardSetting.CallToUpgrade.Gem://실제 결과 도출

                    //업그레이드 완료 레벨 -> 최고 레벨일때는 증가 안함
                    //실제 카드 레벨 증가 시킴
                    iUpgradeComplateLevel = cardSetting.Level + 1;

                    //어짜피 최고 레벨에서는 업글 안되게 막을거라 필없는 코드임
                    if (iUpgradeComplateLevel >= cardSetting.MaxLevel)
                    {
                        cardSetting.Level = cardSetting.MaxLevel;
                    }
                    else
                    {
                        cardSetting.Level = cardSetting.Level + 1;
                    }

                    //무조건 성공
                    upgradeData = new UpgradeData(cardSetting.Level, true);

                    break;
                case CardSetting.CallToUpgrade.Gamble://도박으로 업글. 작은 젬과 운 소비

                    //기본 실패 상태
                    bool isSuccess = false;

                    //겜블 제외 그외 모든 업글은 성공률 100
                    int successRate = 0;

                    //겜블에 의한 업그레이드 일때만 사용
                    if (cardSetting.WhoCallToUpgrade == CallToUpgrade.Gamble)
                    {
                        //기본 갬블 40 + (콤보 카운트 * 콤보 값 )
                        //successRate = GlobalDataController.GambleDefaultRate
                        //    + (GlobalDataController.Instance.GambleComboCount * GlobalDataController.GamblePerValue);

                        //*연속 성공 모두 계산된 값임
                        successRate = GlobalDataController.Instance.GambleCurrentSuccessRate;

                        Debug.Log("기본 성공률 = [" + GlobalDataController.GambleDefaultRate +
                            "] 연속 카운트 = [" + GlobalDataController.Instance.GambleComboCount +
                            "] 겜블 성공1당 증가 값 = ["+ GlobalDataController.GamblePerValue+
                            "] 최종 값 = ["+ successRate+"]");

                        //혹시라도 최대 성공 값보다 크다면 최대 값으로 초기화
                        //if (successRate > GlobalDataController.GambleMaxRate) { successRate = GlobalDataController.GambleMaxRate; }
                    }

                    //0-99까지 랜덤 int 값 생성
                    int rand = UnityEngine.Random.Range(0, 100);

                    Debug.Log("랜덤값 = [" + rand + "] 계산된 성공 값 = [" + successRate + "]");

                    //랜덤 값이 성공최대값에 포함됬다면 성공. 아니라면 실패
                    if (rand < successRate) {
                        isSuccess = true;
                    }

                    //업그레이드 완료 레벨 -> 최고 레벨일때는 증가 안함
                    //실제 카드 레벨 증가 시킴
                    iUpgradeComplateLevel = cardSetting.Level + 1;

                    if(isSuccess)//도박 성공시
                    {
                        //연속 성공 수치 상승
                        GlobalDataController.Instance.GambleComboCount++;
                        if (iUpgradeComplateLevel >= cardSetting.MaxLevel)
                        {
                            cardSetting.Level = cardSetting.MaxLevel;
                        }
                        else
                        {
                            cardSetting.Level = cardSetting.Level + 1;
                        }
                    } else//도박 실패시
                    {
                        cardSetting.Level = 0;
                        isSuccess = false;
                        //연속 성공 카운트 0으로 변경
                        GlobalDataController.Instance.GambleComboCount = 0;
                    }

                    //업그레이드의 성공 또는 실패 결과를 세팅
                    upgradeData = new UpgradeData(cardSetting.Level, isSuccess);

                    break;
                default:
                    break;
            }
            

            return upgradeData;
        }

        /// <summary>
        /// 요구하는 코인을 스트링으로 가져옴
        /// </summary>
        /// <param name="cardSetting"></param>
        /// <returns></returns>
        static public string RequireCoinToString(CardSetting cardSetting)
        {
            //요구 코인 알아오기
            int coin = NextValueCalculator.GetRequireCoinByLevel(cardSetting.MaxLevel, cardSetting.Level);

            string strCoin;
            if (coin < 10) { strCoin = coin.ToString(); }
            else { strCoin = string.Format("{0:0,0}", coin); }

            return strCoin;
        }

        /// <summary>
        /// 요구하는 코인을 가져옴
        /// </summary>
        /// <param name="cardSetting"></param>
        /// <returns></returns>
        static public int RequireCoin(CardSetting cardSetting)
        {
            //요구 코인 알아오기
            int coin = NextValueCalculator.GetRequireCoinByLevel(cardSetting.MaxLevel, cardSetting.Level);

            return coin;
        }

        /// <summary>
        /// 요구하는 젬을 스트링으로 가져옴
        /// </summary>
        /// <param name="cardSetting"></param>
        /// <returns></returns>
        static public string RequireGemToString(CardSetting cardSetting)
        {
            //요구 젬 알아오기
            int gem = NextValueCalculator.GetRequireGemByLevel(cardSetting.MaxLevel, cardSetting.Level);

            string strGem;
            if (gem < 10) { strGem = gem.ToString(); }
            else { strGem = string.Format("{0:0,0}", gem); }

            return strGem;
        }

        /// <summary>
        /// 요구하는 젬을 가져옴
        /// </summary>
        /// <param name="cardSetting"></param>
        /// <returns></returns>
        static public int RequireGem(CardSetting cardSetting)
        {
            //요구 젬 알아오기
            int gem = NextValueCalculator.GetRequireGemByLevel(cardSetting.MaxLevel, cardSetting.Level);

            return gem;
        }

        /// <summary>
        /// 업그레이드 시간을 가져옴
        /// 1. 업글 중이라면 남은시간 가져옴
        /// 2. 업글 중이 아니라면 다음 업글 시간을 가져옴
        /// </summary>
        /// <param name="cardSetting"></param>
        /// <returns></returns>
        static public string UpgradeRemainTimeToString(CardSetting cardSetting)
        {
            //코인 사용 남은시간
            string upgradeRemainTime = null;

            //현재 업그레이드 중이라면
            if (cardSetting.IsUpgrading)
            {
                //업그레이드 시간 가져오김
                upgradeRemainTime = cardSetting.UpgradeTimeset.GetRemainTimeToString();
            }
            else
            {//업글중이 아니라면 다음 업글 예상 시간 가져오기

                int seconds = NextValueCalculator.GetUpgradeTimeByLevel(cardSetting.MaxLevel, cardSetting.Level);
                //Debug.Log("!!!!!! seconds = " + seconds);
                upgradeRemainTime = Timeset.SecondsToTimeToString(seconds);
            }

            return upgradeRemainTime;
        }

        public bool Equals(CardSetting other)
        {
            if (type == other.type) return true;
            else { return false; }
        }
    }
}
