using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ricimi;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// Yes or No 확인 최종 통지
    /// *MVP 모델
    /// </summary>
    public class UINotifyYesOrNoPopupPresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UINotifyYesOrNoPopupView View;

        [Header("[Yes 버튼]")]
        public BasicButton YesButton;
        
        [Header("[[Auto => 전달받은 데이터]")]
        public string Desicription;

        //[Header("[버튼 클릭시 연결될 액션]")]
        //public UnityAction ClickYesAction;
        public enum Type
        {
            PurchaseCharacter = 0, //캐릭터 구매
        }

        //구매 타입에 의해 어디로 Click Yes 결과를 보낼지 결정
        public Type type = Type.PurchaseCharacter;

        /// <summary>
        /// 팝업 활성화시 바로 실행하기 위해
        /// </summary>
        private void OnEnable()
        {
            //활성화시 바로 실행
            UpdateInfo(Desicription);

            //Yes버튼 클릭 이벤트 발생시 호출될 액션 연결
            //YesButton.OnClicked.AddListener(ClickYesAction);
        }

        /// <summary>
        /// 삭제시 모든 리스너 등록 해제
        /// </summary>
        private void OnDestroy()
        {
            YesButton.OnClicked.RemoveAllListeners();
        }

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        public void UpdateInfo(string description)
        {
            View.UpdateView(description);
        }

        /// <summary>
        /// Yes클릭
        /// </summary>
        public void ClickYes()
        {
            switch(type)
            {
                case Type.PurchaseCharacter://캐릭터 구매
                    //구매 요청 실행
                    //* 여기서 동작을 안하는 이유는 데이터는 모두 UIPlayerPresenter가지고 있기 때문에.
                    GameObject.FindObjectOfType<UIPlayerPresenter>().PurchaseCharacter();
                    break;
            }
        }
    }
}
