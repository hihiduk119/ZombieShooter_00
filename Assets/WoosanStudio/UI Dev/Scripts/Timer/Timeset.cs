using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using System.Timers;
using System;
using System.Text;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 시간 데이터변환 클래스
    /// 사용법
    /// -> Make 또는 생성자에 시간 넣기. 생성시간이 셋업됨.
    /// -> GetRemainTime() 호출하여 셋업된 시간으로부터 남은 시간 가져오기.
    /// </summary>
    public class Timeset
    {
        DateTime myDateTime;
        //전체 몇초인지 구해놓기
        int totalSeconds = 0;
        
        //스트링 메모리 절약용 스트링 빌더
        StringBuilder stringBuilder = new StringBuilder();

        //계산된 시간을 외부에서 가져올때 쓰는 변수
        private String timeString;
        public String TimeString { get => this.timeString; }

        String tmp;
        string[] timeSpan;

        /// <summary>
        /// 현재 셋업된 데이터 타임을 가져옴
        /// </summary>
        /// <returns></returns>
        public DateTime GetDateTime()
        {
            //JsonUtility.ToJson(myDateTime.ToBinary());
            return myDateTime;
        }

        /// <summary>
        /// 현재 셋업된 데이터 타임을 바이너리로 가져옴
        /// -> Jason 세이브용 
        /// </summary>
        /// <returns>바이너리데이터</returns>
        public long GetDateTimeByBinary()
        {
            return myDateTime.ToBinary();
        }

        /// <summary>
        /// 현재 셋업된 데이터 타임을 가져옴
        /// 세이브 로드시 사용
        /// </summary>
        /// <returns></returns>
        public void SetDateTime(DateTime dateTime)
        {
            myDateTime = dateTime;
        }

        /// <summary>
        /// 현재 셋업된 데이터 타임을 가져옴
        /// -> 제이슨 로드용 
        /// </summary>
        /// <returns></returns>
        public void SetDateTimeByBinary(long dateTimeValue)
        {
            myDateTime = DateTime.FromBinary(dateTimeValue);
        }

        /// <summary>
        /// 현재 시간 기준으로 셋업 생성자
        /// </summary>
        /// <param name="minutes">분단위로 셋업</param>
        public Timeset(int Seconds)
        {
            Make(Seconds);
        }

        /// <summary>
        /// 현재 시간 기준으로 셋업 
        /// </summary>
        /// <param name="minutes"></param>
        public void Make(int seconds)
        {
            //Debug.Log("지금 시간 = " + DateTime.Now.ToString("yyyy년 MM월 dd일 HH시 mm분 ss초"));
            myDateTime = DateTime.Now;
            //myDateTime = myDateTime.AddMinutes(minutes);
            myDateTime = myDateTime.AddSeconds(seconds);
            //전체 몇초 저장
            totalSeconds = seconds;
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
        /// * 슬라이더에 사용하기 위함
        /// </summary>
        public float GetRemainValue()
        {
            TimeSpan timeSpan = GetRemainTime();

            //int currentSec = timeSpan.TotalSeconds;

            float value = (float)(Math.Floor(timeSpan.TotalSeconds) / totalSeconds);
            value = 1 - value;
            //Debug.Log("currentSec = " + totalSeconds + "   remainSec = " + Math.Floor(timeSpan.TotalSeconds) + "  value = " + value);
            return value;
        }

        /// <summary>
        /// 타임 스팬에서 #D ##:##:## 형식으로 시간 가져오기.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
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