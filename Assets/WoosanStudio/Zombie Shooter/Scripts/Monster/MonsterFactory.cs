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
        [Header("[몬스터 세팅값 리스트 => 스케줄러에서 재새팅 되기 때문에 현재 값은 테스트용]")]
        public List<MonsterSettings> monsterSettings = new List<MonsterSettings>();

        //[Header("스폰 위치")]
        //public SpawnPoints SpawnPoints;

        //[Header("스테이지 모음 루트")]
        //public Transforms parent;

        //[Header("스테이지 모음 리스트")]
        //public List<Transform> SpawnPointList = new List<Transform>();

        //[Header("땅에 떨어진 아이템 리퀘스터 [(Auto->Awake())]")]

        //몬스터 리퀘스터 에서 넣어주기 때문에 숨겨둘 필요가 있음
        [HideInInspector]
        public ItemRequester ItemRequester;

        [Header("몬스터 하늘로 뜰때 행동 액션]")]
        public List<UnityAction<Vector3>> MonsterGoHeavenActions = new List<UnityAction<Vector3>>();

        [Header("몬스터 죽임시 행동 액션]")]
        public List<UnityAction<Vector3>> MonsterOnDieActions = new List<UnityAction<Vector3>>();

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
        /// 외부에서 실행하는 Make
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public GameObject Make(MonsterSettings monsterSettings, SpawnPoints spawnPoints)
        {
            //몬스터 생성
            //*몬스터 세팅 정보는 Instantiate로 생성
            return MakeMonster(Instantiate<MonsterSettings>(monsterSettings), spawnPoints.GetSpawnPosition());
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
            //리셋시 모든 것이 0 또는 1로 초기화됨.
            //이때 스케일도 초기화 됨. => 스케일을 원본 프리팹을 사용해야함
            clone.transform.Reset();

            //스케일은 원본 프리팹을 사용 해야함
            clone.transform.localScale = monsterSettings._prefab.transform.localScale;

            //생성된 몬스터를 몬스터 메니저에 등록[몬스터 메니저는 AI 플레이어의 자동 타겟을 찾기위해 사용됨]
            MonsterList.Instance.Items.Add(clone.transform);

            //생성시 해당 DoDie.cs의 GoToHeavenEvent 에 
            //아이템 리퀘스터의 리퀘스터 부분 연결
            //=> 액션 리스트를 받아서 처리하게 바꿔야 함.
            //*죽자 마자 생성 되게 바꾸는 중 
            MonsterGoHeavenActions?.ForEach(value => clone.GetComponent<DoDie>().GoHeavenEvent.AddListener(value));
            //clone.GetComponent<DoDie>().HeavenEvent.AddListener(ItemRequester.Requester);

            //죽을때 슬로우 모션액션을 이벤트에 넣음
            MonsterOnDieActions.Add(SlowMotionTimeManager.Instance.DoSlow);

            MonsterOnDieActions?.ForEach(value => clone.GetComponent<DoDie>().OnDieEvent.AddListener(value));

            //몬스터 레벨에 맞는 성장 수치 가져오기
            int level = monsterSettings.Level;

            //몬스터 체력 세팅
            //*레벨에 성장에 의해 변화한 체력을 가져온다
            clone.GetComponent<PlayerBar>().SetHealth(monsterSettings.Health);

            //몬스터 레벨에 맞는 컬러 가져옴
            clone.GetComponent<OutlineColor>().SetColor(MonsterSettings.GrowthTable.GetLevelColor(level));

            //그림자 생성.
            MakeShadow(monsterSettings, clone.transform);

            //초기화 안하면 다음번 생성된 녀석이 같은 액션을 가져감
            MonsterGoHeavenActions.Clear();
            MonsterOnDieActions.Clear();

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
