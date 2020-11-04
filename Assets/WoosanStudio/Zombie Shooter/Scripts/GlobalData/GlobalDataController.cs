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

        /// <summary>
        /// 저장 되어야 하는 데이터
        /// </summary>
        [System.Serializable]
        public class Data
        {
            //현재 도박 성공률
            public int GambleCurrentSuccessRate = 50;
            public Data() { }
        }

        public Data data = new Data();

        //겜블시 소모되는 젬
        static public int GambleGem = 10;
        //겜블 성공시 추가되는 성공률
        static public int GamblePerValue = 5;
        //겜블 최대 성공률
        static public int GambleMaxRate = 80;
        //겜블 기본 성공률
        static public int GambleDefaultRate = 50;

        //도박 성공률은 여기서 계산해서 data넣어 저장 시킨다
        //*계산은 아래 계산기 이용해서 결과 결고는 data.GambleCurrentSuccessRate 에 넣기
        //NextValueCalculator.GetGambleSuccessRate(0)

        public void Awake()
        {
            //싱글톤 패턴
            Instance = this;
            //삭제 안함
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
