using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드 아이템 뷰
    /// *MPV 모델
    /// </summary>
    public class UICardItemView : MonoBehaviour , IPointerClickHandler
    {
        [Header("[카드 이미지]")]
        public Image Image;

        [Header("[카드 이름]")]
        public Text Name;

        [Header("[카드 레벨]")]
        public Text Level;

        [Header("[카드 레벨 락]")]
        public Transform Lock;

        [Header("[카드 언락 레벨]")]
        public Text UnlockLevel;

        [Header("[카드 참조]")]
        public CardSetting CardSetting;

        //카드 아이템 클릭시 이벤트
        public class CardItemClickEvent : UnityEvent<CardSetting>{}
        [Header("[카드 아이템 클릭시 이벤트]")]
        public CardItemClickEvent ClickEvent = new CardItemClickEvent();

        //*이게 여기있으먄 인됨 -> Presenter에 있어야 함. 변경 요망
        [Header("[[Auto->Awake()] 카드정보 뷰]")]
        public UICardInfoView View;

        private void Awake()
        {
            View = GameObject.FindObjectOfType<UICardInfoView>();

            //UICardInfoView 의 리스너 등록
            ClickEvent.AddListener(View.UpdateInfoListener);
        }

        /// <summary>
        /// 클릭 이벤트
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            //Debug.Log(CardSetting.Name + " 클릭됨");
            ClickEvent.Invoke(CardSetting);
        }

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

            Image.color = cardSetting.IconColor;
            Name.text = cardSetting.Name;
            //사용자 언락
            if(cardSetting.UseAble) { Lock.gameObject.SetActive(true); }
            else { Lock.gameObject.SetActive(false);}
            UnlockLevel.text = cardSetting.UnlockLevel.ToString();
        }
    }
}
