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

        bool testCode = true;

        #region [-TestCode]
        IEnumerator Start()
        {
            while(true)
            {

                testCode = !testCode;

                if(testCode) 
                    MakeMonster(monsterSettings[0], SpawnPoints.GetSpawnPosition());
                else
                    MakeMonster(monsterSettings[1], SpawnPoints.GetSpawnPosition());

                yield return new WaitForSeconds(2f);
            }
        }
        #endregion

        /// <summary>
        /// 몬스터 생성
        /// </summary>
        /// <param name="id"></param>
        private void MakeMonster(MonsterSettings monsterSettings, Transform parent = null)
        {
            GameObject clone = monsterSettings.MakeModel(monsterSettings.name, parent);
            //세팅값 넣어주기
            //clone.GetComponent<Character>().monsterSettings = Instantiate(monsterSettings[index]) as MonsterSettings;
            clone.GetComponent<Monster>().monsterSettings = monsterSettings;

            if (parent != null) { clone.transform.parent = parent; } 
            clone.transform.Reset();

            //생성된 몬스터를 몬스터 메니저에 등록[몬스터 메니저는 AI 플레이어의 자동 타겟을 찾기위해 사용됨]
            MonsterList.Instance.Items.Add(clone.transform);
        }
    }
}
