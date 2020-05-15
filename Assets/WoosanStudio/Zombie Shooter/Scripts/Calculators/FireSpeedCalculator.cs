using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 총 발사 속도를 무기 타입과 레벨에 맞게 계산
    /// </summary>
    public class FireSpeedCalculator
    {
        static public float GetFireSpeed(int weaponType, int weaponLevel)
        {
            return -1f;
        }

        static public float GetFireSpeed(IGunStat gunStat)
        {
            return -1f;
        }
    }
}
