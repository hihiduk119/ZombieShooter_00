using UnityEngine;
using System.Collections.Generic;

namespace WoosanStudio.ZombieShooter
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "ZombieShooter/LevelSettings/Spot Setting", fileName = "SpotData")]
    public class SpotConfig : ScriptableObject , IHaveID
    {
        /// <summary>
        /// 몬스터의 고유 아이디
        /// </summary>
        [SerializeField] private int _id = 0;
        public int ID { get => _id; set => _id = value; }

        /// <summary>
        /// 최초 생성 딜레이 타임
        /// </summary>
        [SerializeField] private float _firstDelay = 0;
        public float FristDelay { get => _firstDelay; }

        /// <summary>
        /// 생성 딜레이 타임
        /// </summary>
        [SerializeField] private float _delay = 0;
        public float Delay { get => _delay; }

        /// <summary>
        /// 생성 갯수
        /// </summary>
        [SerializeField] private int _makeCount = 0;
        public int MakeCount { get => _makeCount; }
    }
}