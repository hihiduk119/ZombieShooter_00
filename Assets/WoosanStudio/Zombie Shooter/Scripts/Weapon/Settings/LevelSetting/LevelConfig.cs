using UnityEngine;
using System.Collections.Generic;

namespace WoosanStudio.ZombieShooter
{
    [CreateAssetMenu(menuName = "ZombieShooter/LevelSettings/Level Setting", fileName = "LevelData")]
    public class LevelConfig : ScriptableObject
    {
        /// <summary>
        /// 레벨 넘버링
        /// </summary>
        [SerializeField] private int _level = 0;
        public int Level { get => _level; }

        /// <summary>
        /// 출현 몬스터 프리팹
        /// </summary>
        [SerializeField] private List<GameObject> _prefabList = null;

        public GameObject GetPrefab(int index)
        {
            GameObject clone = Instantiate(_prefabList[index]) as GameObject;
            return clone;
        }

        /// <summary>
        /// 몬스터 출현 시간 및 출현 갯수
        /// </summary>
        public List<SpotConfig> spotConfigList;
    }
}