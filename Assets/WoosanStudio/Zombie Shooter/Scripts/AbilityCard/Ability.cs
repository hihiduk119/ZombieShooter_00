using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{

    /// <summary>
    /// 모든 종류의 카드들의 기본 틀
    /// 모든 무기, 공속, 체력 버프, 등을 가진다.
    /// </summary>
    public interface ICard
    {
        /// <summary>
        /// 고유 아이디 => CardType과 일치 해야 한다.
        /// </summary>
        int ID { get;}

        /// <summary>
        /// 스킬 레벨
        /// </summary>
        int SkillLevel { get;}


        /// <summary>
        /// 언락 레벨
        /// </summary>
        int UnlockLevel { get;}
    }

    public interface ICardOverlap
    {
        /// <summary>
        /// 해당 스테이지 에서 중첩된 카드 횟수
        /// </summary>
        int Count { get; set; }

        /// <summary>
        /// 해당 스테이지 에서 중첩된 제한
        /// </summary>
        int Limit { get; set; }
    }

    /// <summary>
    /// 공격 속도 퍼센트 증갗
    /// </summary>
    public interface IAttackSpeedCard : ICard
    {

    }

    /// <summary>
    /// 탄창 용량 퍼센트 증가
    /// </summary>
    public interface IMagazineCapacityCard : ICard
    {

    }

    /// <summary>
    /// 기본 데미지 퍼센트 증가 
    /// </summary>
    public interface IDamageCard : ICard
    {

    }

    /// <summary>
    /// 치명타 데미지 증가
    /// </summary>
    public interface ICriticalDamageCard : ICard
    {

    }

    /// <summary>
    /// 치명타 기회 증가
    /// </summary>
    public interface ICriticalChanceCard : ICard
    {

    }

    /// <summary>
    /// 최대 체력 증가
    /// </summary>
    public interface IMaxHPCard : ICard
    {

    }

    /// <summary>
    /// 베리어 체력 회복
    /// </summary>
    public interface IRecoveHPCard : ICard
    {

    }

    /// <summary>
    /// 공중 폭격 데미지 퍼센트 증가
    /// </summary>
    public interface IAirStrikeDamageCard : ICard
    {

    }

    /// <summary>
    /// 공중 폭격 체우는 속도 퍼센트 증가
    /// </summary>
    public interface IAirStrikeGaugeCard : ICard
    {

    }

    /// <summary>
    /// 권총 카드
    /// </summary>
    public interface IPistolCard : ICard
    {

    }

    /// <summary>
    /// 샷건 카드
    /// </summary>
    public interface IShotgunCard : ICard
    {

    }

    /// <summary>
    /// 돌격 소총 카드
    /// </summary>
    public interface IAssaultRifleCard : ICard
    {

    }

    /// <summary>
    /// 저격 소총 카드
    /// 보스에 데미지 200%
    /// </summary>
    public interface ISniperRifleCard : ICard
    {

    }

    /// <summary>
    /// 물리 기반 탄약
    /// </summary>
    public interface IBulletAmmoCard : ICard
    {

    }

    /// <summary>
    /// 레이지 기반 탄약
    /// </summary>
    public interface ILaserAmmoCard : ICard
    {

    }

    /// <summary>
    /// 플라즈마 기반 탄약
    /// </summary>
    public interface IPlasmaAmmoCard : ICard
    {

    }

    /// <summary>
    /// 획득 코인 증가
    /// </summary>
    public interface ICoinCard : ICard
    {

    }

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
