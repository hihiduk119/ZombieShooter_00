using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using System.Text;
using Ricimi;

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
        public UICardModel Model;
        [Header("[돈이 없음 팝업 오프너]")]
        public PopupOpener NotifyPopupOpener;
        [Header("[돈이 있고 최종 확인용 오프너]")]
        public PopupOpener NotifyYesOrNoPopupOpener;

        [System.Serializable]
        public class UpdateCharacter : UnityEvent<int> { }

        [System.Serializable]
        public class UpdateCharacterPurchaseView : UnityEvent<CardSetting> { }

        [System.Serializable]
        public class UpdateInfoView : UnityEvent<InfoViewData> { }

        [System.Serializable]
        public class UpdateUseAble : UnityEvent<bool> { }

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

                //Debug.Log("이름 = " + Name + " 내용 = " + stringBuilder.ToString() + " 플레이 시간 = " + PlayTime + "  몬스터 킬수 = " + HuntedMonster);
            }
        }


        [Header("[캐릭터 업데이트 이벤트]")]
        public UpdateCharacter ChangeCharacterEvent = new UpdateCharacter();

        [Header("[신 캐릭터 구매 창 이벤트]")]
        public UpdateCharacterPurchaseView CharacterPurchaseActivationEvent = new UpdateCharacterPurchaseView();

        [Header("[캐릭터 정보 이벤트]")]
        public UpdateInfoView UpdateInfoEvent = new UpdateInfoView();

        [Header("[스타트 버튼 사용가능 이벤트]")]
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
            int currentIndex = GlobalDataController.CharacterCardStartIndex;

            //카드 순서에의해 발생한 캐릭터 간 
            int characterIndexInterval = GlobalDataController.CharacterCardStartIndex;

            //현재 모델의 인덱스 가져오기
            currentIndex = GlobalDataController.Instance.SelectedCharacter;

            //카드 데이터 순서에 더하기 때문에 +16필요.
            currentIndex += value;

            //Debug.Log("currentIndex = " + currentIndex + " SelectedCharacter =  " + GlobalDataController.Instance.SelectedCharacter + "   value = " + value);

            int maxIndex = System.Enum.GetValues(typeof(UIPlayerSelectModel.ModelType)).Length;

            //카드 간격 만큼 계산해서 더해주는 작업 필요
            if (currentIndex < 0 + characterIndexInterval) { currentIndex = 0 + characterIndexInterval; }
            if (maxIndex + characterIndexInterval <= currentIndex) { currentIndex = maxIndex - 1 + characterIndexInterval; }

            //Debug.Log("currentIndex = " + currentIndex + "   characterIndexInterval = " + characterIndexInterval);

            //캐릭터 변경 통지 => 변경시 필요한 인덱스는 -16뺀 0 부터 13까지임.
            ChangeCharacterEvent.Invoke(currentIndex - characterIndexInterval);

            //설명
            List<string> desc = new List<string>();

            //프로퍼티에서 설명들 가져옴
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

            infoViewData.Print();

            //* 죽인 몬스터 및 플레이타임 가져오는 부분 필요
            UpdateInfoEvent.Invoke(infoViewData);

            //캐릭터 사용 가능 여부 발생
            UpdateUseAbleEvent.Invoke(Model.cardSettings[currentIndex].UseAble);

            //구매 뷰 업데이트 통지
            CharacterPurchaseActivationEvent.Invoke(Model.cardSettings[currentIndex]);

            //변경 된 인덱스 모델 데이터에 넣음
            GlobalDataController.Instance.SelectedCharacter = currentIndex;
        }

        /// <summary>
        /// 캐릭터 구매 버튼 클릭
        /// </summary>
        public void ClickCharacterPurchaseButton()
        {
            //현재 캐릭터의 구매 가격 알아오기
            CardSetting cardSetting = Model.cardSettings[GlobalDataController.Instance.SelectedCharacter];

            //Debug.Log("이 캐릭터의 가격은 요? Price = " + cardSetting.GemPrice);

            //젬 프리젠트 가져오기
            GemPresenter gemPresenter = GameObject.FindObjectOfType<GemPresenter>();

            //현재 젬 확인
            int gem = gemPresenter.GetGem();
            //구매 가능한 수량 확인 -> 젬 부족
            if( gem < cardSetting.GemPrice)
            {
                //젬 부족 메시지 출력 셋업
                NotifyPopupOpener.popupPrefab.GetComponent<UINotifyPopupPresenter>().Type = UINotifyPopupModel.Type.NotEnoughGem;
                //해당 팝업 오픈
                NotifyPopupOpener.OpenPopup();
            }
            else //있으면 구매 확인 팝업.
            {
                //젬 부족 메시지 출력 셋업 -> set a message print 
                NotifyYesOrNoPopupOpener.popupPrefab.GetComponent<UINotifyYesOrNoPopupPresenter>().Desicription = "Buy [" + cardSetting.Name + "] for $" + string.Format("{0:0,0}", cardSetting.GemPrice);
                
                //해당 팝업 오픈
                NotifyYesOrNoPopupOpener.OpenPopup();
            }
        }

        /// <summary>
        /// 실제 구매
        /// * UINotifyYesOrNoPopupPresenter.ClickYes()에서 실행됨.
        /// </summary>
        public void PurchaseCharacter()
        {
            //구매 실행
            Debug.Log("==========> 구매실행");

            //현재 캐릭터의 구매 가격 알아오기
            CardSetting cardSetting = Model.cardSettings[GlobalDataController.Instance.SelectedCharacter];

            //캐릭터 구매
            GameObject.FindObjectOfType<GemPresenter>().AddGem(-cardSetting.GemPrice);

            //카드 데이터 변경과 동시에 저장 됨.
            cardSetting.UseAble = true;


            //스타트 버튼 사용 가능 여부 발생
            UpdateUseAbleEvent.Invoke(Model.cardSettings[GlobalDataController.Instance.SelectedCharacter].UseAble);

            //구매 뷰 업데이트 통지
            CharacterPurchaseActivationEvent.Invoke(Model.cardSettings[GlobalDataController.Instance.SelectedCharacter]);
        }
    }
}
