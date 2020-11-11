﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 모든 카드 데이터
    /// 카드 세팅 데이터와 동기화도 진행
    /// *MVP 모델
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

        /// <summary>
        /// 저장 데이터와 실제 CardSetting 데이터간 동기화 시킴
        /// * 이때 언락 제한이 풀링는 레벨이 있다면 풀어야 함
        /// </summary>
        public void Synchronization()
        {
            Debug.Log("================> 카드 데이터와 카드 세팅 데이터 동기화 진행 완료 <================");

            //데이터 동기화전 바뀌어야 하는 부분 반영
            
            //이미 언락이 됬다면 역쉬 언락

            //데이터 데입
            for (int i = 0; i < cardSettings.Count; i++)
            {
                //[테스트 코드]
                //data.CardDatas[i].UpgardeTimeset.Print();
                //Debug.Log("사용 여부 = " + data.CardDatas[i].UseAble);
                //Debug.Log("레벨 = " + data.CardDatas[i].Level);
                //Debug.Log("내구도 = " + data.CardDatas[i].durability);
                //Debug.Log("UI 정렬 인덱스 = " + data.CardDatas[i].SortIndex);
                //Debug.Log("UI 연구정렬 인덱스 = " + data.CardDatas[i].ResearchSlotIndex);

                //아직 언락 안된상태
                if(data.CardDatas[i].UseAble == false)
                {
                    //언락 레벨 보다 현재 레벨이 높다면 언락.
                    if (cardSettings[i].UnlockLevel <= data.CardDatas[i].Level)
                    {
                        //카드 언락 여부
                        cardSettings[i].UseAble = data.CardDatas[i].UseAble = true;
                    }
                }

                //업드레이드 타임 데이터 세팅
                cardSettings[i].UpgradeTimeset = data.CardDatas[i].UpgardeTimeset;
                //카드 언락 여부
                cardSettings[i].UseAble = data.CardDatas[i].UseAble;
                //카드의 레벨
                cardSettings[i].Level =  data.CardDatas[i].Level;
                //카드의 내구도 -> 사용 현재 안함
                cardSettings[i].Durability = data.CardDatas[i].durability;
                //UI 에서의 정렬 인덱스
                cardSettings[i].SortIndex = data.CardDatas[i].SortIndex;
                //연구 슬롯의 정렬 인덱스
                cardSettings[i].ResearchSlotIndex = data.CardDatas[i].ResearchSlotIndex;
            }

            //싱크 마무리 세이브
            Save();
        }

        /// <summary>
        /// 카드 데이터의 불러오기
        /// * 반드시 CardSetting 과 씽크 마추는 작업 필요
        /// </summary>
        public void Load()
        {
            if (!PlayerPrefs.HasKey("UICard")) { Save(); }

            data = JsonUtility.FromJson<UICardModel.Data>(PlayerPrefs.GetString("UICard"));

            //data.Print("[Load]");
        }

        /// <summary>
        /// 카드 데이터 저장
        /// * 반드시 CardSetting 과 씽크 마추는 작업 필요
        /// </summary>
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
        /*void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Load();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                PlayerPrefs.DeleteAll();
            }
        }*/
        #endregion
    }
}
