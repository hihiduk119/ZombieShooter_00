using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter
{
    /// <summary> 
    /// 공통으로 공유하는 데이터
    /// </summary>
    public class GlobalDataController : MonoBehaviour
    {
        //싱글톤 패턴
        static public GlobalDataController Instance;

        [System.Serializable]
        public class Data
        {
            public Data() { }
        }

        public Data data;

        //겜블시 소모되는 젬
        static public int GambleGem = 10;
        //겜블 성공시 추가되는 성공률
        static public int GamblePerValue = 5;
        //겜블 최대 성공률
        static public int GambleMaxRate = 80;
        //겜블 기본 성공률
        static public int GambleDefaultRate = 50;


        public void Awake()
        {
            //싱글톤 패턴
            Instance = this;
            //삭제 안함
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
