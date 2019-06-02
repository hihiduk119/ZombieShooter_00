using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using cakeslice;

namespace Woosan.SurvivalGame01
{
    /// <summary>
    /// 적을 생성 및 관리하는 메니져
    /// </summary>
    public class EnemyManager : MonoBehaviour
    {
        /// <summary>
        /// 적 애니메이션 과 네비 메쉬의 속도를 마춰야 하기에 필요
        /// </summary>
        [Serializable]
        public class EnemyAnimData
        {
            //에니메이션 컨트롤러
            [Header("[적 애니메이션]")]
            public RuntimeAnimatorController controller;
            //적 속도
            [Header("[속도 최소,최대]")]
            public Vector2 speed;
        }
        [Header("오브젝트 풀 최대 갯수 => 최소 시작시 미리 만들어 놓음")]
        public int enemyMaxCnt = 500;

        [Header("[적 기본틀]")]
        public GameObject pfEnemy;
        [Header("[기본틀에 넣을 적 모델들]")]
        public List<GameObject> pfEnemyModelList;

        [Header("[에니메이션과 해당 속도를 저장]")]
        public List<EnemyAnimData> enemyAnimDataList;

        [Header("[생성된것 컨트롤 하기위한 인터페이스 리스트] ")]
        [HideInInspector] public List<GameObject> enemyList = new List<GameObject>();

        [Header("[모든 적의 최상위 부모 ] ")]
        public Transform tfEnemys;



        [Header("[적 리스폰할 위치 Root]")]
        public Transform respawnPositionRoot;
        [Header("[적 리스폰할 기준 위치 => (자동세팅)]")]
        public List<Transform> respawnPositionList = new List<Transform>();

        //테스트용
        //리스폰위치 카운트
        private int respawnPositionCount = 0;
        //오브젝트 풀에서 다음 활성화할 적 카운트
        [HideInInspector] public int cnt = 0;
        //생성할때 이름에 붙일 카운트a
        [HideInInspector] public int makeCnt = 0;
        [Header("스폰용 카운트")]
        public UnityEngine.UI.Text text;



        private void Awake()
        {
            //적 리스폰 위치 리스트에 저장.
            for (int index = 0; index < respawnPositionRoot.childCount;index++)  { respawnPositionList.Add(respawnPositionRoot.GetChild(index));}
        }

        private void Start()
        {
            //최초 시작시 적 미리 만들어 놓음
            for (int index = 0; index < enemyMaxCnt; index++)
            {
                //적 리스폰 위치에 미리 만들어 놓기
                MakeEnemyForPool(respawnPositionList[respawnPositionCount].localPosition);
                respawnPositionCount++;
                if (respawnPositionCount >= respawnPositionList.Count) respawnPositionCount = 0;
            }
        }

        /// <summary>
        /// 적을 만듬 [테스트용 임시]
        /// </summary>
        IEnumerator MakeEnemy(Vector3 pos)
        {
            //적 프레임 만들기
            GameObject model = Instantiate(pfEnemyModelList[UnityEngine.Random.Range(0, 10)]);
            GameObject clone = Instantiate(pfEnemy, tfEnemys);
            //모델 만들기
            Enemy enemy = clone.GetComponent<Enemy>();
            model.transform.parent = enemy.tfModel;
            model.transform.localPosition = Vector3.zero;
            enemyList.Add(clone);

            clone.name = "Enemy (" + cnt + ")";
            //에니메이션 데이타 가져오기
            EnemyAnimData enemyAnimData = enemyAnimDataList[UnityEngine.Random.Range(0, enemyAnimDataList.Count)];
            //적 에니메이션 설정
            clone.GetComponent<Animator>().runtimeAnimatorController = enemyAnimData.controller;
            //적 속도 설정
            clone.GetComponent<NavMeshAgent>().speed = UnityEngine.Random.Range(enemyAnimData.speed.x, enemyAnimData.speed.y);
            //쫒아갈 Player 설정
            clone.GetComponent<AICharacterControl>().SetTarget(PlayerController.Instance.gameObject.transform);
            //나중에 수정
            clone.transform.localPosition = new Vector3(pos.x,0,pos.z);

            //일단 비활성화 => 바로 만들고 활성화 시키면 에니메이터에 값이 안들어감
            clone.SetActive(false);
            yield return new WaitForEndOfFrame();
            //한프레임 쉬고 활성화
            clone.SetActive(true);
            cnt++;
        }

        /// <summary>
        /// 적을 만듬 [오브젝트 풀용]
        /// </summary>
        void MakeEnemyForPool(Vector3 pos)
        {
            //적 프레임 만들기
            GameObject model = Instantiate(pfEnemyModelList[UnityEngine.Random.Range(0, 10)]);
            GameObject clone = Instantiate(pfEnemy, tfEnemys);
            //모델 만들기
            Enemy enemy = clone.GetComponent<Enemy>();
            model.transform.parent = enemy.tfModel;
            model.transform.localPosition = Vector3.zero;
            enemyList.Add(clone);

            //아웃 라인 넣기
            GameObject outline = model.transform.GetComponentInChildren<SkinnedMeshRenderer>().gameObject;
            outline.AddComponent<Outline>();
            outline.GetComponent<Outline>().color = 1;

            clone.name = "Enemy (" + makeCnt + ")";
            //에니메이션 데이타 가져오기
            EnemyAnimData enemyAnimData = enemyAnimDataList[UnityEngine.Random.Range(0, enemyAnimDataList.Count)];
            //적 에니메이션 설정
            clone.GetComponent<Animator>().runtimeAnimatorController = enemyAnimData.controller;
            //적 속도 설정
            clone.GetComponent<NavMeshAgent>().speed = UnityEngine.Random.Range(enemyAnimData.speed.x, enemyAnimData.speed.y);
            //쫒아갈 Player 설정
            clone.GetComponent<AICharacterControl>().SetTarget(PlayerController.Instance.gameObject.transform);
            //나중에 수정
            clone.transform.localPosition = new Vector3(pos.x, 0, pos.z);

            //일단 비활성화 => 바로 만들고 활성화 시키면 에니메이터에 값이 안들어감
            clone.SetActive(false);
            makeCnt++;
        }

        /// <summary>
        /// 오브젝트 풀에서 적 활성화
        /// </summary>
        void Spawn() 
        {
            //활성화
            enemyList[cnt].gameObject.SetActive(true);
            cnt++;
        }

        IEnumerator AutoSpawn() {
            while(cnt <= enemyMaxCnt) {
                yield return new WaitForSeconds(0.1f);
                enemyList[cnt].gameObject.SetActive(true);
                cnt++;
                text.text = cnt.ToString();
            }
        }

        void OnGUI()
        {
            //if (GUI.Button(new Rect(0, 0, 200, 150), "make"))
            //{
            //    StartCoroutine(MakeEnemy(respawnPositionList[respawnPositionCount].localPosition));
            //    respawnPositionCount++;
            //    if (respawnPositionCount >= respawnPositionList.Count) respawnPositionCount = 0;
            //}

            if (GUI.Button(new Rect(0, 0, 200, 150), "Activate Enemy cnt ["+ cnt+"]"))
            {
                //Spawn();
                StartCoroutine(AutoSpawn());
            }
        }
    }
}
