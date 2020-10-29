using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class TimesetTest : MonoBehaviour
    {
        Timeset timeset;

        private void Start()
        {
            MakeTime();
        }

        void MakeTime()
        {
            //120초생성
            timeset = new Timeset(2000000);
        }

        void LoadTime()
        {

        }

        void SaveTime()
        {
            //JsonUtility.ToJson();
            //PlayerPrefs.SetString("TestTime",)
        }

        #region [-TestCode]
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                timeset.GetRemainTime();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {

            }
        }
        #endregion

    }
}
