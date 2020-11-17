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
            //기본 비지니스맨 선택됨
            public int SelectedCharacter = 16;
            public Data() { }
        }

        //현재 도박 성공률
        private int gambleCurrentSuccessRate;
        public int GambleCurrentSuccessRate
        {
            get => gambleCurrentSuccessRate; set {
                data.GambleCurrentSuccessRate = gambleCurrentSuccessRate = value;
                Save();
            }
        }

        //기본 비지니스맨 선택됨
        private int selectedCharacter;
        public int SelectedCharacter
        {
            get => selectedCharacter; set {
                data.SelectedCharacter = selectedCharacter = value;
                Save();
            }
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
        //카드 순서에 의해 발생한 캐릭터 카드 시작 인덱스
        static public int CharacterCardStartIndex = 16;
        //알림 통지 기준이 되는 씬이름
        static public string PresentSceneName = "1.ZombieShooter-Robby";

        //도박 성공률은 여기서 계산해서 data넣어 저장 시킨다
        //*계산은 아래 계산기 이용해서 결과 결고는 data.GambleCurrentSuccessRate 에 넣기
        //NextValueCalculator.GetGambleSuccessRate(0)

        public void Awake()
        {
            //싱글톤 패턴
            Instance = this;
            //데이터 로드
            Load();
            //삭제 안함
            DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// Json데이터 저장
        /// </summary>
        void Save()
        {
            //Debug.Log("[Save] 글로벌 컨트롤러");
            string strJson = JsonUtility.ToJson(data);
            PlayerPrefs.SetString("GlobalData", strJson);
        }

        /// <summary>
        /// Json데이터 로드
        /// </summary>
        void Load()
        {
            //Debug.Log("[Load] 글로벌 컨트롤러");
            if (!PlayerPrefs.HasKey("GlobalData")) { Save(); }

            data = JsonUtility.FromJson<GlobalDataController.Data>(PlayerPrefs.GetString("GlobalData"));

            //로드후에 반드시 싱크 마춰야 함
            Synchronization();
        }

        /// <summary>
        /// 데이터와 싱크 마춤
        /// </summary>
        void Synchronization()
        {
            //이떼 불필요한 Save발생을 피하기 위해 로컬 데이터 사용
            this.gambleCurrentSuccessRate = data.GambleCurrentSuccessRate;
            this.selectedCharacter = data.SelectedCharacter;
        }
    }
}
