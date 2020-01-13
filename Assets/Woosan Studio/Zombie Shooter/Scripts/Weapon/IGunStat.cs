using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public interface IGunStat : IWeaponStat
    {
        /// <summary>
        /// 최대 탄약
        /// </summary>
        int MaxAmmo { get; }

        /// <summary>
        /// 현재 잔탄수
        /// </summary>
        int CurrentAmmo { get; set; }

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

