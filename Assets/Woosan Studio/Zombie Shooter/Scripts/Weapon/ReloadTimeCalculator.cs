using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 재장전 시간을 무기 타입과 레벨에 맞게 계산
    /// </summary>
    public class ReloadTimeCalculator
    {
        static public float GetReloadTime(int weaponType, int weaponLevel , float defaultValue)
        {
            return 2f;
        }

        static public float GetReloadTime(IGunStat gunStat)
        {
            return 2f;
        }

    }
}
