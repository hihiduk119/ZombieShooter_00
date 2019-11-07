using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Woosan.SurvivalGame01
{
    public class EnemyData : MonoBehaviour
    {
        /// <summary>
        /// 적의 종류 => 모델을 결정
        /// </summary>
        public enum EnemyType
        {
            CitizenMale_00,
            CitizenMale_01,
            CitizenFemale,
            BeachBabe,
            BusinessMan,
            Nazi,
            Police,
            Punk,
            RiotCop,
            Sherif,
        }

        //좀비 타입
        public EnemyType type;
        //좀비 레벨 => 생성시 해당 레벨로 hp 와 dmg 가 생성
        public int level;
        public int hp;
        public int dmg;
        public bool isBoss;
    }
}
