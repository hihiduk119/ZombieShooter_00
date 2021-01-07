using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter.UI.MVP.InGameCardSelect
{
    /// <summary>
    /// 카드 선택 아이템
    /// *MVP 모델
    /// </summary>
    public class ItemPresenter : MonoBehaviour
    {
        /// <summary>
        /// 아이템 상태
        /// </summary>
        public enum ItemState
        {
            SelectAble,       //선택 가능 상태
            Add,        //추가 가능 상태
            Empty,      //비어있는 상태
        }

        [Header("[MVP View]")]
        public ItemView View;

        [Header("[추가시 가격 -> PopupPresenter에서 설정해줌]")]
        public int AddPrice;

        [Header("[포커스 이벤트]")]
        public FocusEvent FocusedEvent = new FocusEvent();
        public class FocusEvent : UnityEvent<CardSetting> { }

        [Header("[카드 추가 이벤트]")]
        public UnityEvent AddSucceededEvent = new UnityEvent();

        //[Header("[Model의 역활 PupupPresenter애서 세팅해줌]")]
        //캐쉬용
        private CardSetting cardSetting;
        private ItemState itemState;

        private void Awake()
        {
            //View 선택 이벤트와 연결
            View.SelectEvent.AddListener(Select);
            //View 추가 이벤트와 연결
            View.AddEvent.AddListener(Add);
        }

        /// <summary>
        /// 최초 시작시 세팅
        /// *PopupPresenter에서 호출
        /// </summary>
        /// <param name="cardSetting">카드 데이터</param>
        public void Initialize(CardSetting cardSetting, ItemState itemState)
        {
            this.cardSetting = cardSetting;
            this.itemState = itemState;

            View.SetItemState(this.itemState);
            UpdateInfo();
        }

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        public void UpdateInfo()
        {
            View.UpdateView(this.cardSetting.Sprite,this.cardSetting.IconColor,(this.cardSetting.Level+1).ToString(),string.Format("{0:0,0}", this.AddPrice));
        }

        /// <summary>
        /// 포커스 시킴
        /// </summary>
        public void Focus()
        {
            //포커스 됐을 이벤트 발생
            FocusedEvent.Invoke(this.cardSetting);
            //포커스 활성화
            View.SetFocus(true);
        }

        /// <summary>
        /// 언포커스 시킴
        /// </summary>
        public void Unfocus()
        {
            View.SetFocus(false);
        }

        /// <summary>
        /// 클릭
        /// </summary>
        public void Select()
        {
            //포커스 시킴
            Focus();
        }

        /// <summary>
        /// 추가 클릭
        /// </summary>
        public void Add()
        {
            //코인프레젠트 에서 돈빼기 
            if(!GameObject.FindObjectOfType<CoinPresenter>().SubtractCoin(AddPrice))
            {
                //돈없으면 코인 부족 표시
                //NotifyPopupController.Instance.OpenResult(UINotifyPopupModel.Type.NotEnoughCoin);
                //돈없으면 코인 부족 표시
                //GameObject.FindObjectOfType<NotifyPopupController>().OpenResult(UINotifyPopupModel.Type.NotEnoughCoin);

                //코인 팝업 여는 버튼 이벤트 발생
                GameObject.Find("Coins Button").GetComponent<Ricimi.BasicButton>().OnClicked.Invoke();
            } else
            {
                Debug.Log("[Invoke] = " + AddSucceededEvent.GetPersistentEventCount());

                //카드 추가 성공 이벤트
                AddSucceededEvent.Invoke();
            }
        }
    }
}
