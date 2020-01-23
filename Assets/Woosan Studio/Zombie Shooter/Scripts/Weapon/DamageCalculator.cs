using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 무기 데미지를 무기 타입과 레벨에 맞게 계산
    /// </summary>
    public class DamageCalculator
    {
        static public int GetDamage(int weaponType, int weaponLevel)
        {
            return -1;
        }

        static public int GetDamage(IGunStat gunStat)
        {
            return -1;
        }
    }
}
