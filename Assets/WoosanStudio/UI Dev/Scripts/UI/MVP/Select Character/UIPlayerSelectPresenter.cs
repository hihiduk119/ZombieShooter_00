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
    /// *MVP패턴
    /// </summary>
    public class UIPlayerSelectPresenter : MonoBehaviour
    {
        [Header("[MVP 모델]")]
        public UICardModel Model;
        [Header("[돈이 없음 팝업 오프너]")]
        public PopupOpener NotifyPopupOpener;
        [Header("[돈이 있고 최종 확인용 오프너]")]
        public PopupOpener NotifyYesOrNoPopupOpener;

        [System.Serializable]
        public class UpdateCharacter : UnityEvent<int> { }
        [Header("[캐릭터 업데이트 이벤트]")]
        public UpdateCharacter ChangeCharacterEvent = new UpdateCharacter();

        [System.Serializable]
        public class UpdateData : UnityEvent<CardSetting> { }
        [Header("[캐릭터 가격 창 이벤트]")]
        public UpdateData CharacterPurchaseActivationEvent = new UpdateData();

        [Header("[캐릭터 정보 이벤트]")]
        public UpdateData UpdateInfoEvent = new UpdateData();

        [System.Serializable]
        public class UpdateUseAble : UnityEvent<bool> { }
        [Header("[스타트 버튼 사용가능 이벤트]")]
        public UpdateUseAble UpdateUseAbleEvent = new UpdateUseAble();

        private void Start()
        {
            //시작시 0번으로 최기화
            Change(0);
        }

        /// <summary>
        /// 모델을 변경함
        /// 데이터 Invoke
        /// 인포메이션 뷰에 데이터 전달
        /// </summary>
        /// <param name="type"></param>
        public void Change(int value)
        {
            int currentIndex = GlobalDataController.CharacterCardStartIndex;

            //카드 순서에의해 발생한 캐릭터 간 
            int characterIndexInterval = GlobalDataController.CharacterCardStartIndex;

            //현재 모델의 인덱스 가져오기
            currentIndex = GlobalDataController.Instance.SelectedCharacter;

            //카드 데이터 순서에 더하기 때문에 +16필요.
            currentIndex += value;

            //Debug.Log("currentIndex = " + currentIndex + " SelectedCharacter =  " + GlobalDataController.Instance.SelectedCharacter + "   value = " + value);

            int maxIndex = System.Enum.GetValues(typeof(GlobalDataController.ModelType)).Length;

            //카드 간격 만큼 계산해서 더해주는 작업 필요
            if (currentIndex < 0 + characterIndexInterval) { currentIndex = 0 + characterIndexInterval; }
            if (maxIndex + characterIndexInterval <= currentIndex) { currentIndex = maxIndex - 1 + characterIndexInterval; }

            //Debug.Log("currentIndex = " + currentIndex + "   characterIndexInterval = " + characterIndexInterval);

            //캐릭터 변경 통지 => 변경시 필요한 인덱스는 -16뺀 0 부터 13까지임.
            //Debug.Log("Call !!! => " + (currentIndex - characterIndexInterval));
            ChangeCharacterEvent.Invoke(currentIndex - characterIndexInterval);

            //* 죽인 몬스터 및 플레이타임 가져오는 부분 필요
            UpdateInfoEvent.Invoke(Model.cardSettings[currentIndex]);

            Debug.Log("모델 활성 상태 = " + Model.cardSettings[currentIndex].UseAble);

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
                UINotifyYesOrNoPopupPresenter popupPresenter = NotifyYesOrNoPopupOpener.popupPrefab.GetComponent<UINotifyYesOrNoPopupPresenter>();
                //젬 부족 메시지 출력 셋업 -> set a message print 
                popupPresenter.Desicription = "Buy [" + cardSetting.Name + "] for $" + string.Format("{0:0,0}" + ".", cardSetting.GemPrice);
                popupPresenter.type = UINotifyYesOrNoPopupPresenter.Type.PurchaseCharacter;

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
