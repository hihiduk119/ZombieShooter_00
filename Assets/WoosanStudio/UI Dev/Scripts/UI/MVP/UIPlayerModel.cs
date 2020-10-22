using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;


namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 플레이어의 캐릭터 데이터
    /// *MVC패턴
    /// </summary>
    public class UIPlayerModel : MonoBehaviour
    {
        //리스트 순서는 캐릭터 순서와 같다 -> CardSetting.CardTypeByCharacter
        [Header("[캐릭터 카드세팅 리스트 = 0번은 사용 안함]")]
        public List<CardSetting> cardSettings = new List<CardSetting>();

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

            public CardData(bool useAble = false) { UseAble = useAble; }
        }

        [System.Serializable]
        public class Data
        {
            //현재 선택한 캐릭터
            public int SelectedCharacter = 0;

            //저장이 필요한 CardSetting와 연동되는 데이터
            public List<CardData> CardDatas = new List<CardData>();

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

        public Data data = new Data();

        public void Load()
        {
            if (!PlayerPrefs.HasKey("UICharacter")) { Save(); }

            data = JsonUtility.FromJson<UIPlayerModel.Data>(PlayerPrefs.GetString("UICharacter"));

            data.Print("[Load]");
        }

        public void Save()
        {
            //데이터 최초 생성시
            if(data.CardDatas.Count == 0)
            {
                for (int i = 0; i < cardSettings.Count; i++)
                {
                    //첫번째케릭터는 무조건 언락
                    if(i == 0) { data.CardDatas.Add(new CardData(true));}
                    //외 나머지 케릭터 락
                    else { data.CardDatas.Add(new CardData());}
                }
            }

            PlayerPrefs.SetString("UICharacter", JsonUtility.ToJson(data));

            data.Print("[Save]");
        }

        /*
        #region [-TestCode]
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Load();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }
        #endregion
        */
    }
}
