using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter.Map
{
    /// <summary>
    /// 스테이지 구성을 위한 세팅
    /// </summary>
    [CreateAssetMenu(menuName = "ZombieShooter/Stage/Make", fileName = "StageData")]
    public class Setting : ScriptableObject
    {
        [Header("[맵 이름]")]
        [SerializeField]
        private new string name = "";
        public string Name { get => name; }

        [Header("[스테이지 이름->호출되는 씬 이름과 같아야 함]")]
        [SerializeField]
        private string scene = "";
        public string Scene { get => scene; }

        [Header("[해당 맵의 세팅값]")]
        [SerializeField]
        private List<Map> stepDatas = new List<Map>();
        public List<Map> StepDatas { get => stepDatas; }

        [System.Serializable]
        public class Stage
        {
            [Header("[스코어에 의해 계산된 별 계산]")]
            [SerializeField]
            int StarCount = -1;

            [Header("[클리어 시간으로 별점 계산]")]
            //*완료시마다 갱신
            [SerializeField]
            int Score = -1;

            [Header("[클리어 시간별 등급값]")]
            //*칼큘레이터가 등급값 넣어줘야함
            [SerializeField]
            int[] ScoreValue = { -1, -1, -1 };
        }

        [System.Serializable]
        public class Data
        {
            [Header("[스테이지 데이터들]")]
            [SerializeField]
            public List<Stage> StageDatas = new List<Stage>();
        }

        [Header("[데이터 확인용으로 열어놓지만 나중에 막아야함]")]
        public Data data = new Data();

        /// <summary>
        /// 스테이지 구간별 데이터
        /// *라운드 모음
        /// </summary>
        [System.Serializable]
        public class Map
        {
            [Header("[라운드 범위]")]
            public int[] StageRange = {0,1};

            [Header("[스테이지에서 기본 선택 캐릭터 카드]")]
            public List<CardSetting> SelectAbleCharacterCard = new List<CardSetting>();

            [Header("[스테이지에서 기본 선택 무기 카드]")]
            public List<CardSetting> SelectAbleWeaponCard = new List<CardSetting>();

            [Header("[스테이지에서 기본 선택 탄약 카드]")]
            public List<CardSetting> SelectAbleAmmoCard = new List<CardSetting>();
        }


        /// <summary>
        /// Json데이터 저장
        /// </summary>
        void Save()
        {
            string strJson = JsonUtility.ToJson(this.data);
            PlayerPrefs.SetString("Map_" + Name, strJson);
        }

        /// <summary>
        /// Json데이터 로드
        /// </summary>
        public void Load()
        {
            if (!PlayerPrefs.HasKey("Map_" + Name)) { Save(); }

            this.data = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("Map_" + Name));
            //로드한 데이터와 현재 데이터 싱크 마추기
            Synchronization();
        }

        /// <summary>
        /// 데이터 싱크 마추기
        /// </summary>
        public void Synchronization()
        {
            //이떼 불필요한 Save발생을 피하기 위해 로컬 데이터 사용
            //this.st = cardData.UseAble;
        }
    }
}
