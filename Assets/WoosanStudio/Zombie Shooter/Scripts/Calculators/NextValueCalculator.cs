using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using System;


namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 모든 다음 것을 계산해주는 계산기
    /// * 연구 시간
    /// * 연구 비용
    /// </summary>
    public class NextValueCalculator : MonoBehaviour
    {
        //공식의 호출부 이며 Invoke를 통해서 해당 공식 사용함
        [System.Serializable]
        public class FormulaStartEvent : UnityEvent<string, object> { }

        //공식의 완료 이벤트 부분
        [System.Serializable]
        public class FormulaEndEvent : UnityEvent<object> { }

        [System.Serializable]
        //카드 계산시 필요 데이터
        public class CardCalcutionData
        {
            //카드 레벨
            public int Level;
            //카드 중첩
            public int Stack;
            //기본 값
            public int Value;
            //기본 퍼센트 값
            public int DefalutIncreaseValue;
            //레벨 1당 증가 퍼센트
            public int IncreaseValuePerLevel;

            //생성자
            public CardCalcutionData(int level, int stack, int value, int defalutIncreaseValue, int increaseValuePerLevel)
            {
                Level = level;
                Stack = stack;
                Value = value;
                DefalutIncreaseValue = defalutIncreaseValue;
                IncreaseValuePerLevel = increaseValuePerLevel;
            }
        }

        /// <summary>
        /// FormulaEndEvent 에서 사용할 콜백 데이터
        /// </summary>
        public class CallBackData
        {
            public int Level;
            public int Value;

            public CallBackData(int level, int value)
            {
                Level = level;
                Value = value;
            }
        }


        [Header("[공식의 호출부 이며 Invoke를 통해서 해당 공식 사용함]")]
        [SerializeField]
        public FormulaStartEvent StartEvent = new FormulaStartEvent();

        [Header("[공식의 완료 이벤트 부분 => 계산 완료된 값을 받기위해 리스너에 등록 필요]")]
        [SerializeField]
        public FormulaEndEvent EndEvent = new FormulaEndEvent();

        //*아래는 호출될때마다 바뀌지만 사용은 같이 하기때문에 동시 호출시 문제가 발생할수 있음
        //저장시킨 레벨
        //private int level;
        //저장된 메서드 이름
        //private string methodName;

        //해당 공식 실행 코루틴
        private Coroutine formulaCoroutine;

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
        void RunFormula(string methodName, object myObject)
        {
            /*try
            {
                //this.level = (int)myObject;  
            }
            catch (InvalidCastException)
            {
                Console.WriteLine("my Object => int 변환 실패");
            }

            try
            {
                //this.level = ((CardCalcutionData)myObject).Level;
            }
            catch (InvalidCastException)
            {
                Console.WriteLine("my Object => CardCalcutionData 변환 실패");
            }*/

            //this.methodName = methodName;

            //Invoke(methodName, 0);
            //이전 코루틴이 있으면 제거
            //*이벤트 처리가 하나라서 이렇게 마듬
            if (formulaCoroutine != null) { StopCoroutine(formulaCoroutine); }
            //코루틴으로 해당 메서드 실행
            formulaCoroutine = StartCoroutine(methodName+"Coroutine", myObject);
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
        IEnumerator ResearchTimeCoroutine(object myObject)
        {
            int level = (int)myObject;

            //공격속도 업글 시간 공식 (25레벨 시 9Day 소요,total = 62Day)
            int seconds = (level * level * level * 30) * 2 + 30;

            CallBackData returnData = new CallBackData(level, seconds);

            //계산 완료 및 일종의 리턴 호출
            EndEvent.Invoke(returnData);

            yield break;
        }

        /// <summary>
        /// 연구 비용 증가 공식 레벨에 따라 올라가는
        /// </summary>
        IEnumerator CoinCoroutine(object myObject)
        {
            int level = (int)myObject;

            //코인 추가 공식 
            int seconds = (level * level * level  * 5) * 2 + 100;

            CallBackData returnData = new CallBackData(level, seconds);

            //계산 완료 및 일종의 리턴 호출
            EndEvent.Invoke(returnData);

            yield break;
        }

        /// <summary>
        /// 몬스터 체력이 레벨에 따라 증가
        /// </summary>
        IEnumerator MonsterHealthCoroutine(object myObject)
        {
            int level = (int)myObject;

            int seconds = (level * 5) * 2 + 30;

            CallBackData returnData = new CallBackData(level, seconds);

            //계산 완료 및 일종의 리턴 호출
            EndEvent.Invoke(returnData);

            yield break;
        }

        /// <summary>
        /// 몬스터 대미지 레벨에 따라 올라가는
        /// </summary>
        IEnumerator MonsterDamageCoroutine(object myObject)
        {
            int level = (int)myObject;

            int seconds = (level * 5) * 2 + 30;

            CallBackData returnData = new CallBackData(level, seconds);

            //계산 완료 및 일종의 리턴 호출
            EndEvent.Invoke(returnData);

            yield break;
        }

        #region [-PropertyType 계산 구현 => 카드 중첩도 같이 구현]
        /// <summary>
        /// 공격 스피드 계산
        /// 
        /// </summary>
        /// <param name="myObject"></param>
        IEnumerator ValueByCardStackCoroutine(object myObject)
        {
            CardCalcutionData cardCalcutionData = (CardCalcutionData)myObject;

            int returnValue = 0;

            //증감 퍼센트 계산
            int percentValue =
                (cardCalcutionData.DefalutIncreaseValue +//기본 퍼센트 값
                (cardCalcutionData.IncreaseValuePerLevel * cardCalcutionData.Level))//1 레벨당 증가 퍼센트
                * (cardCalcutionData.Stack+1);//중첩 스택 => +1더하는 이유는 중첩 0이 기본이라 

            //올림 계산
            returnValue = Mathf.CeilToInt(cardCalcutionData.Value * ((percentValue + 100) * 0.01f));

            Debug.Log("레벨 = ["+ cardCalcutionData.Level +
                "] 최종 percentValue = [" + percentValue +
                "] 1 스택당 증가 퍼센트  ["+((cardCalcutionData.DefalutIncreaseValue +(cardCalcutionData.IncreaseValuePerLevel * cardCalcutionData.Level))) +
                "] 기본 데미지 = [" + cardCalcutionData.Value + 
                "] 중첩에 의한 최종 데미지 = [" + returnValue + "]");

            CallBackData returnData = new CallBackData(cardCalcutionData.Level, returnValue);

            //계산 완료 및 일종의 리턴 호출
            EndEvent.Invoke(returnData);

            yield break;
        }
        #endregion


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

            //Debug.Log("[level : " + level + "]  seconds = " + seconds + " s  [" + secondsTimeset.TimeString + "]  " + "+ total = ["+ totalTimeset.TimeString + "]");
        }


        /// <summary>
        /// coin 결과 출력용으로 EndEvent에 추가해서 사용
        /// </summary>
        /// <param name="coin"></param>
        void CoinCallback(object callBackData)
        {
            //전체 시간을 구하기 위해 더해줌
            total += ((CallBackData)callBackData).Value;

            Debug.Log("[level : " + ((CallBackData)callBackData).Level + "] "+"[coin : " + ((CallBackData)callBackData).Value + "] "+ "total = [" + total + "]");
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
                //다음 연구 시간을 알아오기 위해 ResearchTime 이름 메서드 테스트
                //StartEvent.Invoke("ResearchTime", index);

                //다음 코인량을 알아오기위해 Coin 이름 메서드 테스트
                //StartEvent.Invoke("Coin", index);

                //AttackSpeed 카드 중첩에 테스트
                //1-25레벨 ,스택,기본 10 ,기본 증가 퍼센트 25%, 1 레벨당 2% 증가
                StartEvent.Invoke("ValueByCardStack", (object)new CardCalcutionData(index, 0, 5, 50, 2));

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
}
