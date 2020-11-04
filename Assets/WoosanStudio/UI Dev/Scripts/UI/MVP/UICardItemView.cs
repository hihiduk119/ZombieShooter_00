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
    /// 카드 아이템 뷰
    /// *MVP 모델
    /// </summary>
    public class UICardItemView : MonoBehaviour , IPointerClickHandler
    {
        [Header("[카드 백그라운드]")]
        public Image Background;

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
        public class CardItemClickEvent : UnityEvent<CardSetting> {}
        [Header("[카드 아이템 클릭시 이벤트]")]
        public CardItemClickEvent ClickEvent = new CardItemClickEvent();

        //*이게 여기있으먄 인됨 -> Presenter에 있어야 함. 변경 요망
        //[Header("[[Auto->Awake()] 카드정보 뷰]")]
        //public UICardSlotInfoView View;
        [Header("[[Auto->Awake()] 카드정보 프리젠터]")]
        public UICardSlotInfoPresenter Presenter;

        [Header("[카드선택 이벤트]")]
        public SelectCardItemEvent SelectEvent = new SelectCardItemEvent();
        public class SelectCardItemEvent : UnityEvent<string> { };

        private void Awake()
        {
            //View = GameObject.FindObjectOfType<UICardSlotInfoView>();
            Presenter = GameObject.FindObjectOfType<UICardSlotInfoPresenter>();

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
        /// 아이템 선택
        /// </summary>
        public void Selected()
        {
            Debug.Log(CardSetting.Name + " 클릭됨");
            //클릭 이벤트 발생
            ClickEvent.Invoke(CardSetting);

            //파랑색으로 요요 트윈
            //Background.DOColor(new Color(32,159,194), 0.5f).SetLoops(-1, LoopType.Yoyo);

            Background.DOColor(Color.gray, 0.75f).SetLoops(-1, LoopType.Yoyo);

            //Background.color = Color.blue;
            //선택 이벤트 발생
            SelectEvent.Invoke(Name.text);
        }

        /// <summary>
        /// 선택 릴리즈
        /// </summary>
        public void Release()
        {
            //트윈 정지 및 컬러 초기화
            Background.DOKill();
            Background.color = Color.white;
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
            //1 더하는 이유는 레벨이 0부터 시작이라서
            Level.text = (cardSetting.Level + 1).ToString();


            Image.color = cardSetting.IconColor;
            Name.text = cardSetting.Name;

            //언락이 풀렸다면 사용 가능
            if(!cardSetting.UseAble) { Lock.gameObject.SetActive(true); }
            //언락이 안풀렸다면 사용 불가
            else { Lock.gameObject.SetActive(false);}
            UnlockLevel.text = cardSetting.UnlockLevel.ToString();
        }
    }
}
