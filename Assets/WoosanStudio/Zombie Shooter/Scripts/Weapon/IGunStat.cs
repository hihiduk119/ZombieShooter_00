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
        int MaxAmmo { get; set; }

        /// <summary>
        /// 현재 잔탄수
        /// </summary>
        int CurrentAmmo { get; set; }

        /// <summary>
        /// 재장전 시간
        /// </summary>
        float ReloadTime { get; set; }
        /// <summary>
        /// 재장전 시간 계산기
        /// </summary>
        //ReloadTimeCalculator ReloadTimeCalculator { get; }

        /// <summary>
        /// 총 발사 속도
        /// *이거 어디서 사용함??
        /// </summary>
        float FireSpeed { get; }
        /// <summary>
        /// 총 발사 속도 계산기
        /// </summary>
        FireSpeedCalculator FireSpeedCalculator { get; }
    }
}

