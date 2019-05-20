using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.AI;

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
            [Header("[적 애니메이션a]")]
            public RuntimeAnimatorController controller;
            //적 속도
            [Header("[속도 최소,최대]")]
            public Vector2 speed;
        }

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
        [HideInInspector] public int cnt = 0;

        void MakeEnemy()
        {
            //적 프레임 만들기
            GameObject clone = Instantiate(pfEnemy, tfEnemys);
            clone.name = "Enemy (" + cnt + ")";
            //에니메이션 데이타 가져오기
            EnemyAnimData enemyAnimData = enemyAnimDataList[UnityEngine.Random.Range(0, enemyAnimDataList.Count)];
            //적 에니메이션 설정
            clone.GetComponent<Animator>().runtimeAnimatorController = enemyAnimData.controller;
            //적 속도 설정
            clone.GetComponent<NavMeshAgent>().speed = UnityEngine.Random.Range(enemyAnimData.speed.x, enemyAnimData.speed.y);

            //나중에 수정
            clone.transform.localPosition = Vector3.zero;

            //모델 만들기
            GameObject model = Instantiate(pfEnemyModelList[UnityEngine.Random.Range(0,10)]);
            Enemy enemy = clone.GetComponent<Enemy>();
            model.transform.parent = enemy.tfModel;
            model.transform.localPosition = Vector3.zero;
            enemyList.Add(clone);

            clone.SetActive(false);

            cnt++;
        }

        void AllSpawn() 
        {
            enemyList.ForEach(value => value.SetActive(true));
        }

        void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 200, 150), "make"))
            {
                MakeEnemy();
            }

            if (GUI.Button(new Rect(0, 150, 200, 150), "spawn"))
            {
                AllSpawn();
            }
        }
    }
}
