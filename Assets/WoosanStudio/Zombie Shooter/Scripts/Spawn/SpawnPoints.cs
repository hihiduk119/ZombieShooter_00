using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 몬스터 생성시 모든 스폰포인터를 가지고 관리함.
    /// </summary>
    public class SpawnPoints : MonoBehaviour
    {
        [Header("[자식에서 비교해서 찾을 이름]")]
        public string CompareName = "SpawnPoint";
        [Header("[스폰포인트 리스트]")]
        public List<Transform> Points = new List<Transform>();

        private void Awake()
        {
            //FindAllPointToTheChild();
            WoosanStudio.Common.Transforms.FindAllTransformToTheChild(ref Points, transform, CompareName);
        }

        /// <summary>
        /// 랜덤으로 다음 스폰할 장소를 가져옴
        /// </summary>
        /// <returns></returns>
        public Transform GetSpawnPositionByRandom()
        {
            return Points[Random.Range(0, Points.Count)];
        }
    }
}