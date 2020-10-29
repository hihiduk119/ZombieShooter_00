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
    /// 시간이 필요한 모든 데이터는 생성시 이걸 가지고 있어야 한다.
    /// 사용법
    /// -> Make 또는 생성자에 시간 넣기. 생성시간이 셋업됨.
    /// -> GetRemainTime() 호출하여 셋업된 시간으로부터 남은 시간 가져오기.
    /// </summary>
    public class Timeset
    {
        public long endDateTime;
        public long startDateTime;
        //전체 몇초인지 구해놓기
        public int totalSeconds = 0;

        //1.생성시 바로 데이터 저장
        //2.로드후 호출시 바로 데이터 반영

        /// <summary>
        /// 현재 시간 기준으로 셋업 생성자
        /// </summary>
        /// <param name="minutes">분단위로 셋업</param>
        public Timeset(int Seconds)
        {
            Make(Seconds);
        }

        //빈 생성자
        public Timeset() {}

        /// <summary>
        /// 현재 시간 기준으로 셋업 
        /// </summary>
        /// <param name="minutes"></param>
        public void Make(int seconds)
        {
            Debug.Log("지금 시간 = " + DateTime.Now.ToString("yyyy년 MM월 dd일 HH시 mm분 ss초"));
            startDateTime = endDateTime =  DateTime.Now.ToBinary();

            endDateTime = DateTime.FromBinary(endDateTime).AddSeconds(seconds).ToBinary();
            //전체 몇초 저장
            totalSeconds = seconds;
        }

        /// <summary>
        /// 내가 가진 시간에서 남은시간을 timespan으로 반환
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetRemainTime()
        {
            TimeSpan timeSpan = DateTime.FromBinary(endDateTime).Subtract(DateTime.Now);
            TimeToString(timeSpan);
            //Debug.Log("남은시간 = " + timeString);
            return timeSpan;
        }

        /// <summary>
        /// 남은시간을 timespan으로 반환
        /// </summary>
        /// <param name="preDateTime">현재와 비교하려고하는 이전 시간</param>
        /// <returns></returns>
        static public TimeSpan GetRemainTime(DateTime preDateTime)
        {
            TimeSpan timeSpan = preDateTime.Subtract(DateTime.Now);
            TimeToString(timeSpan);
            //Debug.Log("남은시간 = " + timeString);
            return timeSpan;
        }

        /// <summary>
        /// 남은시간을 timespan으로 반환
        /// </summary>
        /// <param name="preDateTime">현재와 비교하려고하는 이전 시간</param>
        /// <returns></returns>
        static public TimeSpan GetRemainTime(long preDateTime)
        { 
            TimeSpan timeSpan = DateTime.FromBinary(preDateTime).Subtract(DateTime.Now);
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
        static public String TimeToString(TimeSpan time)
        {
            String tmp, timeString;
            string[] timeSpan;
            StringBuilder stringBuilder = new StringBuilder();

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
            Debug.Log(timeString);
            return timeString;
        }


        /// <summary>
        /// 타임 스팬에서 #D ##:##:## 형식으로 시간 가져오기.
        /// </summary>
        /// <param name="binary">바이너리값</param>
        /// <returns></returns>
        static public String TimeToString(long binary)
        {
            TimeSpan time = DateTime.FromBinary(binary).TimeOfDay;
            string tmp, timeString;
            string[] timeSpan;
            StringBuilder stringBuilder = new StringBuilder();

            tmp = time.ToString();
            timeSpan = tmp.Split('.');
            stringBuilder.Clear();

            //Day 없고 시간만 존제
            if (timeSpan.Length == 2)
            {
                stringBuilder.Append(timeSpan[0]);
            }
            else if (timeSpan.Length == 3)
            {
                stringBuilder.Append(timeSpan[0]);
                stringBuilder.Append("d ");
                stringBuilder.Append(timeSpan[1]);
            }
            else
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

            Debug.Log(timeString);
            return timeString;
        }
    }
}