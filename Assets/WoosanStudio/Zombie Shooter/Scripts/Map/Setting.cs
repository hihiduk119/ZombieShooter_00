using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter.Map
{
    /// <summary>
    /// 스테이지 구성을 위한 세팅
    /// </summary>
    [CreateAssetMenu(menuName = "ZombieShooter/Map/Make", fileName = "[Map ]")]
    public class Setting : ScriptableObject
    {
        //public enum Type
        //{
        //    Town = 1,
        //    Farm,
        //    Port,
        //    MilitaryBase,
        //}

        //[Header("[맵 타입]")]
        //public Type MapType;

        [Header("[맵 사용 유무]")]
        [SerializeField]
        public bool CanUse = false;
        //public bool CanUse { get => canUse; }

        [Header("[맵 초기화 유뮤-> Stage할당을 위해 사용]")]
        [SerializeField]
        private bool hasInitialized = false;
        public bool HasInitialized { get => hasInitialized; }

        [Header("[맵 이름]")]
        [SerializeField]
        private new string name = "";
        public string Name { get => name; }

        [Header("[맵 아이콘]")]
        [SerializeField]
        private Sprite icon;
        public Sprite Icon { get => icon; }

        [Header("[언락 레벨]")]
        [SerializeField]
        private int unlockLevel;
        public int UnlockLevel { get => unlockLevel; }

        [Header("[스테이지 이름->호출되는 씬 이름과 같아야 함]")]
        [SerializeField]
        private string scene = "";
        public string Scene { get => scene; }

        [Header("[사용되는 카드셋 -> 라운드 끝날때 선택 하는 캐릭터,탄약,무기]")]
        [SerializeField]
        private Card.Set.Setting roundEndCardSetting;
        public Card.Set.Setting RoundEndCardSetting { get => roundEndCardSetting; }

        //[Header("[해당 맵의 세팅값]")]
        //[SerializeField]
        //private List<DefaultCard> defaultCardDatas = new List<DefaultCard>();
        //public List<DefaultCard> DefaultCardDatas { get => defaultCardDatas; }

        /*
        [System.Serializable]
        public class Rank
        {
            [Header("[별 갯수 [0:스테이지 미완료]]")]
            [SerializeField]
            public int RankCount = 0;//[0:스테이지 미완료][1,2,3:스테이지 완료 랭크]

            [Header("[저장된 스코어]")]
            //*완료시마다 갱신
            [SerializeField]//스코어는 스테이지 공략 완료 시간.
            public int RankScore = 0;

            [Header("[클리어 시간별 등급값]")]
            //*칼큘레이터가 등급값 넣어줘야함
            [SerializeField]
            public int[] AllRankTable = { 0, 0 }; //{[별 2개 커트], [별 3개 커트]}, 별 1개는 완료만 하면 받음,

            public Rank(int[] allRankTable)
            {
                AllRankTable = allRankTable;
            }
        }
        */

        //최대 도달했던 라운드
        private int reachedRound;
        public int ReachedRound
        {
            get => reachedRound; set
            {
                data.ReachedRound = reachedRound = value;
                Save();
            }
        }


        [System.Serializable]
        public class Data
        {
            [Header("[최대 도달했던 라운드]")]
            [SerializeField]
            public int ReachedRound = 0;

            /// <summary>
            /// 세이브 확인용
            /// </summary>
            //public void Print()
            //{
            //}

            /// <summary>
            /// 모든 아이템 0으로 만들기
            /// </summary>
            public void Reset()
            {
                ReachedRound = 0;
            }
        }

        [Header("[스테이지 세팅들]")]
        [SerializeField]
        private List<Stage.Setting> stageSetting = new List<Stage.Setting>();
        public List<Stage.Setting> StageSetting { get => stageSetting; }


        [Header("[데이터 확인용으로 열어놓지만 나중에 막아야함]")]
        public Data data = new Data();


        /// <summary>
        /// Json데이터 저장
        /// </summary>
        public void Save()
        {
            string strJson = JsonUtility.ToJson(this.data);
            PlayerPrefs.SetString("Map_" + Name, strJson);
        }

        /// <summary>
        /// Json데이터 로드
        /// </summary>
        public void Load()
        {
            if (!PlayerPrefs.HasKey("Map_" + Name))
            {
                //아무것도 없으면 최초 초기화 실행
                //Initialize();
                Save();
            }

            this.data = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("Map_" + Name));
            //로드한 데이터와 현재 데이터 싱크 마추기
            Synchronization();
        }

        /// <summary>
        /// 데이터와 싱크 마춤
        /// </summary>
        void Synchronization()
        {
            this.reachedRound = data.ReachedRound;
        }


        /// <summary>
        /// 
        /// </summary>
        //void Initialize()
        //{
        //    //사용가능 상태 체크
        //    if (!CanUse) return;
        //    //초기화 안됐으면 한번만 실행
        //    //*초기화 됐으면 실행 안함
        //    if(hasInitialized) { return; }
        //    hasInitialized = true;
        //}

        /// <summary>
        /// 해당 맵 데이터 리셋
        /// </summary>
        public void Reset()
        {
            PlayerPrefs.DeleteKey("Map_" + Name);
            //Initialize();
        }
        
    }
}
