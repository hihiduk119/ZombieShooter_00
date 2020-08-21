using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter
{
    [CreateAssetMenu(menuName = "ZombieShooter/CardPropertySettings/Make Setting", fileName = "Property")]
    [System.Serializable]
    public class CardProperty : ScriptableObject, IProperty
    {
        [Header("[프로퍼티 타입]")]
        [SerializeField]
        private PropertyType type;
        public PropertyType Type => type;

        [Header("[기본 프로퍼티 초기 값]")]
        [SerializeField]
        private int value;
        public int Value => value;

        [Header("[레벨 1업당 증가 수치]")]
        [SerializeField]
        private int increasedValuePerLevelUp;
        public int IncreasedValuePerLevelUp => increasedValuePerLevelUp;

        [Header("[해당 속성 설명]")]
        [SerializeField][TextArea(5, 8)]
        private string descripsion;
        public string Descripsion => descripsion;

        [System.Serializable]
        public enum PropertyType
        {
            AttackSpeed,        //공격 속도
            MagazineCapacity,   //탄창 용량
            CriticalDamage,     //치명 데미지
            CriticalChance,     //치명 기회
            MaxHP,              //최대 체력
            AirStrikeDamage,    //공습 데미지
            AirStrikeRecharge,     //공습 체움 속도
            Coin,               //획득 돈
            Exp,                //획득 경험치
            ChangeCharacter,                //캐릭터 변경
            ChangeWeapon,                   //무기 변경
            ChangeAmmoType,                 //탄약 타입 변경
            NamedZombieDamage,              //네임드 좀비 데미지
            GeneralZombieDamage,            //일반 좀비 데미지
            PistolDamage,                   //권총 데미지
            ShotgunDamage,                  //샷건 데미지
            AssaultRifleDamage,             //돌격 소총 데미지
            SniperRifleDamage,              //스나이퍼소총 데미지
            AllWeaponDamage,                //모든 무기 데미지
            BulletAmmoDamage,               //총알타입탄약 데미지
            LaserAmmoDamage,                //레이저타입탄약 데미지
            PlasmaAmmoDamage,               //플라즈마타입탄약 데미지
            AllAmmoDamage,                  //모든 탄약 데미지
        }
    }
}
