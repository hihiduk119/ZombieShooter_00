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
    ///
    /// *이벤트를 통해서 호출하며
    /// 호출전에 EndEvent에 액션 등록 필요
    /// 공식 사용이 끝났다면 EndEvent 리스너에서 제거 필요
    /// </summary>
    public class NextValueCalculator : MonoBehaviour
    {
        //[Header("[테스트를 위해 사용하는 임시 라운드 => 테스트 후 삭제 필요]")]
        public int TestRound;
        //[Header("[테스트를 위해 사용하는 임시 플레이어 레벨 => 테스트 후 삭제 필요]")]
        public int TestPlayerLevel;


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
            public float Value;
            //기본 퍼센트 값
            public int DefalutIncreaseValue;
            //레벨 1당 증가 퍼센트
            public int IncreaseValuePerLevel;

            //생성자
            public CardCalcutionData(int level, int stack, float value, int defalutIncreaseValue, int increaseValuePerLevel)
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
            public float Value;

            public CallBackData(int level, float value)
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
            //*제거필요
            //*공식이 추가될때 마다 등록 시켜야 함.
            StartEvent.AddListener(RunFormula);

            //*리스너에 2개 동시에 있을경우 Total 이 중첩되어 적용됨 알아둬야함.
            //디버그용 리스너 등록 => TestCode 삭제시 삭제 요망.
            //EndEvent.AddListener(ResearchTimeCallback);
            //디버그용 리스너 등록 => TestCode 삭제시 삭제 요망.
            //EndEvent.AddListener(CoinCallback);
        }

        /// <summary>
        /// 최종 코인 아이템의 값을 가져옴
        /// *Exp와 동일
        /// </summary>
        /// <param name="mapSetting">해당 되는 맵</param>
        /// <param name="round">몇 라운드</param>
        /// <param name="cardSettings">카드들</param>
        public int GetItemCoinValue(int round,int playerLevel,List<CardSetting> cardSettings)
        {
            CardProperty property;
            //라운드와 레벨에 의해 계산된 기본 코인 값
            float defaultCoinValue;
            //카드 스택
            int stack = 0;
            //증가 퍼센트
            float percent;
            //반환 값
            int returnValue = 0;

            //라운드와 레벨에 의해 계산된 기본 코인 값 가져오기
            defaultCoinValue = GetDefalutItemCoinValue(round, playerLevel);

            //Debug.Log("현재 기본 코인 값 = [" +  defaultCoinValue + "] 라운드 = [" + round+"] 플레이어 레벨 = [" + playerLevel +"]" );

            //Debug.Log("defaultCoinValue = " + defaultCoinValue);
            //코인증가 카드 없을수도 있어서 
            returnValue = Mathf.CeilToInt(defaultCoinValue);

            //Debug.Log("0 returnValue = " + returnValue);

            //코인증가 카드 찾기
            //모든 카드 검색 
            for (int i = 0; i < cardSettings.Count; i++)
            {
                //카드의 모든 프로퍼티 검색
                for (int j = 0; j < cardSettings[i].Properties.Count; j++)
                {
                    //코인 값 증가 프로퍼티 찾음
                    property = cardSettings[i].Properties[j];
                    stack = cardSettings[i].StackCount;

                    if (property.Type == CardProperty.PropertyType.Coin)
                    {
                        //증가 퍼센트 계산
                        percent = (((property.IncreasedValuePerLevelUp * cardSettings[i].Level) + property.Value) * stack + 100f) * 0.01f;

                        //Debug.Log(percent + " = (((" + property.IncreasedValuePerLevelUp +
                        //    " * " + cardSettings[i].Level + ") + " + property.Value +") * " + stack + " + 100f) * 0.01f");
                        //Debug.Log("Total Rate = [" + percent + "] card level = [" + cardSettings[i].Level + "] Stack = [" + stack + "]  card = [" + cardSettings[i].Type.ToString() + "] type = [" + property.Type.ToString() + "]");
                        //올림 계산
                        returnValue = Mathf.CeilToInt(defaultCoinValue * percent);
                    }
                }
            }

            Debug.Log("생성 코인 값 = " + returnValue);
            return returnValue;
        }

        /// <summary>
        /// 기본이 되는 아이템 코인 값 가져오기
        /// *카드에 의한 증가분은 계산 되지 않음
        /// </summary>
        /// <param name="mapSetting"></param>
        /// <param name="round"></param>
        /// <param name="playerLevel"></param>
        /// <param name="cardSettings"></param>
        float GetDefalutItemCoinValue(int round, int playerLevel)
        {
            float roundValue;
            float playerlevelValue;
            float factorValue = 5;
            //최소 값
            float baseValue = 2;
            float value;

            //맵과 라운드에 의한 코인 상승 구함
            roundValue =  factorValue * round * 0.75f;
            //플레이어 레벨에 의한 코인 상승 구함
            playerlevelValue = factorValue * playerLevel * 0.25f;
            //최소 값에 맵&라운드 + 플레이어 가치 더함
            value = baseValue + roundValue + playerlevelValue;

            return value;
        }


        /// <summary>
        /// 최종 Exp 아이템의 값을 가져옴
        /// *Coin과 동일
        /// </summary>
        /// <param name="mapSetting">해당 되는 맵</param>
        /// <param name="round">몇 라운드</param>
        /// <param name="cardSettings">카드들</param>
        public int GetItemExpValue(int round, int playerLevel, List<CardSetting> cardSettings)
        {
            CardProperty property;
            //라운드와 레벨에 의해 계산된 기본 코인 값
            float defaultCoinValue;
            //카드 스택
            int stack = 0;
            //증가 퍼센트
            float percent;
            //반환 값
            int returnValue = 0;

            //라운드와 레벨에 의해 계산된 기본 코인 값 가져오기
            defaultCoinValue = GetDefalutItemExpValue(round, playerLevel);

            //Debug.Log("현재 기본 코인 값 = [" + defaultCoinValue + "] 라운드 = [" + round + "] 플레이어 레벨 = [" + playerLevel + "]");

            //Debug.Log("defaultCoinValue = " + defaultCoinValue);
            //코인증가 카드 없을수도 있어서 
            returnValue = Mathf.CeilToInt(defaultCoinValue);

            //Debug.Log("0 returnValue = " + returnValue);

            //코인증가 카드 찾기
            //모든 카드 검색 
            for (int i = 0; i < cardSettings.Count; i++)
            {
                //카드의 모든 프로퍼티 검색
                for (int j = 0; j < cardSettings[i].Properties.Count; j++)
                {
                    //코인 값 증가 프로퍼티 찾음
                    property = cardSettings[i].Properties[j];
                    stack = cardSettings[i].StackCount;

                    if (property.Type == CardProperty.PropertyType.Exp)
                    {
                        //증가 퍼센트 계산
                        percent = (((property.IncreasedValuePerLevelUp * cardSettings[i].Level) + property.Value) * stack + 100f) * 0.01f;

                        //Debug.Log(percent + " = (((" + property.IncreasedValuePerLevelUp +
                        //    " * " + cardSettings[i].Level + ") + " + property.Value + ") * " + stack + " + 100f) * 0.01f");
                        //Debug.Log("Total Rate = [" + percent + "] card level = [" + cardSettings[i].Level + "] Stack = [" + stack + "]  card = [" + cardSettings[i].Type.ToString() + "] type = [" + property.Type.ToString() + "]");
                        //올림 계산
                        returnValue = Mathf.CeilToInt(defaultCoinValue * percent);
                    }
                }
            }

            Debug.Log("생성 Exp 값 = " + returnValue);
            return returnValue;
        }


        /// <summary>
        /// 기본이 되는 아이템 EXP 값 가져오기
        /// *카드에 의한 증가분은 계산 되지 않음
        /// </summary>
        /// <param name="mapSetting"></param>
        /// <param name="round"></param>
        /// <param name="playerLevel"></param>
        /// <param name="cardSettings"></param>
        float GetDefalutItemExpValue(int round, int playerLevel)
        {
            float roundValue;
            float playerlevelValue;
            float factorValue = 5;
            //최소 값
            float baseValue = 2;
            float value;

            //맵과 라운드에 의한 코인 상승 구함
            roundValue = factorValue * round * 0.75f;
            //플레이어 레벨에 의한 코인 상승 구함
            playerlevelValue = factorValue * playerLevel * 0.25f;
            //최소 값에 맵&라운드 + 플레이어 가치 더함
            value = baseValue + roundValue + playerlevelValue;

            return value;
        }

        /// <summary>
        /// *제거필요
        /// 계산을 실행하는 부분 이며 임의로 메서드를 변경 할수 있음
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="level"></param>
        void RunFormula(string methodName, object myObject)
        {

            //Invoke(methodName, 0);
            //이전 코루틴이 있으면 제거
            //*이벤트 처리가 하나라서 이렇게 마듬
            if (formulaCoroutine != null) { StopCoroutine(formulaCoroutine); }
            //코루틴으로 해당 메서드 실행
            formulaCoroutine = StartCoroutine(methodName+"Coroutine", myObject);
        }

        /// <summary>
        /// *제거필요
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
        /// *제거필요
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
        /// 겜블시 젬 비용
        /// </summary>
        /// <returns></returns>
        static public int GetGambleGem()
        {
            return 10;
        }


        /// <summary>
        /// 스테이지 결과 랭크 등급 기준표
        /// *4가지 변수로 별3,별2 클리어 시간 표시
        /// </summary>
        /// <param name="cardLevelAverage">예상되는 모든 카드 평균 레벨</param>
        /// <param name="monsterCount">등장 몬스터 수</param>
        /// <param name="roundCount">라운드 수</param>
        /// <param name="bossCount">등장 보스 수</param>
        /// <returns></returns>
        static public int[] GetAllRankTable(int cardLevelAverage,int monsterCount,int roundCount,int bossCount)
        {

            return new int[]{ 0, 0 };
        }

        /// <summary>
        /// 겜블시 젬 비용
        /// </summary>
        /// <returns></returns>
        static public int GetGambleSuccessRate(int successCount)
        {
            int defalut = GlobalDataController.GambleDefaultRate;
            int max = GlobalDataController.GambleMaxRate;
            int interval = GlobalDataController.GamblePerValue;

            //50시작 성공 카운트 마다 5씩 증가
            //80이상 초과 불가.
            int value = defalut + (successCount * interval);
            if (value >= max) { value = max; }

            return value;
        }


        /// <summary>
        /// 스킬(능력,카드) 레벨이 증가함에 따라 증가하는 업그레이드 시간
        /// </summary>
        /// <param name="MaxLevel"></param>
        /// <returns></returns>
        static public List<int> GetUpgradeTimes(int MaxLevel)
        {
            //지역변수 선언
            List<int> valueList = new List<int>();
            int value = 0;

            for (int level = 0; level < MaxLevel; level++)
            {
                //공식 적용
                value = (level * level * level * 30) * 2 + 30;
                //리스트에 넣기
                valueList.Add(value);
            }

            return valueList;
        }

        /// <summary>
        /// 스킬(능력,카드) 레벨이 증가함에 따라 증가하는 업그레이드 시간
        /// </summary>
        /// <param name="MaxLevel"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        static public int GetUpgradeTimeByLevel(int MaxLevel, int level)
        {
            List<int> tmp = NextValueCalculator.GetUpgradeTimes(MaxLevel);

            //테스트용 내용 확인코드
            //for (int i = 0; i < MaxLevel; i++)
            //{
            //    Debug.Log("level = [" + i + "] [" + tmp[i] + "]");
            //}
            //return tmp[level];
            return NextValueCalculator.GetUpgradeTimes(MaxLevel)[level];
        }


        /// <summary>
        /// 연구 젬 비용 증가 공식 레벨에 따라 올라가는
        /// </summary>
        /// <param name="MaxLevel"></param>
        /// <returns></returns>
        static public List<int> GetRequireGems(int MaxLevel)
        {
            //지역변수 선언
            List<int> valueList = new List<int>();
            int value = 0;

            for (int level = 0; level < MaxLevel; level++)
            {
                //공식 적용
                value = (level * level * 10) * 2 + 50;
                //리스트에 넣기
                valueList.Add(value);
            }

            return valueList;
        }

        /// <summary>
        /// 필요 젬 비용 레벨로 가져오기 
        /// </summary>
        /// <param name="MaxLevel"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        static public int GetRequireGemByLevel(int MaxLevel, int level)
        {
            return NextValueCalculator.GetRequireGems(MaxLevel)[level];
        }


        /// <summary>
        /// 연구 비용 증가 공식 레벨에 따라 올라가는
        /// </summary>
        /// <param name="MaxLevel"></param>
        /// <returns></returns>
        static public List<int> GetRequireCoins(int MaxLevel)
        {
            //지역변수 선언
            List<int> valueList = new List<int>();
            int value = 0;

            for (int level = 0; level < MaxLevel; level++)
            {
                //공식 적용
                value = (level * level * level * 5) * 2 + 100;
                //리스트에 넣기
                valueList.Add(value);
            }

            return valueList;
        }

        /// <summary>
        /// 필요 연구 비용 레벨로 가져오기 
        /// </summary>
        /// <param name="MaxLevel"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        static public int GetRequireCoinByLevel(int MaxLevel,int level)
        {
            return NextValueCalculator.GetRequireCoins(MaxLevel)[level];
        }

        /// <summary>
        /// 필요 경험치 리스트 가져오기
        /// </summary>
        /// <param name="MaxLevel"></param>
        /// <returns></returns>
        static public List<int> GetRequireExps(int MaxLevel)
        {
            //지역변수 선언
            List<int> valueList = new List<int>();
            int value = 0;

            for (int level = 0; level < MaxLevel; level++)
            {
                //공식 적용
                value = (level * level * level * 5) * 2 + 300;
                //리스트에 넣기
                valueList.Add(value);
            }

            return valueList;
        }

        /// <summary>
        /// 필요 연구 비용 레벨로 가져오기 
        /// </summary>
        /// <param name="MaxLevel"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        static public int GetRequireExpByLevel(int MaxLevel, int level)
        {
            return NextValueCalculator.GetRequireExps(MaxLevel)[level];
        }

        /// <summary>
        /// *코루틴 제거로 변경 필요
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
        /// *코루틴 제거로 변경 필요
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
        /// *코루틴 제거로 변경 필요
        /// 크리티컬 찬스,탄약,총,탄약 데미지 계산
        /// 매 레벨업시 상승 값 제공
        /// </summary>
        /// <param name="myObject"></param>
        IEnumerator UpValueByCardStackCoroutine(object myObject)
        {
            CardCalcutionData cardCalcutionData = (CardCalcutionData)myObject;

            float returnValue = 0;

            //증감 퍼센트 계산
            int percentValue =
                (cardCalcutionData.DefalutIncreaseValue +//기본 퍼센트 값
                (cardCalcutionData.IncreaseValuePerLevel * cardCalcutionData.Level))//1 레벨당 증가 퍼센트
                * (cardCalcutionData.Stack+1);//중첩 스택 => +1더하는 이유는 중첩 0이 기본이라 

            //올림 계산
            //returnValue = Mathf.CeilToInt(cardCalcutionData.Value * ((percentValue + 100) * 0.01f));
            //올림 계산 사용 안함
            returnValue = cardCalcutionData.Value * ((percentValue + 100) * 0.01f);

            Debug.Log("레벨 = ["+ cardCalcutionData.Level +
                "] 최종 percentValue = [" + percentValue +
                "] 스택 1 당 증가 퍼센트  ["+((cardCalcutionData.DefalutIncreaseValue +(cardCalcutionData.IncreaseValuePerLevel * cardCalcutionData.Level))) +
                "] 기본 데미지 = [" + cardCalcutionData.Value + 
                "] 중첩에 의한 최종 데미지 = [" + returnValue + "]");

            CallBackData returnData = new CallBackData(cardCalcutionData.Level, returnValue);

            //계산 완료 및 일종의 리턴 호출
            EndEvent.Invoke(returnData);

            yield break;
        }


        /// <summary>
        /// *코루틴 제거로 변경 필요
        /// 공격 속도 계산
        /// * 공격속도는 1을 기준으로 점점 줄어야 한다.
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        IEnumerator DownValueByCardStackCoroutine(object myObject)
        {
            CardCalcutionData cardCalcutionData = (CardCalcutionData)myObject;

            float returnValue = 0;

            //증감 퍼센트 계산
            int percentValue =
                (cardCalcutionData.DefalutIncreaseValue +//기본 퍼센트 값
                (cardCalcutionData.IncreaseValuePerLevel * cardCalcutionData.Level))//1 레벨당 증가 퍼센트
                * (cardCalcutionData.Stack + 1);//중첩 스택 => +1더하는 이유는 중첩 0이 기본이라 

            //올림 계산
            //returnValue = Mathf.CeilToInt(cardCalcutionData.Value * ((percentValue + 100) * 0.01f));
            //올림 계산 사용 안함
            returnValue = cardCalcutionData.Value / ((percentValue + 100) * 0.01f);

            Debug.Log("레벨 = [" + cardCalcutionData.Level +
                "] 최종 percentValue = [" + percentValue +
                "] 스택 1 당 증가 퍼센트  [" + ((cardCalcutionData.DefalutIncreaseValue + (cardCalcutionData.IncreaseValuePerLevel * cardCalcutionData.Level))) +
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
        private float total = 0;

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

            Timeset totalTimeset = new Timeset( Mathf.CeilToInt( total));
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

            Debug.Log("[level : " + ((CallBackData)callBackData).Level + "] "+"[current value : " + ((CallBackData)callBackData).Value + "] "+ "total = [" + total + "]");
        }

        /// <summary>
        /// 25레벨 까지 순차 실행하기 위해 만든 임시 메서드
        /// 4중첩 실행
        /// *테스트 용임
        /// </summary>
        /// <returns></returns>
        IEnumerator SequenceRun()
        {
            int defaultIncreaseValue = 50;  //최초 증가 퍼센트
            int increaseValuePerLevel = 2;  //1레벨당 증가 퍼센트
            float initValue = 0.8f;              //기준이 되는 데미지

            int maxStack = 4;               //최대 줓업

            Debug.Log("===================[테스트 시작]===================");
            //레벨 25까지 실행
            for (int index = 0; index < 25; index++)
            {
                #region [-다음 연구 시간을 알아오기 위해 ResearchTime 이름 메서드 테스트]
                //StartEvent.Invoke("ResearchTime", index);
                #endregion

                #region [-다음 코인량을 알아오기위해 Coin 이름 메서드 테스트]
                //StartEvent.Invoke("Coin", index);
                #endregion


                #region [-AttackSpeed 카드 중첩에 테스트]

                //StartEvent.Invoke("UpValueByCardStack", (object)new CardCalcutionData(index, 0, initValue, defaultIncreaseValue, increaseValuePerLevel));

                Debug.Log("===================[" + index + " 레벨 " + maxStack + " 중첩]===================");

                //StartEvent.Invoke("Exp", index);

                //1레벨 0-4중첩 계산
                for (int stack = 0; stack < maxStack; stack++)
                {
                    //n레벨 ,n스택,기본 값 5 ,기본 증가 퍼센트 50%, 1 레벨당 2% 증가
                    //StartEvent.Invoke("UpValueByCardStack", (object)new CardCalcutionData(index, stack, initValue, defaultIncreaseValue, increaseValuePerLevel));
                    //n레벨 ,n스택,기본 값 5 ,기본 증가 퍼센트 50%, 1 레벨당 2% 증가
                    StartEvent.Invoke("DownValueByCardStack", (object)new CardCalcutionData(index, stack, initValue, defaultIncreaseValue, increaseValuePerLevel));

                    yield return new WaitForEndOfFrame();
                }

                //Debug.Log("===========================================");
                #endregion
                //yield return new WaitForEndOfFrame();
            }
            Debug.Log("===================[테스트 끝]===================");
        }

        void Update()
        {
            /*
            //맵과 라운드 플레이어 레벨에 의한 코인 값 측정
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                float value = GetDefalutItemCoinValue(TestRound, TestPlayerLevel);
                Debug.Log( "결과 = ["+value+"] 플레이어 레벨 = [" + TestPlayerLevel + "] 라운드 = [" + TestRound + "]");
            }

            //라운드 증가
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                TestRound++;
                Debug.Log("라운드 증가 = ["+ TestRound + "]");
            }

            //플레이어 레벨 증가
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                TestPlayerLevel++;
                Debug.Log("라운드 증가 = [" + TestPlayerLevel + "]");
            }
            */

            //라운드 증가
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                //[0,1,2]  [0 = 라운드, 1 = 플레이어 래벨, 2 = 모든 카드 데이터]
               float value  = GetItemCoinValue(this.TestRound, this.TestPlayerLevel,GlobalDataController.Instance.SelectAbleAllCard);
                Debug.Log("최종 값 = " + value);
            }
        }
        #endregion

    }
}
