using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 에너지 관련 정보 세이브, 로드
    /// MVC 패턴
    /// </summary>
    public class EnergyModel : MonoBehaviour
    {

        [Serializable]
        public class Data
        {
            //최대 에너지
            //로드해서 가져옴
            public int MaxEnergy = 100;

            //현재 에너지
            //로드해서 가져옴
            public int CurrentEnergy = 0;

            //회복 하는 시간
            //로드해서 가져옴
            public int MaxRechargingTime = 45;

            //회복 하는 시간
            //로드해서 가져옴
            public int RemainRechargingTime = 0;

            //한번에 회복하는 수치
            //로드해서 가져옴
            public int RechargingPoint = 1;

            //시간 저장용 바이너리
            public long dateTimeByBinary;

            public Data(){}

            //내부 데이터 출력
            public void Print()
            {
                Debug.Log(">>> 에너지 = [" + CurrentEnergy +
                    "/" + MaxEnergy +
                    "]  충전시간 = [" + RemainRechargingTime +
                    "/" + MaxRechargingTime +
                    "]  1당 회복포인트 = " + RechargingPoint
                    );
            }
        }

        [SerializeField]
        private Data data = new Data();
        public Data GetData() => data;

        //캐쉬용
        private WaitForEndOfFrame WFF = new WaitForEndOfFrame();
        private float deltaTime;
        

        /// <summary>
        /// 생성시 자동으로 돌며 에너지 업데이트
        /// 0.5s로 업데이트
        /// </summary>
        /// <returns></returns>
        IEnumerator Start()
        {
            //플레이어 데이터 로드
            Load();

            deltaTime = 0;

            while (true)
            {
                deltaTime += Time.deltaTime;

                //Debug.Log(deltaTime);
                if(1f <= deltaTime)
                {
                    deltaTime -= 1f;
                    //남은 시간 1 줄이기
                    data.RemainRechargingTime -= 1;

                    //0이되면 45로 초기화
                    if(data.RemainRechargingTime < 0){
                        //최대 충전 시간 남은시간에 넣기
                        data.RemainRechargingTime = data.MaxRechargingTime;

                        //업데이트
                        UpdateEnergy(data.RechargingPoint);
                    }
                }
                
                yield return WFF;
            }
        }

        /// <summary>
        /// 에너지 값의 변화 적용
        /// </summary>
        /// <param name="value"></param>
        public void UpdateEnergy(int value)
        {
            //현재 에너지 증가
            data.CurrentEnergy += value;

            //최대 에너지 보다 클수 없다
            if (data.MaxEnergy < data.CurrentEnergy) { data.CurrentEnergy = data.MaxEnergy; }

            //0보다 작을수 없다
            if (data.CurrentEnergy < 0) { data.CurrentEnergy = 0; }
                
            //업데이트 정보 저장 필요
            Save();
        }

        /// <summary>
        /// 최소 실행시 플레이어 이전 데이터를 로드해서 실행
        /// </summary>
        private void Load()
        {
            //첫 시작이라면 세이브 부터
            if(!PlayerPrefs.HasKey("Energy")){ Save(); Debug.Log("처음이야!!"); }

            string energyByJson = PlayerPrefs.GetString("Energy");

            //테스트 출력
            //data.Print();

            EnergyModel.Data loadData = JsonUtility.FromJson<EnergyModel.Data>(energyByJson);

            data.MaxRechargingTime = loadData.MaxRechargingTime;
            data.RechargingPoint = loadData.RechargingPoint;
            data.MaxEnergy = loadData.MaxEnergy;

            //지난 시간 가져오기
            TimeSpan timeSpan = DateTime.Now.Subtract(DateTime.FromBinary(loadData.dateTimeByBinary));

            double seconds = timeSpan.TotalSeconds;
            //Debug.Log("seconds = " + seconds);

            double addEnergy = seconds / data.MaxRechargingTime;

            //Debug.Log("addEnergy = " + addEnergy + " seconds  = " + seconds + " MaxRechargingTime = " + data.MaxRechargingTime);

            double addRechargingTime = seconds % data.MaxRechargingTime;

            //Debug.Log("addRechargingTime = " + addRechargingTime + " seconds  = " + seconds + " MaxRechargingTime = " + data.MaxRechargingTime);

            //Debug.Log("지난 시간 = " + timeSpan.ToString() + "   에너지 = " + loadData.CurrentEnergy + "/" + addEnergy);

            //에너지 추가
            //에너지 추가
            UpdateEnergy(loadData.CurrentEnergy + Convert.ToInt32(addEnergy));

            //충전 현재 시간 감소
            data.RemainRechargingTime = data.MaxRechargingTime - Convert.ToInt32(addRechargingTime);

        }

        /// <summary>
        /// 에너지 변화 있을시 저장
        /// </summary>
        private void Save()
        {
            //세이브 시간 저장
            data.dateTimeByBinary = DateTime.Now.ToBinary();

            //제이슨 변환
            string energyByJson  = JsonUtility.ToJson(data);

            //테스트 출력
            data.Print();

            //제이슨 저장
            PlayerPrefs.SetString("Energy", energyByJson);
        }
    }
}
