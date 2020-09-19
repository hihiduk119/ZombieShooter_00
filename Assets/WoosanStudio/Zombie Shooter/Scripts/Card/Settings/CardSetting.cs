using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드의 정보와 고유 아이디 및 성능
    /// *그대로 사용하면 안돼고 Instantiate해서 사용해야함.
    /// </summary>
    [CreateAssetMenu(menuName = "ZombieShooter/CardSettings/Make Setting", fileName = "CardData")]
    public class CardSetting : ScriptableObject , ICard , ICardData
    {
        [Header("[카드 활성상태 => 무기,탄약,캐릭터는 활성상태에 따라 활서]")]
        [SerializeField]
        [HideInInspector]
        private bool isActivate = false;
        public bool IsActivate { get => isActivate; set => isActivate = value; }

        //===============================[ICardData Implemet]===============================
        [Header("[해당 카드 ID]")]
        [SerializeField] private CardType type;
        public CardType Type { get => type; }

        [Header("[해당 카드 무기 타입 ID]")]
        [SerializeField] private CardTypeByWeapon typeByWeapon;
        public CardTypeByWeapon TypeByWeapon { get => typeByWeapon; }

        [Header("[해당 카드 탄약 타입 ID]")]
        [SerializeField] private CardTypeByAmmo typeByAmmo;
        public CardTypeByAmmo TypeByAmmo { get => typeByAmmo; }

        [Header("[해당 카드 캐릭터 타입 ID]")]
        [SerializeField] private CardTypeByCharacter typeByCharacter;
        public CardTypeByCharacter TypeByCharacter { get => typeByCharacter; }

        [Header("[현재 내구도]")]
        [SerializeField]
        //저장 필요
        private int drability = 10;
        public int Durability { get => drability; }

        [Header("[스킬 레벨]")]
        [HideInInspector]
        [SerializeField]
        //저장 필요
        private int level = 0;
        public int Level { get => level; }

        [HideInInspector]//레벨 업그레이드 연구 중이라면 남은 시간 System.Data
        //저장 불필요
        private long remainResearchTime = 0;
        public long RemainResearchTime => remainResearchTime;

        [HideInInspector]//연구 중이었다면 해당 슬롯
        //저장 필요
        private int researchSlot = -1;
        public int ResearchSlot => researchSlot;

        //===============================[ICard Implemet]===============================
        

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
        private int maxDurability = 10;
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
        //==============================================================================

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

        [SerializeField]
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
        }

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
            
            //*아래 4개는 하나의 그룹 토글
            Pistol = 100,       //무기를 권총으로 변경 및 해당 무기 데미지 25%증가
            Shotgun,            //무기를 샷건으로 변경 및 해당 무기 데미지 25%증가   lv 4 unlock
            AssaultRifle,       //무기를 돌격소총으로 변경 및 무기 데미지 25%증가    lv 8 unlock  
            SniperRifle,        //무기를 스나이퍼소총으로 변경 및 무기 데미지 25%증가 lv 12 unlock

            //아래 3개는 하나의 구룹 토글
            BulletAmmo = 200,         //총알타입탄약 변경 및 총알타입탄약 데미지 25% 증가
            LaserAmmo,          //레이저타입탄약 변경 및 레이저타입탄약 데미지 25% 증가        lv 10 unlock
            PlasmaAmmo,         //플라즈마타입탄약 변경 및 플라즈마타입탄약 데미지 25% 증가       lv 14 unlock

            //캐릭터들은 토글 그룹 [장점 단점이 동시 존재]
            BusinessMan = 300,        //캐릭터 변경 및 권총 데미지 10% 증가 & 레이저타입탄약 데미지 -10% 감소                      
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
        }
    }
}
