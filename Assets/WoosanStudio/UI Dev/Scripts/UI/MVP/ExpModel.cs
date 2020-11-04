using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 경험치 모델
    /// 레벨과 현재 경험치를 저장한다.
    /// 레벨별 최대 경험치 값을 가지고 있다.
    /// *MVP 모델
    /// </summary>
    public class ExpModel : MonoBehaviour
    {
        [System.Serializable]
        public class Data
        {
            [Header("[현재 경험치]")]
            public int CurrentExp = 0;
            [Header("[현재 레벨]")]
            public int Level = 1;

            public Data() { }

            public void Print()
            {
                Debug.Log("레벨 = " + Level + "  경험치 = " + CurrentExp);
            }
        }

        public Data data = new Data();
        //레벨별 최대값 리스트
        public List<int> MaxExpList = new List<int>();

        public int MaxLevel = 25;

        //레벨별 최대 경험치 계산기
        public NextValueCalculator calculator;

        private UnityAction<object> callback;
        //시작 인덱스가 1이라서 
        //private int callbackCnt = 0;

        //확인용 
        //private int total;

        /// <summary>
        /// 최초 MaxExp를 가져오기 위해 한번 실행 필요
        /// </summary>
        public void Initialize()
        {
            //콜백 액션을 계산기 콜벨 이벤트에 넣는다
            //callback = GetMaxExpCallback;
            //calculator.EndEvent.AddListener(callback);
            //StartCoroutine(UpdateMexExpListCoroutine(MaxLevel));

            MaxExpList = NextValueCalculator.GetRequireExps(MaxLevel);

            Load();
        }

        /// <summary>
        /// 최대 경험치 계산 호출
        /// </summary>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        //IEnumerator UpdateMexExpListCoroutine(int maxLevel)
        //{
        //    for (int index = 0; index < maxLevel; index++)
        //    {
        //        calculator.StartEvent.Invoke("Exp", index);
        //        yield return new WaitForEndOfFrame();
        //    }
        //}

        //레벨별 최대 경험치 결과 콜백
        //void GetMaxExpCallback(object callBackData)
        //{
        //    //전체 시간을 구하기 위해 더해줌
        //    //전체 시간을 구하기 위해 더해줌
        //    total += (int)((NextValueCalculator.CallBackData)callBackData).Value;

        //    //최대 경험치 세팅
        //    MaxExpList.Add((int)((NextValueCalculator.CallBackData)callBackData).Value);

        //    //Debug.Log("[level : " + ((NextValueCalculator.CallBackData)callBackData).Level + "] " + "[current value : " + ((NextValueCalculator.CallBackData)callBackData).Value + "] " + "total = [" + total + "]");

        //    //콜백의 마지막을 알기위해 카운팅
        //    callbackCnt++;
        //    //콜백의 마지막이며 리스너 제거
        //    if(callbackCnt == MaxLevel) { calculator.EndEvent.RemoveListener(GetMaxExpCallback); }
        //}

        /// <summary>
        /// 데이터 로드
        /// </summary>
        public void Load()
        {
            if (!PlayerPrefs.HasKey("Exp")) { Save(); }
            data = null;
            data = JsonUtility.FromJson<ExpModel.Data>(PlayerPrefs.GetString("Exp"));

            Debug.Log("Exp 로드 완료");
            //data.Print();
        }

        /// <summary>
        /// 데이터 저장
        /// </summary>
        public void Save()
        {
            PlayerPrefs.SetString("Exp", JsonUtility.ToJson(data));

            Debug.Log("Exp 저장 완료");
            //data.Print();
        }

        /*
        #region [-TestCode]
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Load();
            }
        }
        #endregion
        */
    }
}
