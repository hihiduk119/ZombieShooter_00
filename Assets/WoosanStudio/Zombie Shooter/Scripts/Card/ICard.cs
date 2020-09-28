using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{

    /// <summary>
    /// 모든 종류의 카드들의 기본 틀
    /// 모든 무기, 공속, 체력 버프, 등을 가진다.
    /// 프랍 저장시 에도 사용
    /// </summary>
    public interface ICard
    {
        /// <summary>
        /// 시작 카드의 레밸
        /// *ICardData 에서 구현
        /// </summary>
        //int Level { get; }

        /// <summary>
        /// 카드의 최대 레벨
        /// </summary>
        int MaxLevel { get; }

        /// <summary>
        /// 언락에 필요한 레
        /// </summary>
        int UnlockLevel { get; }

        /// <summary>
        /// 레벨업시 연구시 걸리는 시간 공식
        /// </summary>
        string ResearchTimeFormula { get; }

        /// <summary>
        /// 다음 업그레이드에 필요한 코인 공식
        /// </summary>
        string CoinFormula { get; }

        /// <summary>
        /// 최대 중첩 => 0부터 계산
        /// </summary>
        int MaxStack { get; }

        /// <summary>
        /// 중첩 카운트
        /// </summary>
        int StackCount { get; }

        /// <summary>
        /// 최대 내구도
        /// </summary>
        int MaxDurability { get; }

        /// <summary>
        /// 현재 내구도
        /// *ICardData 에서 구현
        /// </summary>
        //int Durability { get; }
    }

    /// <summary>
    /// 각각의 속성값
    /// </summary>
    public interface IProperty
    {
        /// <summary>
        /// 실제 값
        /// </summary>
        int Value { get; }

        /// <summary>
        /// 레벨 1업에 증가하는 Value수치
        /// </summary>
        float IncreasedValuePerLevelUp { get; }

        /// <summary>
        /// 해당 프로퍼티 설명
        /// </summary>
        string Descripsion { get; }
    }

    /// <summary>
    /// 카드 중첩
    /// </summary>
    public interface IOverlap
    {
        /// <summary>
        /// 해당 스테이지 에서 중첩된 카드 횟수ww
        /// </summary>
        int Count { get; set; }

        /// <summary>
        /// 해당 스테이지 에서 중첩된 제한
        /// </summary>
        int Limit { get; set; }
    }

    /*
    /// <summary>
    /// 공격 속도 퍼센트 증갗
    /// </summary>
    public interface IAttackSpeed : IProperty
    {

    }

    /// <summary>
    /// 탄창 용량 퍼센트 증가
    /// </summary>
    public interface IMagazineCapacity : IProperty
    {

    }

    /// <summary>
    /// 치명타 데미지 증가
    /// </summary>
    public interface ICriticalDamage : IProperty
    {

    }

    /// <summary>
    /// 치명타 기회 증가
    /// </summary>
    public interface ICriticalChance : IProperty
    {

    }

    /// <summary>
    /// 최대 체력 증가
    /// </summary>
    public interface IMaxHP : IProperty
    {

    }

    /// <summary>
    /// 공중 폭격 데미지 퍼센트 증가
    /// </summary>
    public interface IAirStrikeDamage : IProperty
    {

    }

    /// <summary>
    /// 공중 폭격 체우는 속도 퍼센트 증가
    /// </summary>
    public interface IAirStrikeRecharge : IProperty
    {

    }

    /// <summary>
    /// 권총 카드
    /// </summary>
    public interface IPistolDamage : IProperty
    {

    }

    /// <summary>
    /// 샷건 카드
    /// </summary>
    public interface IShotgunDamage : IProperty
    {

    }

    /// <summary>
    /// 돌격 소총 카드
    /// </summary>
    public interface IAssaultRifleDamage : IProperty
    {

    }

    /// <summary>
    /// 저격 소총 카드
    /// 보스에 데미지 200%
    /// </summary>
    public interface ISniperRifleDamage : IProperty
    {

    }

    /// <summary>
    /// 모든 무기 데미지
    /// </summary>
    public interface IAllWeaponDamage
    {
        IPistolDamage Pistol { get; set; }
        IShotgunDamage Shotgun { get; set; }
        IAssaultRifleDamage AssaultRifle { get; set; }
        ISniperRifleDamage SniperRifle { get; set; }
    }

    /// <summary>
    /// 물리 기반 탄약
    /// </summary>
    public interface IBulletTypeAmmoDamage : ICardProperty
    {

    }

    /// <summary>
    /// 레이지 기반 탄약
    /// </summary>
    public interface ILaserTypeAmmoDamage : ICardProperty
    {

    }

    /// <summary>
    /// 플라즈마 기반 탄약
    /// </summary>
    public interface IPlasmaTypeAmmoDamage : ICardProperty
    {

    }

    /// <summary>
    /// 모든 타입 탄약
    /// </summary>
    public interface IAllTypeAmmoDamage
    {
        IBulletTypeAmmoDamage Bullet { get; set; }
        ILaserTypeAmmoDamage Laser { get; set; }
        IPlasmaTypeAmmoDamage Plasma { get; set; }
    }

    /// <summary>
    /// 획득 코인
    /// </summary>
    public interface IGainCoin : ICardProperty
    {
        
    }

    /// <summary>
    /// 획득 경험치
    /// </summary>
    public interface IGainExp : ICardProperty
    {
        
    }

    /// <summary>
    /// 네임드 좀비 데미지
    /// </summary>
    public interface INamedZombieDamage : ICardProperty
    {

    }

    /// <summary>
    /// 일 좀비 데미지
    /// </summary>
    public interface IGeneralZombieDamage : ICardProperty
    {

    }
    */

    /// <summary>
    /// 폭발성 탄약
    /// </summary>
    //public interface IExplosiveBaseAmmo : ICard
    //{
    //}

    //public interface IAntiDamageShieldCard : ICard
    //{
    //}

    /// <summary>
    /// 느려짐 시간 동안 기본 데미지 및 크리티컬 데미지 증가
    /// </summary>
    //public interface IOnShooterTimeCard : ICard
    //{
    //}
}
