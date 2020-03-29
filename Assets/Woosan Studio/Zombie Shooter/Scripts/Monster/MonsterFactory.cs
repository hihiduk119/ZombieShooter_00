using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WoosanStudio.Extension;
using UnityEngine.AI;

namespace WoosanStudio.ZombieShooter
{
    public class MonsterFactory : MonoBehaviour
    {
        [Tooltip("몬스터 세팅값 리스트")]
        public List<MonsterSettings> monsterSettings = new List<MonsterSettings>();
        [Tooltip("스폰 위치")]
        public SpawnPoints SpawnPoints;

        //작업해야함
        public LevelConfig monsterLevelConfig;

        IEnumerator Start()
        {
            while(true)
            {
                MakeMonster(0, SpawnPoints.GetSpawnPosition());

                yield return new WaitForSeconds(2f);
            }
        }

        /// <summary>
        /// 몬스터 생성
        /// </summary>
        /// <param name="id"></param>
        private void MakeMonster(int index,Transform parent = null)
        {
            GameObject clone = monsterSettings[index].MakeModel(monsterSettings[index].name, parent);
            //세팅값 넣어주기
            //clone.GetComponent<Character>().monsterSettings = Instantiate(monsterSettings[index]) as MonsterSettings;
            clone.GetComponent<Character>().monsterSettings = monsterSettings[index];

            if (parent != null) { clone.transform.parent = parent; } 
            clone.transform.Reset();
        }
    }
}
