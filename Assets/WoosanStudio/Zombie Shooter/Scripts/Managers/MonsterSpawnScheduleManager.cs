﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 몬스터의 생성을 관리함
    /// 스테이지 또는 맵 레벨에 마춰서 몬스터 생성
    /// </summary>
    public class MonsterSpawnScheduleManager : MonoBehaviour
    {
        [Header("[스테이지별 몬스터의 해당 스캐줄 리스트]")]
        public List<MonsterScheduleSetting> MonsterScheduleList = new List<MonsterScheduleSetting>();

        [Header("[현재 스폰된 몬스터 수]")]
        public int CurrentSpawnedMonster = 0;

        [Header("[전체 스폰된 몬스터 수]")]
        public int TotalSpawnedMonster = 0;

        //캐쉬용
        private MonsterScheduleSetting monsterSchedule;
        private Coroutine autoSpawnCallCoroutine;
        private WaitForSeconds WFS;
        private bool bSpawnedNamedMonster = false;
        private int round = 0;

        /// <summary>
        /// 해당 스테이지의 몬스터 스폰을 실행 한다.
        /// </summary>
        /// <param name="round">해당 스테이지</param>
        public void SpawnStage(int stage)
        {
            //미리 캐쉬에 받아 놓기
            monsterSchedule = MonsterScheduleList[stage];
            //코루틴 스폰 호출 간격 세팅
            WFS = new WaitForSeconds(monsterSchedule.SpawnInterval);
            //코루틴이 이미 실행 중이라면 중지
            if (autoSpawnCallCoroutine != null) StopCoroutine(autoSpawnCallCoroutine);
            //코루틴이 시작
            StartCoroutine(AutoSpawnCallCoroutine( monsterSchedule, WFS));
        }

        /// <summary>
        /// 한 라운드 에서 자동으로 스폰 시키는 코루틴
        /// </summary>
        /// <param name="round">해당 라운드</param>
        /// <param name="maxSameTimeSpawn">최대 동시 스폰 갯수</param>
        /// <param name="waitForSeconds">스폰과 스폰사이 interval</param>
        /// <returns></returns>
        IEnumerator AutoSpawnCallCoroutine( MonsterScheduleSetting monsterSchedule, WaitForSeconds waitForSeconds)
        {
            //첫 시작시 라운드 초기화
            round = 0;
            //현재 스폰 몬스터 0으로 초기화
            CurrentSpawnedMonster = 0;
            //전체 스폰 몬트터 0으로 초기화
            TotalSpawnedMonster = 0;

            while (true)
            {
                //몬스터 스폰
                Spawn(round, monsterSchedule);
                yield return waitForSeconds;
                //라운드 증가
                round++;
            } 
        }

        /// <summary>
        /// 실제 스폰시킴
        /// </summary>
        /// <param name="round">해당 라운드</param>
        /// <param name="monsterSchedule"></param>
        /// <param name="isFirst"></param>
        void Spawn(int round, MonsterScheduleSetting monsterSchedule,bool isFirst = false)
        {
            //맵에 최대 몬스터 생성 제한게 걸렸는으면 생성 중지
            if (CurrentSpawnedMonster < monsterSchedule.MaxSpawnLimit)
            {
                Debug.Log("몬스터 스폰이 정지 됐습니다. 맵에 최대 스폰 [" + monsterSchedule.MaxSpawnLimit + "]   현재 스폰 [" + CurrentSpawnedMonster + "]");
                return;
            }

            //전체 생성된 몬스터의 숫자가 해당 라운드 최대 스폰 수에 도달 했으면 정지
            if(TotalSpawnedMonster <= monsterSchedule.MaxSpawnByRound[round])
            {
                Debug.Log("몬스터 스폰이 정지 됐습니다. Total 스폰 [" + TotalSpawnedMonster + "]   round ["+ round + "] Max 스폰 [" + monsterSchedule.MaxSpawnByRound[round] + "]");
                return;
            }


            //첫번째 라운드에 네임드 몬스터 확인
            if(round == 0)
            {
                //현재 라운드가 네임드 몬스터 출현 라운드 인가?
                //네임드 스폰 인덱스에 현재 라운드가 존재 한다
                if (monsterSchedule.SpawnRoundIndexByNamedMonster.Exists(value => value.Equals(round)))
                {
                    //보스 몬스터 생성 요청
                }
            }

            //동시 스폰 갯수만큼 (일반)몬스터 스폰함
            for(int index = 0; index < monsterSchedule.MaxSameTimeSpawn;index++)
            {

                //현재 몬스터 카운트 증가
                CurrentSpawnedMonster++;
                //전체 생성된 몬스터 카운트 증ㄱ
                TotalSpawnedMonster++;
            }
        }
    }
}
