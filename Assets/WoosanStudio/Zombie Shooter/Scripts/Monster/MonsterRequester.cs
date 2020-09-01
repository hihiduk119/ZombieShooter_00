﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 몬스터를 어떻게 구성할지 요청
    /// </summary>
    public class MonsterRequester : MonoBehaviour
    {
        [Header("[몬스터 팩토리]")]
        public MonsterFactory MonsterFactory;

        [Header("스폰 위치")]
        public SpawnPositionController SpawnPositionController;

        private SpawnPoints spawnPoints;
        private Coroutine infiniteMonsterRequestCoroutine;

        //해당 레벨에 의해 생성위치가 변함
        [Header("스테이지 레벨")]
        public int Level = 0;

        [Header("몬스터 인덱스")]
        public int Index = 0;

        [Header("아이템 리퀘스터 [(Auto->Awake())]")]
        public ItemRequester ItemRequester;

        private void Awake()
        {
            //자동으로 할당
            ItemRequester = GameObject.FindObjectOfType<ItemRequester>();
        }

        /// <summary>
        /// 몬스터 요청
        /// </summary>
        /// <param name="level">스테이지 레벨</param>
        /// <param name="index">몬스터 조</param>
        public void Requester(int level, int index)
        {
            //스폰 리스트에서 스폰위치 현재 레벨에 맞는 스폰위치 가져옴
            spawnPoints = SpawnPositionController.GetSpawnPoints(level);

            //몬스터 죽음시 아이템 생성 요청.
            MonsterFactory.MonsterDieActions.Add(ItemRequester.Requester);

            //몬스터 생성
            GameObject clone = MonsterFactory.Make(index, spawnPoints);
        }

        #region [-TestCode]
        /// <summary>
        /// 테스트용으로 무한하게 몬스터 랜덤으로 만듬
        /// </summary>
        /// <returns></returns>
        IEnumerator InfiniteMonsterRequest()
        {
            while (true)
            {
                //몬스터 랜덤 생성
                int index = Random.Range(0, 3);
                //요청
                Requester(Level, index);

                yield return new WaitForSeconds(3f);
            }
        }

        /// <summary>
        /// 해당 레벨로 몬스터 만들기
        /// 무한으로 몬스터를 만든다.
        /// *해당 레벨은 Level 이다.
        /// ** 이 부분이 몬스터 스폰 스케줄 매니저가 해야 한다.
        /// </summary>
        public void MakeMonsterByStageLevel()
        {
            //* 이 부분 -> 몬스터를 생성하고 어떤 스케줄로 스폰하는지를
            //           몬스터 스폰 스케줄 메니저가 해야한다
            //코루틴이 실행 중이면 일단 정지
            Stop();
            //몬스터 무한 생성 코루틴 시작
            infiniteMonsterRequestCoroutine = StartCoroutine(InfiniteMonsterRequest());
        }

        /// <summary>
        /// 무한대로 몬스터 만드는 코루틴 정지
        /// </summary>
        public void Stop()
        {
            if (infiniteMonsterRequestCoroutine != null) StopCoroutine(infiniteMonsterRequestCoroutine);
        }
        #endregion
    }
}
