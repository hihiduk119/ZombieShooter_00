using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 모든 데미지를 무기 타입과 레벨에 맞게 계산
    /// </summary>
    public class DamageCalculator
    {
        //===================>   건 데미지 계산용    <===================
        static public int GetDamage(int weaponType, int weaponLevel)
        {
            return -1;
        }

        static public int GetDamage(IGunStat gunStat)
        {
            return -1;
        }

        //===================>   몬스터의 데미지 계산용    <===================
        static public int GetDamage(int id,int level, MonsterSettings.MonsterID monsterID, bool isMonster )
        {
            //return -1;
            return 10;
        }
    }
}
