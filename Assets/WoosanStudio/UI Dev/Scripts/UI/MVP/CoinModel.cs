using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 
    /// *MPV 모델
    /// </summary>
    public class CoinModel : MonoBehaviour
    {

        [System.Serializable]
        public class Data
        {
            [Header("[현재 코인]")]
            public int Coin = 0;

            public Data() { }

            public void Print()
            {
                Debug.Log("코인 = " + Coin);
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
            if (!PlayerPrefs.HasKey("Coin")) { Save(); }
            data = null;
            data = JsonUtility.FromJson<CoinModel.Data>(PlayerPrefs.GetString("Coin"));

            //Debug.Log("Coin 로드 완료");
            //data.Print();
        }

        /// <summary>
        /// 데이터 저장
        /// </summary>
        public void Save()
        {
            PlayerPrefs.SetString("Coin", JsonUtility.ToJson(data));

            //Debug.Log("Coin 저장 완료");
            //data.Print();
        }
    }
}
