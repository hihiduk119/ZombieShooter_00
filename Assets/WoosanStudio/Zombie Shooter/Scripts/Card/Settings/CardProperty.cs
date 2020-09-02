﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드 자체의 프로퍼티
    /// 프로퍼티의 초기값 및 레벨당 증가 수치 및 설명 등이 있음
    /// </summary>
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
            AttackSpeed,                    //공격 속도
            MagazineCapacity,               //탄창 용량
            CriticalDamage,                 //치명 데미지
            CriticalChance,                 //치명 기회
            MaxHP,                          //최대 체력
            AirStrikeDamage,                //공습 데미지
            AirStrikeRecharge,              //공습 체움 속도
            Coin,                           //획득 돈
            Exp,                            //획득 경험치
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

            //몬스터가 가지는 프로퍼티
            PistolDamageResistance,         //권총 데미지 저항
            ShotgunDamageResistance,        //샷건 데미지 저항
            AssaultRifleDamageResistance,   //돌격 소총 데미지 저항
            SniperRifleDamageResistance,    //스나이퍼소총 데미지 저항
            AllWeaponDamageResistance,      //모든 무기 데미지 저항
            BulletAmmoDamageResistance,     //총알타입탄약 데미지 저항
            LaserAmmoDamageResistance,      //레이저타입탄약 데미지 저항
            PlasmaAmmoDamageResistance,     //플라즈마타입탄약 데미지 저항
            AllAmmoDamageResistance,        //모든 탄약 데미지 저항
            CriticalDamageResistance,       //치명타 데미지 저항
            AirStrikeDamageResistance,      //공습ㄴ 데미지 저항
        }
    }
}