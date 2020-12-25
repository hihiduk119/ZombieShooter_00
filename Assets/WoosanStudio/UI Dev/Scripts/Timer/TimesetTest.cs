using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace WoosanStudio.ZombieShooter
{
    public class TimesetTest : MonoBehaviour
    {
        Timeset timeset;

        //끝나는 시간
        public long endDateTime;
        //시작했던 시간
        public long startDateTime;

        private void Start()
        {
            //LoadTime();
        }

        //void MakeTime()
        //{
        //    //120초생성
        //    timeset = new Timeset(2000000);
        //}

        /// <summary>
        /// 시간을 저장하면 한번 저장하면 이후 값만 가져오기만 하면됨
        /// </summary>
        /// <param name="seconds"></param>
        void SaveTime(int seconds)
        {
            //미래시간
            TimeSpan nextSpan = DateTime.Now.AddSeconds(seconds).TimeOfDay;
            //현재 시간
            TimeSpan nowSpan = DateTime.Now.TimeOfDay;

            //현재에서 미래뺌
            nowSpan = nowSpan.Subtract(nextSpan);

            //변경 시간 나옴 -> Score로 사용
            Debug.Log("total = " + nowSpan.TotalSeconds);

            //현재시간 과 지난 시간 계산 -> 초(s) 계산
            //해당 시간이 스코아

            //DateTime.FromBinary(endDateTime).Se;
        }

        //#region [-TestCode]
        //void Update()
        //{
        //    //업그레이드 연구 시간 저장 테스트
        //    if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        SaveTime(180);
        //    }
        //    if (Input.GetKeyDown(KeyCode.S))
        //    {

        //    }
        //}
        //#endregion
    }
}
