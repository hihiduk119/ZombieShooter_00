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
    [System.Serializable]
    public class Timeset
    {
        //끝나는 시간
        public long endDateTime;
        //시작했던 시간
        public long startDateTime;
        //전체 몇초인지 미리 구해놓기
        public int totalSeconds = 0;
        //지금 업글 중인지 아닌지 확인 => GetRemainTime()호출시 자동 변경
        public bool bUpgrading = false;

        //1.생성시 바로 데이터 저장
        //2.로드후 호출시 바로 데이터 반영

        /// <summary>
        /// 내용 출력
        /// </summary>
        public void Print()
        {
            Debug.Log("지금 업그레이드 중 = " + bUpgrading + " 끝 시간 = " + TimeToString(endDateTime) + "  시작 시간 = " + TimeToString(startDateTime) + "  전체 시간(초) = " + totalSeconds);
        }

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
        private void Make(int seconds)
        {
            //Debug.Log("지금 시간 = " + DateTime.Now.ToString("yyyy년 MM월 dd일 HH시 mm분 ss초"));
            startDateTime = endDateTime =  DateTime.Now.ToBinary();

            endDateTime = DateTime.FromBinary(endDateTime).AddSeconds(seconds).ToBinary();
            //전체 몇초 저장
            totalSeconds = seconds;
        }

        /// <summary>
        /// 현재 레벨 기준의 업그레이드 시간 기준으로 타임셋을 만든다.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="maxLevel"></param>
        public void MakeTimesetUsingLevel(int level,int maxLevel)
        {
            //최대 레벨 만큼의 모든 시간 값 가져오기
            List<int> timeValues = NextValueCalculator.GetUpgradeTimes(maxLevel);
            //실제 시간을 만듬
            Make(timeValues[level]);
        }

        /// <summary>
        /// 현재 업그레이드 중인지 아닌지 확인
        /// * 이 부분 값이 없을때는 가져오면 문제 발생
        /// * 테스트 더 필요
        /// </summary>
        /// <returns></returns>
        public bool IsUpgrading()
        {
            TimeSpan timeSpan = DateTime.FromBinary(endDateTime).Subtract(DateTime.Now);

            //전체 시간을 가져와서 0보다 크다면 아직 업그레이드중
            if (timeSpan.TotalSeconds > 0) { bUpgrading = false; }
            //0보다 작다면 업그레이드 끝
            else { bUpgrading = true; }

            return bUpgrading;
        }

        /// <summary>
        /// 내가 가진 시간에서 남은시간을 timespan으로 반환
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetRemainTime()
        {
            TimeSpan timeSpan = DateTime.FromBinary(endDateTime).Subtract(DateTime.Now);
            //스트링으로 출력 및 스트링 결과 리턴
            TimeToString(timeSpan);

            //업글 중인지 아닌지 확인
            //이때 자동으로 isUpgrading에 정보 업데이트
            IsUpgrading();
            //Debug.Log("남은시간 = " + timeString);
            return timeSpan;
        }

        /// <summary>
        /// 내가 가진 시간에서 남은시간을 string으로 반환
        /// ##d ##:##:## 형태
        /// </summary>
        /// <returns></returns>
        public String GetRemainTimeToString()
        {
            TimeSpan timeSpan = DateTime.FromBinary(endDateTime).Subtract(DateTime.Now);
            //스트링으로 출력 및 스트링 결과 리턴
            

            //업글 중인지 아닌지 확인
            //이때 자동으로 isUpgrading에 정보 업데이트
            IsUpgrading();
            //Debug.Log("남은시간 = " + timeString);
            return TimeToString(timeSpan);
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
        static public string TimeToString(TimeSpan time)
        {
            String tmp, timeString;
            string[] timeSpan = null;
            StringBuilder stringBuilder = new StringBuilder();

            //타임 스팬에서 남은 시간을 가져옴
            //-> ex) 86400초(하루)+10초
            //1.00:00:10.2934014
            tmp = time.ToString();

            
            timeSpan = tmp.Split('.');

            Debug.Log("tmp = " + tmp);
            for (int i = 0; i < timeSpan.Length; i++)
            {
                Debug.Log("length = [" + i + "]  index = [" + i + "] = " + timeSpan[i]);
            }

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
            } else if(timeSpan.Length == 1)//timespan에서 초 직접 변환의 경우 발생
            {
                stringBuilder.Append(time.ToString());
            }
            else
            {
                Debug.Log("시간 파싱 에러 발생!!" + time.ToString());
                stringBuilder.Append("E --:--:--");
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
        static public string TimeToString(long binary)
        {
            TimeSpan time = DateTime.FromBinary(binary).TimeOfDay;
            string tmp, timeString;
            string[] timeSpan;
            StringBuilder stringBuilder = new StringBuilder();

            tmp = time.ToString();
            
            timeSpan = tmp.Split('.');

            Debug.Log("tmp = " + tmp);
            for (int i = 0; i < timeSpan.Length; i++)
            {
                Debug.Log("length = [" + i + "]  index = [" + i + "] = " + timeSpan[i]);
            }

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
            else if (timeSpan.Length == 1) //timespan에서 초 직접 변환의 경우 발생
            {
                stringBuilder.Append(time.ToString());
            }
            else
            {
                Debug.Log("시간 파싱 에러 발생!!" + time.ToString());
                stringBuilder.Append("E --:--:--");
            }

            //for(int index = 0; index < timeSpan.Length ;index++)
            //{
            //    Debug.Log(timeSpan[index]);
            //}

            //Debug.Log("원본 = " + time.ToString());
            timeString = stringBuilder.ToString();

            //Debug.Log(timeString);
            return timeString;
        }

        /// <summary>
        /// 초로 시간 스트링을 얻어오기
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        static public string SecondsToTimeToString(int seconds)
        {
            TimeSpan timeSpan = new TimeSpan(0, 0, seconds);
            return TimeToString(timeSpan);
        }
    }
}