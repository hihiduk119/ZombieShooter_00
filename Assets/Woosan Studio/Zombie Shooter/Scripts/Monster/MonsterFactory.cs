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

        #region [-TestCode]
        //bool testCode = true;
        IEnumerator Start()
        {
            while(true)
            {
                //testCode = !testCode;

                //if(testCode) 
                //    MakeMonster(monsterSettings[0], SpawnPoints.GetSpawnPosition());
                //else
                //    MakeMonster(monsterSettings[1], SpawnPoints.GetSpawnPosition());

                int index = Random.Range(0, 3);
                //몬스터 생성
                GameObject clone = MakeMonster(monsterSettings[index], SpawnPoints.GetSpawnPosition());
                //그림자 생성.
                MakeShadow(monsterSettings[index],clone.transform);

                yield return new WaitForSeconds(2f);
            }
        }
        #endregion

        /// <summary>
        /// 몬스터 생성
        /// </summary>
        /// <param name="id"></param>
        private GameObject MakeMonster(MonsterSettings monsterSettings, Transform parent = null)
        {
            GameObject clone = monsterSettings.MakeModel(monsterSettings.name, parent);
            //세팅값 넣어주기
            //clone.GetComponent<Character>().monsterSettings = Instantiate(monsterSettings[index]) as MonsterSettings;
            clone.GetComponent<Monster>().monsterSettings = monsterSettings;

            if (parent != null) { clone.transform.parent = parent; } 
            clone.transform.Reset();

            //생성된 몬스터를 몬스터 메니저에 등록[몬스터 메니저는 AI 플레이어의 자동 타겟을 찾기위해 사용됨]
            MonsterList.Instance.Items.Add(clone.transform);

            return clone;
        }

        /// <summary>
        /// 몬스터 그림자 생성
        /// </summary>
        /// <param name="monsterSettings"></param>
        /// <param name="parent"></param>
        private void MakeShadow(MonsterSettings monsterSettings,Transform parent)
        {
            GameObject clone = Instantiate(monsterSettings.ShadowProejector);
            //부모 지정
            clone.transform.parent = parent.GetChild(0).GetChild(0);
            //그림자 위치 초기화
            clone.transform.localPosition = new Vector3(0, 5, 0);
        }             
    }
}
