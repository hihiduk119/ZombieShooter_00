using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드의 정보와 고유 아이디 및 성능
    /// </summary>
    [CreateAssetMenu(menuName = "ZombieShooter/CardSettings/Make Setting", fileName = "CardData")]
    public class CardSetting : ScriptableObject , ICard
    {
        //===============================[ICard Implemet]===============================
        [Header("[고유 아이디]")]
        [SerializeField] private int id;
        public int ID { get => id; }

        [Header("[스킬 레벨]")]
        [SerializeField] private int skillLevel;
        public int SkillLevel { get => skillLevel; }

        [Header("[언락 레벨]")]
        [SerializeField] private int unlockLevel;
        public int UnlockLevel { get => unlockLevel; }
        //==============================================================================

        [Header("[UI Sprite]")]
        [SerializeField] private Sprite sprite;
        public Sprite Sprite { get => sprite; }

        [Header("[표시 이름]")]
        [SerializeField] private string title;
        public string Title { get => title; }

        [Header("[표시 설명]")]
        [TextArea]
        [SerializeField] private string contents;
        public string Contents { get => contents; }

        [Header("[해당 카드 Enum]")]
        [SerializeField] private CardType type;
        public CardType Type { get => type; }

        [SerializeField]
        public enum CardType
        {
            //RecoverHP,        //체력 회복 25% 증가 => 체력회복 의문..
            AttackSpeed = 0,    //공격 속도 25% 증가
            MagazineCapacity,   //탄창 용량 25% 증가
            CriticalDamage,     //치명 데미지 25% 증가
            CriticalChance,     //치명 기회 10% 증가
            MaxHP,              //최대 체력 25% 증가
            AirStrikeDamage,    //공습 데미지 25% 증가
            AirStrikeGauge,     //공습 체움 속도 25% 증가

            Coin,               //획득 돈 25% 증가
            Exp,                //획득 경험치 25% 증가

            //*아래 4개는 하나의 그룹 토글
            Pistol,             //무기를 권총으로 변경 및 해당 무기 데미지 25%증가
            Shotgun,            //무기를 샷건으로 변경 및 해당 무기 데미지 25%증가   lv 4 unlock
            AssaultRifle,       //무기를 돌격소총으로 변경 및 무기 데미지 25%증가    lv 8 unlock  
            SniperRifle,        //무기를 스나이퍼소총으로 변경 및 무기 데미지 25%증가 lv 12 unlock

            //아래 3개는 하나의 구룹 토글
            BulletAmmo,         //총알타입탄약 변경 및 총알타입탄약 데미지 25% 증가
            LaserAmmo,          //레이저타입탄약 변경 및 레이저타입탄약 데미지 25% 증가        lv 10 unlock
            PlasmaAmmo,         //플라즈마타입탄약 변경 및 플라즈마타입탄약 데미지 25% 증가       lv 14 unlock

            //캐릭터들은 토글 그룹 [장점 단점이 동시 존재]
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

            //연구에 의한 보스 몬스터의 속성 확인 및 데미지 침투 연구
            //
        }
    }
}
