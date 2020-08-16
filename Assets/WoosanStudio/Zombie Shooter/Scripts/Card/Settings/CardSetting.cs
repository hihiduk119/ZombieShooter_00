using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드의 정보와 고유 아이디 및 성능
    /// </summary>
    [CreateAssetMenu(menuName = "ZombieShooter/CardSettings/Make Setting", fileName = "CardData")]
    public class CardSetting : ScriptableObject
    {
        [Header("[고유 아이디]")]
        [SerializeField] private int id;
        public int ID { get => id; }

        [Header("[UI Sprite]")]
        [SerializeField] private Sprite sprite;
        public Sprite Sprite { get => sprite; }

        [Header("[표시 이름]")]
        [SerializeField] private string title;
        public string Title { get => title; }

        [Header("[표시 설명]")]
        [SerializeField] private string contents;
        public string Contents { get => contents; }

        [Header("[해당 카드 Enum]")]
        [SerializeField] private CardType type;
        public CardType Type { get => type; }

        [SerializeField]
        public enum CardType
        {
            AttackSpeed = 0,    //공격 속도
            MagazineCapacity,   //탄창 용량 증가
            BaseDamage,             //기본 데미지 25% 증가
            CriticalDamage,     //치명 데미지
            CriticalChance,     //치명 기회
            MaxHP,              //최대 체력
            RecoverHP,          //체력 회복
            AirStrikeDamage,    //공습 데미지
            AirStrikeGauge,     //공습 체움 속도

                                //*아래 4개는 하나의 그룹 토글
            Pistol,             //권총으로 변경 및 해당 속성 데미지 25%증가
            Shotgun,            //샷건으로 변경 및 해당 속성 데미지 25%증가
            AssaultRifle,       //돌격소총으로 변경 및 해당 속성 데미지 25%증가
            SniperRifle,        //스나이퍼소총으로 변경 및 해당 속성 데미지 25%증가

                                //아래 3개는 하나의 구룹 토글
            BulletAmmo,        //물리탄약 으로 변경 및 해당 데미지 25% 증가
            LaserAmmo,          //레이저탄약 으로 변경 및 해당 데미지 25% 증가
            PlasmaAmmo,         //플라즈마탄약 으로 변경 및 해당 데미지 25% 증가

            Coin,               //획득 돈 증가 
        }
    }
}
