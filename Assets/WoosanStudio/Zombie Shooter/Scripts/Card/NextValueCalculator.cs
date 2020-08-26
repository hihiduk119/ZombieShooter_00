using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 모든 다음 것을 계산해주는 계산기
    /// * 연구 시간
    /// * 연구 비용
    /// </summary>
    public class NextValueCalculator : MonoBehaviour
    {
        [Header("[공식의 호출부 이며 Invoke를 통해서 해당 공식 사용함]")]
        [SerializeField]
        public FormulaStartEvent StartEvent = new FormulaStartEvent();

        [Header("[공식의 완료 이벤트 부분 => 계산 완료된 값을 받기위해 리스너에 등록 필요]")]
        [SerializeField]
        public FormulaEndEvent EndEvent = new FormulaEndEvent();

        //*아래는 호출될때마다 바뀌지만 사용은 같이 하기때문에 동시 호출시 문제가 발생할수 있음
        //저장시킨 레벨
        private int level;
        //저장된 메서드 이름
        private string methodName;

        private void Awake()
        {
            //남은 시간 계산 공식 등록
            //*공식이 추가될때 마다 등록 시켜야 함.
            StartEvent.AddListener(RunFormula);

            //*리스너에 2개 동시에 있을경우 Total 이 중첩되어 적용됨 알아둬야함.
            //디버그용 리스너 등록 => TestCode 삭제시 삭제 요망.
            //EndEvent.AddListener(ResearchTimeCallback);
            //디버그용 리스너 등록 => TestCode 삭제시 삭제 요망.
            EndEvent.AddListener(CoinCallback);
        }

        /// <summary>
        /// 계산을 실행하는 부분 이며 임의로 메서드를 변경 할수 있음
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="level"></param>
        void RunFormula(string methodName, int level)
        {
            this.level = level;
            this.methodName = methodName;

            Invoke(methodName, 0);
        }

        /// <summary>
        /// 레벨에 맞는 공격 속도 업글 시간을 가져옴
        /// </summary>
        /*void AttackSpeed()
        {
            //공격속도 업글 시간 공식 (25레벨 시 9Day 소요,total = 62Day)
            int seconds = (level * level * level * 30) * 2 + 30;

            //계산 완료 및 일종의 리턴 호출
            EndEvent.Invoke(seconds);
        }*/

        /// <summary>
        /// 기본 레벨 업글 시간
        /// * 메서드 추가로 새로운 공식 넣으면 됨.
        /// </summary>
        void ResearchTime()
        {
            //공격속도 업글 시간 공식 (25레벨 시 9Day 소요,total = 62Day)
            int seconds = (level * level * level * 30) * 2 + 30;


            //계산 완료 및 일종의 리턴 호출
            EndEvent.Invoke(seconds);
        }

        /// <summary>
        /// 코인공식 레벨에 따라 올라가는
        /// This is a coin calculation formula that step by step by level.
        /// </summary>
        void Coin()
        {
            //공격속도 업글 시간 공식 (25레벨 시 9Day 소요,total = 62Day)
            int seconds = (level * level * level * level * 5) * 2 + 30;

            //계산 완료 및 일종의 리턴 호출
            EndEvent.Invoke(seconds);
        }



        #region [-TestCode]
        //전체 시간 계산용
        private int total = 0;

        /// <summary>
        /// ResearchTime 결과 출력용
        /// </summary>
        /// <param name="seconds"></param>
        void ResearchTimeCallback(int seconds)
        {
            //생성시 셋업으로 현재 시간을 세팅
            Timeset secondsTimeset = new Timeset(seconds);
            //실행을 통해 현재시간 대비 해당 남은 시간 가져옴
            secondsTimeset.GetRemainTime();

            //전체 시간을 구하기 위해 더해줌
            total += seconds;

            Timeset totalTimeset = new Timeset(total);
            totalTimeset.GetRemainTime();

            Debug.Log("[level : " + level + "]  seconds = " + seconds + " s  [" + secondsTimeset.TimeString + "]  " + "+ total = ["+ totalTimeset.TimeString + "]");
        }


        /// <summary>
        /// coin 결과 출력
        /// </summary>
        /// <param name="coin"></param>
        void CoinCallback(int coin)
        {
            //전체 시간을 구하기 위해 더해줌
            total += coin;

            Debug.Log("[level : " + level + "] "+"[coin : " + coin + "] "+ "total = [" + total + "]");
        }

        /// <summary>
        /// 25레벨 까지 순차 실행하기 위해 만든 임시 메서드
        /// </summary>
        /// <returns></returns>
        IEnumerator SequenceRun()
        {
            //레벨 25까지 실행
            for (int index = 0; index < 25; index++)
            {
                //다음 연구 시간을 알아오기 위해 ResearchTime 이름 메서드 실행
                //StartEvent.Invoke("ResearchTime", index);

                //다음 코인량을 알아오기위해 Coin 이름 메서드 실행
                StartEvent.Invoke("Coin", index);

                yield return new WaitForEndOfFrame();
            }
        }

        void Update()
        {
            //순차적으로 모든 레벨업 요구 시간 보여주기
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                StartCoroutine(SequenceRun());
            }
        }
        #endregion

    }

    //공식의 호출부 이며 Invoke를 통해서 해당 공식 사용함
    [System.Serializable]
    public class FormulaStartEvent : UnityEvent<string, int> { }

    //공식의 완료 이벤트 부분
    [System.Serializable]
    public class FormulaEndEvent : UnityEvent<int> { }
}
