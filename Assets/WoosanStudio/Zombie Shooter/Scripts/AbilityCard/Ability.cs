using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{

    /// <summary>
    /// 모든 종류의 카드들의 기본 틀
    /// 모든 무기, 공속, 체력 버프, 등을 가진다.
    /// </summary>
    public interface IAbilityCard
    {
        /// <summary>
        /// 고유 아이디
        /// </summary>
        int ID { get; set; }

        /// <summary>
        /// 스킬 레벨
        /// </summary>
        int SkillLevel { get; set; }

        /// <summary>
        /// 해당 스테이지 에서 중첩된 카드 횟수
        /// </summary>
        int OverlapCount { get; set; }

        /// <summary>
        /// 해당 스테이지 에서 중첩된 제한
        /// </summary>
        int OverlapLimit { get; set; }
    }

    /// <summary>
    /// 공격 속도 증갗
    /// </summary>
    public interface IAttackSpeed
    {

    }

    /// <summary>
    /// 재장전 속도 증가
    /// </summary>
    public interface IReloadSpeed
    {

    }

    /// <summary>
    /// 기본 데미지 증가
    /// </summary>
    public interface IUpBaseDamage
    {

    }

    /// <summary>
    /// 치명타 데미지 증가
    /// </summary>
    public interface IUpCriticalDamage
    {

    }

    /// <summary>
    /// 치명타 기회 증가
    /// </summary>
    public interface IUpCriticalChance
    {

    }

    /// <summary>
    /// 베리어 최대 체력 증가
    /// </summary>
    public interface IUpMaxBarrierHP
    {

    }

    /// <summary>
    /// 베리어 체력 회복
    /// </summary>
    public interface IRecoverBarrierHP
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public interface IAntiDamageShield
    {

    }

    /// <summary>
    /// 느려짐 시간 동안 기본 데미지 및 크리티컬 데미지 증가
    /// </summary>
    public interface IUpDamageOnShooterTime
    {

    }

    /// <summary>
    /// 공중 폭격 데미지 증가
    /// </summary>
    public interface IUpDamageForAirStrike
    {

    }

}
