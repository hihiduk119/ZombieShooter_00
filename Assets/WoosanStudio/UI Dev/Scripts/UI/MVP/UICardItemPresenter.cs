using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

using DG.Tweening;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드 아이템 프레젠터
    /// *MVP 모델
    /// </summary>
    public class UICardItemPresenter : MonoBehaviour , IPointerClickHandler
    {
        //UICardInfoPopupPresenter.Awake()에서 넣어줌
        [Header("[[Auto->Awake()] 카드 참조]")]
        public CardSetting CardSetting;

        [Header("[[Auto->Awake()] MVP View]")]
        public UICardItemView View;


        //카드 아이템 클릭시 이벤트
        public class CardItemReturnMeEvent : UnityEvent<UICardItemPresenter> { }
        [Header("[크릭한 나 자신을 돌려주는 이벤트]")]
        public CardItemReturnMeEvent ReturnMeEvent = new CardItemReturnMeEvent();

        //카드 아이템 클릭시 이벤트
        public class CardItemClickEvent : UnityEvent<CardSetting> {}
        [Header("[카드 아이템 클릭시 이벤트]")]
        public CardItemClickEvent ClickEvent = new CardItemClickEvent();

        [Header("[[Auto->Awake()] 카드정보 프리젠터]")]
        public UICardSlotInfoPresenter Presenter;

        [Header("[카드선택 이벤트]")]
        public SelectCardItemEvent SelectEvent = new SelectCardItemEvent();
        public class SelectCardItemEvent : UnityEvent<string> { };

        private void Awake()
        {
            //View = GameObject.FindObjectOfType<UICardSlotInfoView>();
            Presenter = GameObject.FindObjectOfType<UICardSlotInfoPresenter>();

            //나의 트랜스폼에서 찾는다
            View = this.GetComponent<UICardItemView>();

            //UICardInfoView 의 리스너 등록
            ClickEvent.AddListener(Presenter.UpdateInfo);
        }

        /// <summary>
        /// 클릭 이벤트
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            //선택 호출
            Selected();
        }

        /// <summary>
        /// 카드 뷰 업데이트
        /// </summary>
        /// <param name="cardSetting"></param>
        public void UpdateInfo(CardSetting cardSetting)
        {
            //초반 카드 정보 세팅 부분
            //*UICardInfoPopupPresenter.Awake()에서 작업하게 이동
            //CardSetting = cardSetting;
            //카드 정보 뷰 호출
            View.UpdateInfo(cardSetting);
        }

        /// <summary>
        /// 아이템 선택
        /// </summary>
        public void Selected()
        {
            //Debug.Log(CardSetting.Name + " 클릭됨");
            //클릭 이벤트 발생
            ClickEvent.Invoke(CardSetting);

            //나 자신을 넣어서 보냄
            ReturnMeEvent.Invoke(this);

            //선택 이벤트 발생
            SelectEvent.Invoke(CardSetting.Name);

            //뷰 연출
            View.Selected();
        }

        /// <summary>
        /// 선택 릴리즈
        /// </summary>
        public void Release()
        {
            //릴리즈 연출
            View.Release();
        }

        /*
        /// <summary>
        /// 카드 뷰 업데이트
        /// </summary>
        /// <param name="cardSetting"></param>
        public void UpdateInfo(CardSetting cardSetting )
        {
            CardSetting = cardSetting;

            Image.sprite = cardSetting.Sprite;
            //이미지에 따라 사이즈 재정의
            float width = Image.sprite.rect.width/6;
            float height = Image.sprite.rect.height/6;
            Image.rectTransform.sizeDelta = new Vector2(width, height);
            //1 더하는 이유는 레벨이 0부터 시작이라서
            Level.text = (cardSetting.Level + 1).ToString();


            Image.color = cardSetting.IconColor;
            Name.text = cardSetting.Name;

            //언락이 풀렸다면 사용 가능
            if(!cardSetting.UseAble) { Lock.gameObject.SetActive(true); }
            //언락이 안풀렸다면 사용 불가
            else { Lock.gameObject.SetActive(false);}
            UnlockLevel.text = (cardSetting.UnlockLevel+1).ToString();
        }*/
    }
}
