using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 에너지 정보를 요청 받음
    /// Model 에서 정보를 얻어와 필요한 곳에 전달
    /// MVC 패
    /// </summary>
    public class EnergyPresenter : MonoBehaviour
    {
        [Header("[에너지 모델]")]
        public EnergyModel Model;
        public string RechargeInfoText { get; set; }
        public string ProgressText { get; set; }
        public string RechargeTimeText { get; set; }
        public float EnergySliderValue { get; set; }

        //캐쉬용
        private WaitForSeconds WFS = new WaitForSeconds(0.5f);
        private StringBuilder stringBuilder = new StringBuilder();
        private string[] Token = { "[", "/", "s]", "00:", "MAX" };
        private int max = 0;
        private int current = 0;
        private bool isFull = false;

        private IEnumerator Start()
        {
            //0.5s간격으로 에너지 정보 가져옴
            while (true)
            {
                //충전 시간 업데이트
                UpdageProgress();
                //충전 정보 업데이트
                UpdageRechargeTime();
                //충전 진행 업데이트
                UpdageRechargeInfo();
                
                yield return WFS;
            }
        }

        /// <summary>
        /// 충전 정보 보여주기
        /// ex)[1/45s]
        /// </summary>
        void UpdageRechargeInfo()
        {
            stringBuilder.Append(Token[0]);
            stringBuilder.Append(Model.GetData().RechargingPoint);
            stringBuilder.Append(Token[1]);
            stringBuilder.Append(Model.GetData().MaxRechargingTime);
            stringBuilder.Append(Token[2]);

            RechargeInfoText = stringBuilder.ToString();

            stringBuilder.Clear();
        }

        /// <summary>
        /// 충전 진행 상황 보여주기
        /// </summary>
        void UpdageProgress()
        {
            int max = Model.GetData().MaxEnergy;
            int current = Model.GetData().CurrentEnergy;
            //0-1로 구하기
            float value = (float)current / (float)max;

            //current 와 max 가 같아도 0이나와서 1로 강제 조정 
            if (current == max) { value = 1; isFull = true; }
            else { isFull = false; }

            //Debug.Log("current = " + current + ": max = " + max + " : value = " + value);

            EnergySliderValue = value;

            stringBuilder.Append(current);
            stringBuilder.Append(Token[1]);
            stringBuilder.Append(max);

            ProgressText = stringBuilder.ToString();

            stringBuilder.Clear();
        }

        /// <summary>
        /// 충전 시간 보여주기
        /// </summary>
        void UpdageRechargeTime()
        {
            //가득 찾다면 충전시간이 안닌Full 단어 보여주기
            if (isFull) {
                RechargeTimeText = Token[4];
                stringBuilder.Clear();
                return;
            }

            //남은 출력시간 string으로 만들기
            //* 분이 추가될 경우 수정 해야함. 현재는 최대 59초까지만 동작 가능
            stringBuilder.Append(Token[3]);
            stringBuilder.Append(string.Format("{0,2:00}", Model.GetData().RemainRechargingTime));

            RechargeTimeText = stringBuilder.ToString();

            stringBuilder.Clear();
        }

        /// <summary>
        /// 외부에서 호출되는 에너지 소비
        /// </summary>
        /// <param name="value"></param>
        public void UpdateEnergy(int value)
        {
            Model.UpdateEnergy(value);
        }

        
        #region [-TestCode]
        void Update()
        {
            //에너지 소비
            if (Input.GetKeyDown(KeyCode.A))
            {
                UpdateEnergy(-25);
            }

            //현재 데이터 출력
            if (Input.GetKeyDown(KeyCode.S))
            {
                UpdateEnergy(+25);
            }

            //현재 데이터 출력
            if (Input.GetKeyDown(KeyCode.D))
            {
                Model.Load();
            }

            //현재 데이터 출력
            if (Input.GetKeyDown(KeyCode.F))
            {
                PlayerPrefs.DeleteAll();
            }
        }
        #endregion
    }
}
