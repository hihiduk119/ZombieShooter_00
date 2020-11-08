using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드의 정보와 고유 아이디 및 성능
    /// *그대로 사용하면 안돼고 Instantiate해서 사용해야함.
    /// </summary>
    [CreateAssetMenu(menuName = "ZombieShooter/CardSettings/Make Setting", fileName = "CardData")]
    public class CardSetting : ScriptableObject// , ICard , ICardData
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

        //========================= [CardData] =========================

        [Header("[사용 가능 => 레벨 언락 또는 구매 언락이 됬음]")]
        //저장 필요
        [SerializeField]
        private bool useAble = false;
        public bool UseAble { get => useAble; set => useAble = value; }

        [Header("[스킬 레벨]")]
        //[HideInInspector]
        [SerializeField]
        //저장 필요
        private int level = 0;
        public int Level { get => level; set => level = value; }

        [Header("[현재 내구도]")]
        [SerializeField]
        //저장 필요
        private int durability = 10;
        public int Durability { get => durability; set => durability = value; }

        //UI에서 데이터의 순서
        public int SortIndex;

        //연구 중이었다면 UI데이터의 순서
        public int ResearchSlotIndex = -1;

        //업글중 이었는지 아닌지
        public bool IsUpgrading = false;

        //==============================================================

        [Header("[해당 카드 ID]")]
        [SerializeField] private CardType type;
        public CardType Type { get => type; }

        //[Header("[해당 카드 무기 타입 ID]")]
        //[SerializeField] private CardTypeByWeapon typeByWeapon;
        //public CardTypeByWeapon TypeByWeapon { get => typeByWeapon; }

        //[Header("[해당 카드 탄약 타입 ID]")]
        //[SerializeField] private CardTypeByAmmo typeByAmmo;
        //public CardTypeByAmmo TypeByAmmo { get => typeByAmmo; }

        //[Header("[해당 카드 캐릭터 타입 ID]")]
        //[SerializeField] private CardTypeByCharacter typeByCharacter;
        //public CardTypeByCharacter TypeByCharacter { get => typeByCharacter; }

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



        //레벨 업그레이드 연구 중이라면 남은 시간 System.Data
        //저장 불필요
        //private long upgradeStartedTime = 0;
        //public long UpgradeStartedTime => upgradeStartedTime;

        [Header("[업글 시간을 가지고 있는 데이터]")]
        [SerializeField]
        private Timeset upgradeTimeset;
        public Timeset UpgradeTimeset{get => upgradeTimeset; set => upgradeTimeset = value; }


        [HideInInspector]//연구 중이었다면 해당 슬롯
        //저장 필요
        //private int researchSlot = -1;
        //public int ResearchSlot => researchSlot;


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

        //[Header("[표시 설명]")]
        //[TextArea(5,8)]
        //[SerializeField] private string contents;
        //public string Contents { get => contents; }

        

        [Header("[프로퍼티 값 리스트[순서대로 넣음]]")]
        [SerializeField]
        public List<CardProperty> Properties = new List<CardProperty>();

        /*[SerializeField]
        public enum CardTypeByWeapon
        {
            None = 0,
            Pistol,             //무기를 권총으로 변경 및 해당 무기 데미지 25%증가
            Shotgun,            //무기를 샷건으로 변경 및 해당 무기 데미지 25%증가   lv 4 unlock
            AssaultRifle,       //무기를 돌격소총으로 변경 및 무기 데미지 25%증가    lv 8 unlock  
            SniperRifle,        //무기를 스나이퍼소총으로 변경 및 무기 데미지 25%증가 lv 12 unlock
        }

        [SerializeField]
        public enum CardTypeByAmmo
        {
            None = 0,
            BulletAmmo,         //총알타입탄약 변경 및 총알타입탄약 데미지 25% 증가
            LaserAmmo,          //레이저타입탄약 변경 및 레이저타입탄약 데미지 25% 증가        lv 10 unlock
            PlasmaAmmo,         //플라즈마타입탄약 변경 및 플라즈마타입탄약 데미지 25% 증가       lv 14 unlock
        }

        [SerializeField]
        public enum CardTypeByCharacter
        {
            None = 0,
            BusinessMan,        //캐릭터 변경 및 권총 데미지 10% 증가 & 레이저타입탄약 데미지 -10% 감소                      
            FireFighter,        //캐릭터 변경 및 샷건 데미지 10% 증가 & 탄챵 용량 -10% 감소                                 lv 2 unlock
            Hobo,               //캐릭터 변경 및 돌격소총 데미지 10% 증가 & 공격 속도 -10% 감소                             lv 3 unlock
            Pimp,               //캐릭터 변경 및 치명 데미지 10% 증가 & 치명타 기회 -10% 감소                               lv 5 unlock
            Policeman,          //캐릭터 변경 및 총알타입탄약 데미지 10% 증가 & 돌격소총 데미지 -10%감소 & & 스나이퍼 라이플 데미지 -10%감소 lv 6 unlock
            Prostitute,         //캐릭터 변경 및 치명타 데미지 10% 증가 & 총알타입탄약 데미지 -10%감소                       lv 7 unlock
            Punk,               //캐릭터 변경 및 레이저타입 탄약 데미지 10% 증가 & 모든타입탄약 데미지 -10%감소                 lv 9 unlock
            RiotCop,            //캐릭터 변경 및 돌격소총 데미지 10% 증가 & 공습 데미지 -10% 감소                          lv 11 unlock
            Roadworker,         //캐릭터 변경 및 네임드 좀비 데미지 10% 증가 & 일반 좀비 데미지 -20% 감소                     lv 12 unlock
            Robber,             //캐릭터 변경 및 스나이퍼 소총 데미지 10% 증가 & 레이저타입탄약 데미지 -10% 감소                    lv 13 unlock
            Sheriff,            //캐릭터 변경 및 플라즈마타입탄약 데미지 10% 증가 & 탄약 용량 -10% 감소                       lv 15 unlock
            StreetMan,          //캐릭터 변경 및 모든 무기 데미지 5% 증가 & 최대 체력 -10%감소                            lv 20 unlock
            Trucker,            //캐릭터 변경 및 공습 데미지 25% 증가 & 공습 체움 속도 25% 증가 & 모든 무기 데미지 -10% 감소     lv 22 unlock
            Woman,              //캐릭터 변경 및 최대 체력 25% 증가 & 모든 타입의 탄약 데미지 10 감소                      lv 25 unlock
        }*/

        [SerializeField]
        public enum CardType
        {
            AttackSpeed = 0,    //공격 속도 25% 증가
            MagazineCapacity,   //탄창 용량 25% 증가
            CriticalDamage,     //치명 데미지 25% 증가
            CriticalChance,     //치명 기회 10% 증가
            MaxHP,              //최대 체력 25% 증가
            AirStrikeDamage,    //공습 데미지 25% 증가
            AirStrikeRecharge,     //공습 체움 속도 25% 증가

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
        /// 업그레이드 완료시 레벨을 스트링으로 가져옴
        /// </summary>
        /// <param name="cardSetting">해당 카드 세팅</param>
        /// <returns></returns>
        static public string UpgradeComplateLevelToString(CardSetting cardSetting)
        {
            //업그레이드 완료 레벨 -> 최고 레벨일때는 MAX 표시
            int iUpgradeComplateLevel = cardSetting.Level + 1;//기본 연구는 +1 업글
            string upgradeComplateLevel;
            if (iUpgradeComplateLevel >= cardSetting.MaxLevel) { upgradeComplateLevel = "MAX"; }
            else { upgradeComplateLevel = (iUpgradeComplateLevel + 1).ToString(); }//레벨은 표시상 +1

            return upgradeComplateLevel;
        }

        /// <summary>
        /// 도박 성공시 레벨을 스트링으로 가져옴
        /// </summary>
        /// <param name="cardSetting"></param>
        /// <returns></returns>
        static public string GambleSuccessLevelToString(CardSetting cardSetting)
        {
            //도박 성공 목표 레벨
            int iGambleSuccessLevel = cardSetting.Level + 2;//도박은 +2 업글
            string gambleSuccessLevel;
            if (iGambleSuccessLevel >= cardSetting.MaxLevel) { gambleSuccessLevel = "MAX"; }
            else { gambleSuccessLevel = (iGambleSuccessLevel + 1).ToString(); }//레벨은 표시상 +1

            return gambleSuccessLevel;
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
            if (cardSetting.UpgradeTimeset.bUpgrading)
            {
                //업그레이드 시간 가져오김
                upgradeRemainTime = cardSetting.UpgradeTimeset.GetRemainTimeToString();
            }
            else
            {//업글중이 아니라면 다음 업글 예상 시간 가져오기

                int seconds = NextValueCalculator.GetUpgradeTimeByLevel(cardSetting.MaxLevel, cardSetting.Level);
                Debug.Log("!!!!!! seconds = " + seconds);
                upgradeRemainTime = Timeset.SecondsToTimeToString(seconds);
            }

            return upgradeRemainTime;
        }
    }
}
