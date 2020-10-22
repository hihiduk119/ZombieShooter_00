using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// UI에서 사용하는 플레이어의 데이터
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
        }

        [System.Serializable]
        public class Data
        {
            //현재 선택한 캐릭터
            public int SelectedCharacter = 0;

            //저장이 필요한 CardSetting와 연동되는 데이터
            public List<CardData> CardDatas = new List<CardData>();
        }

        public Data data = new Data();

        public void Load()
        {
            if (!PlayerPrefs.HasKey("UICharacter")) { Save(); }

            data = JsonUtility.FromJson<UIPlayerModel.Data>(PlayerPrefs.GetString("UICharacter"));
        }

        public void Save()
        {
            PlayerPrefs.SetString("UICharacter", JsonUtility.ToJson(data));
        }
    }
}
