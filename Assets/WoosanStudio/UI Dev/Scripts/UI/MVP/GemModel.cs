using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 
    /// *MVP 모델
    /// </summary>
    public class GemModel : MonoBehaviour
    {
        [System.Serializable]
        public class Data
        {
            [Header("[현재 Gem]")]
            public int Gem = 0;

            public Data() { }

            public void Print()
            {
                Debug.Log("Gem = " + Gem);
            }
        }

        public Data data = new Data();

        /// <summary>
        /// 현재 가지고있는 코인 가져오기 위해 한번 실행 필요
        /// </summary>
        public void Initialize()
        {
            Load();
        }

        /// <summary>
        /// 데이터 로드
        /// </summary>
        public void Load()
        {
            if (!PlayerPrefs.HasKey("Gem")) { Save(); }
            data = null;
            data = JsonUtility.FromJson<GemModel.Data>(PlayerPrefs.GetString("Gem"));

            //Debug.Log("Gem 로드 완료");
            //data.Print();
        }

        /// <summary>
        /// 데이터 저장
        /// </summary>
        public void Save()
        {
            PlayerPrefs.SetString("Gem", JsonUtility.ToJson(data));

            //Debug.Log("Gem 저장 완료");
            //data.Print();
        }
    }
}
