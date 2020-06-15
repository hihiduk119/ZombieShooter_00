using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using System.Timers;
using System;
using System.Text;

namespace WoosanStudio.ZombieShooter
{
    public class Timeset
    {
        DateTime myDateTime;
        //전체 몇초인지 구해놓기
        int totalSecond = 0;
        
        //스트링 메모리 절약용 스트링 빌더
        StringBuilder stringBuilder = new StringBuilder();

        //계산된 시간을 외부에서 가져올때 쓰는 변수
        private String timeString;
        public String TimeString { get => this.timeString; }

        String tmp;
        string[] timeSpan;


        /// <summary>
        /// 생성자 생성시 시간 분단위로 넣음
        /// </summary>
        /// <param name="minutes"></param>
        public Timeset(int minutes)
        {
            Make(minutes);
        }

        /// <summary>
        /// 생성시 셋업 
        /// </summary>
        /// <param name="minutes"></param>
        public void Make(int minutes)
        {
            //Debug.Log("지금 시간 = " + DateTime.Now.ToString("yyyy년 MM월 dd일 HH시 mm분 ss초"));
            myDateTime = DateTime.Now;
            myDateTime = myDateTime.AddMinutes(minutes);
            //전체 몇초인지 구해 놓기
            totalSecond = minutes * 60;
        }

        /// <summary>
        /// 남은시간을 timespan으로 반환
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetRemainTime()
        {
            TimeSpan timeSpan = myDateTime.Subtract(DateTime.Now);
            TimeToString(timeSpan);
            //Debug.Log("남은시간 = " + timeString);
            return timeSpan;
        }

        /// <summary>
        /// 남은시간은 0-1사이 값으로 반환
        /// </summary>
        public float GetRemainValue()
        {
            TimeSpan timeSpan = GetRemainTime();

            //int currentSec = timeSpan.TotalSeconds;

            float value = (float)(Math.Floor(timeSpan.TotalSeconds) / totalSecond);
            value = 1 - value;
            //Debug.Log("currentSec = " + totalSecond + "   remainSec = " + Math.Floor(timeSpan.TotalSeconds) + "  value = " + value);
            return value;
        }

        public String TimeToString(TimeSpan time)
        {
            tmp = time.ToString();
            timeSpan = tmp.Split('.');
            stringBuilder.Clear();

            //Day 없고 시간만 존제
            if (timeSpan.Length == 2)
            {
                stringBuilder.Append(timeSpan[0]);
            } else if(timeSpan.Length == 3)
            {
                stringBuilder.Append(timeSpan[0]);
                stringBuilder.Append("d ");
                stringBuilder.Append(timeSpan[1]);
            } else
            {
                Debug.Log("시간 파싱 에러 발생!!" + time.ToString());
                stringBuilder.Append("E 00:00:00");
            }

            //for(int index = 0; index < timeSpan.Length ;index++)
            //{
            //    Debug.Log(timeSpan[index]);
            //}

            //Debug.Log("원본 = " + time.ToString());
            timeString = stringBuilder.ToString();
            return timeString;
        }
    }
}