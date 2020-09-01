using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WoosanStudio.Extension;
using UnityEngine.AI;
using WoosanStudio.Common;
using UnityEngine.Events;

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
        
        //[Header("스폰 위치")]
        //public SpawnPoints SpawnPoints;

        //[Header("스테이지 모음 루트")]
        //public Transforms parent;

        //[Header("스테이지 모음 리스트")]
        //public List<Transform> SpawnPointList = new List<Transform>();

        //[Header("땅에 떨어진 아이템 리퀘스터 [(Auto->Awake())]")]
        [HideInInspector]
        //몬스터 리퀘스터 에서 넣어주기 때문에 숨겨둘 필요가 있음
        public ItemRequester ItemRequester;

        [Header("몬스터 죽임시 행동 액션]")]
        public List<UnityAction<Vector3>> MonsterDieActions = new List<UnityAction<Vector3>>();

        //해당 레벨에 의해 생성위치가 변함
        [Header("스테이지 레벨")]
        public int Level = 1;

        //캐쉬
        Coroutine coroutineInfiniteMakeMonster;

        private void Awake()
        {
            //몬스터 생성할 스테이지의 생성 위치를 가져오기 위해
            //Transforms.FindChildInFirstLayer(ref SpawnPointList, parent.transform);

            //땅에 떨어진 아이템 만드는 리퀘스터 가져오기
            ItemRequester = GameObject.FindObjectOfType<ItemRequester>();
        }

        /// <summary>
        /// 외부에서 실행하는 Make
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public GameObject Make(int index, SpawnPoints spawnPoints)
        {
            //몬스터 생성
            return  MakeMonster(monsterSettings[index], spawnPoints.GetSpawnPosition());
        }

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

            //생성시 해당 DoDie.cs의 GoToHeavenEvent 에 
            //아이템 리퀘스터의 리퀘스터 부분 연결
            //=> 액션 리스트를 받아서 처리하게 바꿔야 함.

            MonsterDieActions?.ForEach(value => clone.GetComponent<DoDie>().HeavenEvent.AddListener(value));
            //clone.GetComponent<DoDie>().HeavenEvent.AddListener(ItemRequester.Requester);

            //그림자 생성.
            MakeShadow(monsterSettings, clone.transform);

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
