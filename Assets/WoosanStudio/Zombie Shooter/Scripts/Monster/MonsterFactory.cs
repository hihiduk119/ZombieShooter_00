using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WoosanStudio.Extension;
using UnityEngine.AI;

using WoosanStudio.Common;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 몬슨터를 만듬
    /// 생성 위치에 따라 해당 위치에 생성 시켜줌 
    /// </summary>
    public class MonsterFactory : MonoBehaviour
    {
        [Header("몬스터 세팅값 리스트")]
        public List<MonsterSettings> monsterSettings = new List<MonsterSettings>();
        
        [Header("스폰 위치")]
        public SpawnPoints SpawnPoints;

        [Header("스테이지 모음 루트")]
        public Transforms parent;

        [Header("스테이지 모음 리스트")]
        public List<Transform> SpawnPointList = new List<Transform>();

        //해당 레벨에 의해 생성위치가 변함
        [Header("스테이지 레벨")]
        public int Level = 1;

        Coroutine coroutineInfiniteMakeMonster;

        private void Awake()
        {
            //몬스터 생성할 스테이지의 생성 위치를 가져오기 위해
            Transforms.FindChildInFirstLayer(ref SpawnPointList, parent.transform);
        }

        #region [-TestCode]
        //bool testCode = true;
        //IEnumerator Start()
        //{
        //    while (true)
        //    {
        //        Initialize();

        //        yield return new WaitForSeconds(2f);
        //    }
        //}

        /// <summary>
        /// 테스트용으로 무한하게 몬스터 랜덤으로 만듬
        /// </summary>
        /// <returns></returns>
        IEnumerator InfiniteMakeMonster(int level)
        {
            while (true)
            {
                //testCode = !testCode;

                //if(testCode) 
                //    MakeMonster(monsterSettings[0], SpawnPoints.GetSpawnPosition());
                //else
                //    MakeMonster(monsterSettings[1], SpawnPoints.GetSpawnPosition());

                int index = Random.Range(0, 3);

                //int index = 1;
                //스폰 리스트에서 스폰위치 현재 레벨에 맞는 스폰위치 가져옴
                SpawnPoints = SpawnPointList[level].GetComponent<SpawnPoints>();

                //몬스터 생성
                GameObject clone = MakeMonster(monsterSettings[index], SpawnPoints.GetSpawnPosition());
                //그림자 생성.
                MakeShadow(monsterSettings[index], clone.transform);

                yield return new WaitForSeconds(3f);
            }
        }

        /// <summary>
        /// 해당 레벨로 몬스터 만들기
        /// 무한으로 몬스터를 만든다.
        /// *해당 레벨은 Level 이다.
        /// </summary>
        public void MakeMonsterByStageLevel()
        {
            Stop();
            coroutineInfiniteMakeMonster = StartCoroutine(InfiniteMakeMonster(Level));
        }

        /// <summary>
        /// 무한대로 몬스터 만드는 코루틴 정지
        /// </summary>
        public void Stop()
        {
            if (coroutineInfiniteMakeMonster != null) StopCoroutine(coroutineInfiniteMakeMonster);
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

        #region [-TestCode]
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                MakeMonsterByStageLevel();
            }
        }
        #endregion

    }
}
