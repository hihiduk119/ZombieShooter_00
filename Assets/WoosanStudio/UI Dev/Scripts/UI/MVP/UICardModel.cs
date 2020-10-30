using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 모든 카드 데이터
    /// *MPV 모델
    /// </summary>
    public class UICardModel : MonoBehaviour
    {
        [System.Serializable]
        public class CardData
        {
            //캐릭터가 사용 가능 한지 아닌지
            //* 구매 또는 레벨 달성시 언락됨.
            public bool UseAble = false;

            //카드의 레벨
            public int Level = 0;

            //카드 내구도 [현재 사용 안함]
            public int durability = 100;

            //<===== 이하 추가 한 데이터

            //UI에서 데이터의 순서
            public int SortIndex;

            //연구 중이었다면 UI데이터의 순서
            public int ResearchSlotIndex = -1;

            //남은 업글 시간
            //public long UpgradeStartedTime = 0;
            //업글중 이었는지 아닌지
            //public bool IsUpgrading = false;
            //남은 업글 시간 및 업글 중인지 아닌지 까지 모두 알수 있음
            public Timeset UpgardeTimeset;

            

            
            
            

            public CardData(bool useAble = false) { UseAble = useAble; }
        }

        [System.Serializable]
        public class Data
        {
            //현재 선택한 캐릭터 => 모든 카드에서 선택되기때문에 캐릭터 16부터 시작
            public int SelectedCharacter = 16;

            //저장이 필요한 CardSetting와 연동되는 데이터
            public List<UICardModel.CardData> CardDatas = new List<UICardModel.CardData>();

            private StringBuilder stringBuilder = new StringBuilder();

            public void Print(string prefix)
            {
                for (int i = 0; i < CardDatas.Count; i++)
                {
                    stringBuilder.Append(" - [");
                    stringBuilder.Append(i);
                    stringBuilder.Append("=");
                    stringBuilder.Append(CardDatas[i].UseAble.ToString());
                    stringBuilder.Append(",");
                    stringBuilder.Append(CardDatas[i].Level);
                    stringBuilder.Append(",");

                    stringBuilder.Append(CardDatas[i].durability);
                    stringBuilder.Append("] - ");
                }

                Debug.Log(prefix +  " 선택한 캐릭터 = " + SelectedCharacter + "  전체 = " + stringBuilder.ToString());

                stringBuilder.Clear();
            }
        }

        //UICardInfoPopupPresenter-> 에서 카드 세팅을 가져와 사용하기 때문에 업데이트 필수
        [Header("[모든 카드 리스트 세팅 => 세이브 로드시 반드시 동기화 해야함]")]
        public List<CardSetting> cardSettings = new List<CardSetting>();

        [Header("[실제 모든 카드 리스트가 저장된 데이터]")]
        public UICardModel.Data data = new UICardModel.Data();

        public void Load()
        {
            if (!PlayerPrefs.HasKey("UICard")) { Save(); }

            data = JsonUtility.FromJson<UICardModel.Data>(PlayerPrefs.GetString("UICard"));

            //data.Print("[Load]");
        }

        public void Save()
        {
            //데이터 최초 생성시
            if (data.CardDatas.Count == 0)
            {
                for (int i = 0; i < cardSettings.Count; i++)
                {
                    //첫번째케릭터는 무조건 언락
                    //첫 캐릭터는 16이며 SelectedCharacter와 같아서 통일
                    if (i == data.SelectedCharacter) { data.CardDatas.Add(new UICardModel.CardData(true)); }
                    //외 나머지 케릭터 락
                    else { data.CardDatas.Add(new UICardModel.CardData()); }
                }
            }

            PlayerPrefs.SetString("UICard", JsonUtility.ToJson(data));

            //data.Print("[Save]");
        }

        
        #region [-TestCode]
        //void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        Load();
        //    }

        //    if (Input.GetKeyDown(KeyCode.S))
        //    {
        //        Save();
        //    }

        //    if (Input.GetKeyDown(KeyCode.D))
        //    {
        //        PlayerPrefs.DeleteAll();
        //    }
        //}
        #endregion
    }
}
