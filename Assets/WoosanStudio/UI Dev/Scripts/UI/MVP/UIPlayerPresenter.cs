using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using System.Text;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 플레이어의 선택 및 구매 컨트롤
    /// 
    /// *MVC패턴
    /// </summary>
    public class UIPlayerPresenter : MonoBehaviour
    {
        [Header("[MVP 모델]")]
        public UIPlayerModel Model;

        [System.Serializable]
        public class UpdateCharacter : UnityEvent<int> { }

        [System.Serializable]
        public class UpdatePurchaseView : UnityEvent<PurchaseViewData> { }

        [System.Serializable]
        public class UpdateInfoView : UnityEvent<InfoViewData> { }

        [System.Serializable]
        public class UpdateUseAble : UnityEvent<bool> { }
        

        [System.Serializable]
        //구매 뷰 전용 데이터
        public class PurchaseViewData
        {
            public bool UseAble = false;
            public int RequireGem = 100;
            public int RequireLevel = 10;

            public PurchaseViewData(bool value = true) { UseAble = value; }
            public PurchaseViewData(int gem,int level,bool value = false) { RequireGem = gem; RequireLevel = level; UseAble = value; }

            public void Print()
            {
                Debug.Log("사용 여부 = " + UseAble + " 젬 요구 = " + RequireGem + "  요구레벨 = " + RequireLevel);
            }
        }

        [System.Serializable]
        //캐릭터 인포 뷰 전용 데이터
        public class InfoViewData
        {
            //캐릭터 이름
            public string Name;
            //캐릭터 이미지
            public Sprite Image;
            //설명 리스트
            public List<string> Descripsions;
            //캐릭터 플레이 시간
            public long PlayTime = 0;
            //사냥한 몬스터 수
            public int HuntedMonster = 0;

            public InfoViewData(string name, Sprite image , List<string> descripsions, long playTime, int huntedMonster)
            {
                Name = name;
                Image = image;
                Descripsions = descripsions;
                PlayTime = playTime;
                HuntedMonster = huntedMonster;
            }

            public void Print()
            {
                StringBuilder stringBuilder = new StringBuilder();
                
                for (int i = 0; i < Descripsions.Count; i++)
                {
                    stringBuilder.Append("[");
                    stringBuilder.Append(Descripsions[i]);
                    stringBuilder.Append("]");
                }

                Debug.Log("이름 = " + Name + " 내용 = " + stringBuilder.ToString() + " 플레이 시간 = " + PlayTime + "  몬스터 킬수 = " + HuntedMonster);
            }
        }


        [Header("[캐릭터 업데이트 이벤트]")]
        public UpdateCharacter ChangeCharacterEvent = new UpdateCharacter();

        [Header("[캐릭터 구매 창 이벤트]")]
        public UpdatePurchaseView PurchaseActivationEvent = new UpdatePurchaseView();

        [Header("[캐릭터 정보 이벤트]")]
        public UpdateInfoView UpdateInfoEvent = new UpdateInfoView();

        [Header("[캐릭터 사용가능 이벤트]")]
        public UpdateUseAble UpdateUseAbleEvent = new UpdateUseAble();

        private void Start()
        {
            //저장된 모델 불러오기
            Model.Load();
            //실제 모델 변경
            CharacterChange(0);
        }

        /// <summary>
        /// 모델을 변경함
        /// </summary>
        /// <param name="type"></param>
        public void CharacterChange(int value)
        {
            int currentIndex = 0;

            //현재 모델의 인덱스 가져오기
            currentIndex = Model.data.SelectedCharacter;

            currentIndex += value;

            //Debug.Log("currentIndex = " + currentIndex);

            int maxIndex = System.Enum.GetValues(typeof(UIPlayerSelectModel.ModelType)).Length;

            if (currentIndex < 0) { currentIndex = 0; }
            if (maxIndex <= currentIndex) { currentIndex = maxIndex - 1; }

            //캐릭터 변경 통지
            ChangeCharacterEvent.Invoke(currentIndex);

            List<string> desc = new List<string>();


            //1부터 시작
            //0은 캐릭터 변경한다는 내용이 있음
            for (int i = 1; i < Model.cardSettings[currentIndex].Properties.Count; i++)
            {
                //0레벨 적용된 완성된 설명 가져오기
                //* [수정필요]카드 레벨을 적용 할지 말지 나중에 결정
                desc.Add(Model.cardSettings[currentIndex].Properties[i].GetCompletedDescripsion(Model.cardSettings[currentIndex].Level));
            }

            InfoViewData infoViewData = new InfoViewData(
                Model.cardSettings[currentIndex].Name,
                Model.cardSettings[currentIndex].Sprite,
                desc,
                0,
                0
                );

            //infoViewData.Print();

            //* 죽인 몬스터 및 플레이타임 가져오는 부분 필요
            UpdateInfoEvent.Invoke(infoViewData);


            //캐릭터 사용 가능 여부 발생
            UpdateUseAbleEvent.Invoke(Model.data.CardDatas[currentIndex].UseAble);

            //구매 뷰 사용 여부 통지
            if (Model.data.CardDatas[currentIndex].UseAble)
            {
                PurchaseActivationEvent.Invoke(new PurchaseViewData());
            }
            else
            {
                PurchaseActivationEvent.Invoke(new PurchaseViewData(Model.cardSettings[currentIndex].GemPrice, Model.cardSettings[currentIndex].UnlockLevel));
            }


            //변경 된 인덱스 모델 데이터에 넣음
            Model.data.SelectedCharacter = currentIndex;

            //저장
            Model.Save();
        }
    }
}
