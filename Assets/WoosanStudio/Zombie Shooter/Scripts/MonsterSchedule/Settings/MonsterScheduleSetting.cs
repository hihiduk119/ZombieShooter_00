using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 맵당 13개 존재하는 하나의 스테이지 스케줄
    /// 맵당 스테이지에서 몬스터 생성 스케줄을 설정함
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "ZombieShooter/MonsterSchedule/Make", fileName = "MonsterSchedule")]
    public class MonsterScheduleSetting : ScriptableObject
    {
        /// <summary>
        /// 기본 룰
        /// 1 스테이지는 15라운드로 구성.
        /// 매 5라운드 마다 보스 출현
        /// 
        /// </summary>

        [Header("[생성 몬스터 리스트]")]
        public List<GameObject> Monsters = new List<GameObject>();

        [Header("[생성 네임드 몬스터 (보스몹)]")]
        public GameObject NamedMonster;

        [Header("[스테이지 별 최대 스폰 갯수]")]
        public List<int> MaxSpawn = new List<int>();

        [Header("[동시 스폰하는 최대 갯수]")]
        public int MaxSpawnPoint = 1;

        [Header("[스폰과 스폰사이의 시간]")]
        public float SpawnInterval = 2.5f;
    }
}
