using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace WoosanStudio.ZombieShooter
{
    public class TimesetTest : MonoBehaviour
    {
        Timeset timeset;

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
            timeset = new Timeset(seconds);
            PlayerPrefs.SetString("TestTime", JsonUtility.ToJson(this.timeset));
        }

        /// <summary>
        /// 시간을 로드하며 로드 이후 GetRemainTime()호출하여 남은 시간 알아올수 있음.
        /// </summary>
        void LoadTime()
        {
            //데이터 없으면 저장
            if (!PlayerPrefs.HasKey("TestTime"))
            {
                SaveTime(15);
                Debug.Log("데이터가 없어요");
            }
            timeset = JsonUtility.FromJson<Timeset>(PlayerPrefs.GetString("TestTime"));
        }

        /*#region [-TestCode]
        void Update()
        {
            //업그레이드 연구 시간 저장 테스트
            if (Input.GetKeyDown(KeyCode.A))
            {
                Timeset timeset = new Timeset(30);

                timeset.GetRemainTimeToString();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {

            }
        }
        #endregion
        */

    }
}
