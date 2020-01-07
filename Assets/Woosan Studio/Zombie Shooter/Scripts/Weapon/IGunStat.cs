using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public interface IGunStat
    {
        /// <summary>
        /// 최대 탄약
        /// </summary>
        int MaxAmmo { get; }
        /// <summary>
        /// 현재 잔탄수
        /// </summary>
        int CurrentAmmo { get; }
        /// <summary>
        /// 무기 레벨
        /// </summary>
        int Level { get; }
        /// <summary>
        /// 무기 종류
        /// </summary>
        int Type { get; }

        /// <summary>
        /// 무기 데이지
        /// </summary>
        int Damage { get; }
        /// <summary>
        /// 데미지 계산기
        /// </summary>
        DamageCalculator DamageCalculator { get; }

        /// <summary>
        /// 재장전 시간
        /// </summary>
        float ReloadTime { get; }
        /// <summary>
        /// 재장전 시간 계산기
        /// </summary>
        ReloadTimeCalculator ReloadTimeCalculator { get; }

        /// <summary>
        /// 총 발사 속도
        /// </summary>
        float FireSpeed { get; }
        /// <summary>
        /// 총 발사 속도 계산기
        /// </summary>
        FireSpeedCalculator FireSpeedCalculator { get; }
    }
}

