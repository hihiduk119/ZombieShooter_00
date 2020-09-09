using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 몬스터의 생성을 관리함
    /// 스테이지 또는 맵 레벨에 마춰서 몬스터 생성
    /// 라운드의 스캐줄도 직업 합.
    /// </summary>
    public class MonsterSpawnScheduleManager : MonoBehaviour
    {
        [Header("[스테이지별 몬스터의 해당 스캐줄 리스트]")]
        public List<MonsterScheduleSetting> MonsterScheduleList = new List<MonsterScheduleSetting>();

        [Header("[현재 스폰된 몬스터 수]")]
        public int CurrentSpawnedMonster = 0;

        [Header("[전체 스폰된 몬스터 수]")]
        public int TotalSpawnedMonster = 0;

        [Header("[실제 몬스터를 호출하는 부분 [Auto->Awake()]]")]
        public MonsterRequester MonsterRequester;

        [Header("[라운드가 시작됨을 알림 이벤트]")]
        public UnityEvent StartSpawnEvent = new UnityEvent();
        
        //스폰이 끝났다는걸 알아야 라운드의 끝을 계산할수 있기때문에 몬스터 매니저가와의 연결 필요
        [Header("[스폰이 끝났음을 알림 이벤트]")]
        public UnityEvent EndSpawnEndEvent = new UnityEvent();

        //모든 스폰이 끝났다는걸 알아야 남은 몬스터에서 게임 끝을 계산할수 있음
        [Header("[모든 스폰이 끝났음을 알림 이벤트]")]
        public UnityEvent EndSpawnByAllRoundEvent = new UnityEvent();

        //캐쉬용
        private MonsterScheduleSetting monsterSchedule;
        private Coroutine autoSpawnCallCoroutine;
        private WaitForSeconds WFS;
        private bool bSpawnedNamedMonster = false;

        //현재 라운드
        private int currentRound = 0;
        private int currentStage = 0;

        private void Awake()
        {
            //자동으로 찾아서 넣어줌
            MonsterRequester = GameObject.FindObjectOfType<MonsterRequester>();
        }

        /// <summary>
        /// 해당 스테이지의 몬스터 스폰을 실행.
        /// 라운드의 첫 시작
        /// </summary>
        /// <param name="round">해당 스테이지</param>
        public void SpawnByStage(int stage)
        {
            //현재 스테이지 저장
            currentStage = stage;

            //현재 라운드 0으로 초기화
            currentRound = 0;
            
            //라운드 별 몬스터 스폰 실행
            SpawnByRound(stage,currentRound);
        }

        /// <summary>
        /// 라운드 별 몬스터 스폰 실행.
        /// *현재 라운드 수동으로 증가 필요
        /// </summary>
        /// <param name="stage">해당 스테이지</param>
        /// <param name="round">해당 라운드</param>
        public void SpawnByRound(int stage,int round)
        {
            //미리 캐쉬에 받아 놓기
            monsterSchedule = MonsterScheduleList[stage];
            //코루틴 스폰 호출 간격 세팅
            WFS = new WaitForSeconds(monsterSchedule.SpawnInterval);
            //코루틴이 이미 실행 중이라면 중지
            if (autoSpawnCallCoroutine != null) StopCoroutine(autoSpawnCallCoroutine);
            //코루틴이 시작
            autoSpawnCallCoroutine = StartCoroutine(AutoSpawnCallCoroutine(stage, round, monsterSchedule, WFS));
        }

        /// <summary>
        /// 다음 라운드 스폰 실행
        ///  SpawnByRound같으나 자동으로 라운드 증가
        /// </summary>
        public void SpawnByNextRound()
        {
            //현재 라운드 자동 증가
            currentRound++;
            //미리 캐쉬에 받아 놓기
            monsterSchedule = MonsterScheduleList[currentStage];
            //코루틴 스폰 호출 간격 세팅
            WFS = new WaitForSeconds(monsterSchedule.SpawnInterval);
            //코루틴이 이미 실행 중이라면 중지
            if (autoSpawnCallCoroutine != null) StopCoroutine(autoSpawnCallCoroutine);
            //코루틴이 시작
            autoSpawnCallCoroutine = StartCoroutine(AutoSpawnCallCoroutine(currentStage, currentRound, monsterSchedule, WFS));
        }

        /// <summary>
        /// 한 라운드 에서 자동으로 스폰 시키는 코루틴
        /// </summary>
        /// <param name="round">해당 라운드</param>
        /// <param name="maxSameTimeSpawn">최대 동시 스폰 갯수</param>
        /// <param name="waitForSeconds">스폰과 스폰사이 interval</param>
        /// <returns></returns>
        IEnumerator AutoSpawnCallCoroutine(int stage,int round, MonsterScheduleSetting monsterSchedule, WaitForSeconds waitForSeconds)
        {
            //현재 스폰 몬스터 0으로 초기화
            CurrentSpawnedMonster = 0;
            //전체 스폰 몬트터 0으로 초기화
            TotalSpawnedMonster = 0;

            while (true)
            {
                //몬스터 스폰
                Spawn(stage, round, monsterSchedule);
                yield return waitForSeconds;
            } 
        }

        /// <summary>
        /// 실제 스폰시킴
        /// </summary>
        /// <param name="round">해당 라운드</param>
        /// <param name="monsterSchedule"></param>
        /// <param name="isFirst"></param>
        void Spawn(int stage, int round, MonsterScheduleSetting monsterSchedule,bool isFirst = false)
        {
            
            //맵에 최대 몬스터 생성 제한게 걸렸는으면 생성 중지
            if (MonsterList.Instance.Items.Count >= monsterSchedule.MaxSpawnLimit)
            {
                Debug.Log("몬스터 스폰이 정지 됐습니다. 맵에 최대 스폰 [" + monsterSchedule.MaxSpawnLimit + "]   현재 스폰 [" + MonsterList.Instance.Items.Count + "]");
                return;
            }

            //전체 생성된 몬스터의 숫자가 해당 라운드 최대 스폰 수에 도달 했으면 스폰 코루틴정지
            if(TotalSpawnedMonster >= monsterSchedule.MaxSpawnByRound[round])
            {
                Debug.Log("몬스터 스폰이 정지 됐습니다. Total 스폰 [" + TotalSpawnedMonster + "]   round ["+ round + "] 한라운드 당 최대 Max 스폰 [" + monsterSchedule.MaxSpawnByRound[round] + "]");

                //몬스터 생성이 모두 끝났으니 몬스터 리스트에서 모든 몬스터가 모두 사라지면 이벤트 보낸는 메서드 실행.
                //* popup의 실행은 스테이지 메이저가 관리
                MonsterList.Instance.ActiveEmptyEvent();

                //현재 코루틴을 중지 시킨다
                if (autoSpawnCallCoroutine != null){ StopCoroutine(autoSpawnCallCoroutine);}
                Debug.Log("코루틴Null 이 아니면 을 중지 합니다");
            }

            Debug.Log("현재 스폰 상태 =>  Total 스폰 [" + TotalSpawnedMonster + "]   round [" + round + "]  한라운드 당 최대 Max 스폰 [" + monsterSchedule.MaxSpawnByRound[round] + "]  맵에 최대 스폰 = [" + monsterSchedule.MaxSpawnLimit + "]");


            //첫번째 라운드에 네임드 몬스터 확인
            //*이거 테스트 코드임
            //*원래는 마지막 라운드에 출현 시켜야 함
            //*현재 스폰 카운트가 0보다 크면
            //** 지금은 0번째 라운드의 최 스폰 처음에 한번 출현으로 작성 
            if (round == 0 && CurrentSpawnedMonster == 0)
            {
                //현재 라운드가 네임드 몬스터 출현 라운드 인가?
                //네임드 스폰 인덱스에 현재 라운드가 존재 한다
                if (monsterSchedule.SpawnRoundIndexByNamedMonster.Exists(value => value.Equals(round)))
                {
                    //보스 몬스터 생성 요청
                    MonsterRequester.RequesterBySetting(stage,monsterSchedule.NamedMonster);
                    Debug.Log("============= 보스 생성 OK =============");
                }
            }

            //동시 스폰 갯수만큼 (일반)몬스터 스폰함
            for(int index = 0; index < monsterSchedule.MaxSameTimeSpawn;index++)
            {
                //몬스터 랜덤 생성 => 몬스터 스캐줄의 생성몬스터 인덱스대로 생성
                int monsterIndex = Random.Range(0, monsterSchedule.Monsters.Count);
                //실제 스폰이 일어남.
                //몬스터 리퀘스트 호출.
                //*실제 몬스터 호출 부이며 index 부분은 좀더 생각해서 작업할 필요가 있음
                MonsterRequester.RequesterBySettings(stage, monsterIndex, monsterSchedule.Monsters);
                //현재 몬스터 카운트 증가
                CurrentSpawnedMonster++;
                //전체 생성된 몬스터 카운트 증가
                TotalSpawnedMonster++;

                Debug.Log("[몬스터 호출] 현재 스폰카운트 = " + CurrentSpawnedMonster + "    전체 생성 몬스터 카운트 = " + TotalSpawnedMonster);
            }
        }

        /// <summary>
        /// 라운드의 끝인지 알려줌.
        /// </summary>
        /// <returns></returns>
        public bool IsEndRound()
        {
            bool value = false;

            //현재 스테이지의 마지막 라운드 인지 확인
            if (MonsterScheduleList[currentStage].MaxSpawnByRound.Count <= currentRound) { value = true;}

            Debug.Log("현재 라운드는 ["+ currentStage + "] 입니다 끝 라운드["+ value + "]");

            return value;
        }
    }
}
