using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;
using DG.DemiLib;

namespace WoosanStudio.ZombieShooter
{
    [Serializable]
    public class CardSelectEvent : UnityEvent<Sprite> { }
    /// <summary>
    /// 카드 UI 컨트롤러
    /// </summary>
    public class CardUIController : MonoBehaviour , IPointerDownHandler
    {
        [Header("[찾을려는 이미지 아이콘 이름]")]
        public string IconName = "Ability Icon";
        public Image Image;
        public Image Background;

        private Color mBackgroundColor;
        private RectTransform mRectTransform;

        private Color mSelectedColor = new Color32(230, 230, 230,255);
        private Color mUnselectedColor = Color.white;

        [SerializeField]
        public CardSelectEvent CardSelectEvent = new CardSelectEvent();

        private void Awake()
        {
            //아이콘 이미지 찾기
            Transform tmpTransform = transform.Find(IconName);
            Image = tmpTransform.GetComponent<Image>();
            mRectTransform = tmpTransform.GetComponent<RectTransform>();

            //백그라운드 이미지 찾기
            Background = transform.Find("Background").GetComponent<Image>();

            //백그라운드 컬러 미리 저자
            mBackgroundColor = Background.color;
        }

        /// <summary>
        /// 이미지 변경
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="autoSize"></param>
        public void SetSprite(Sprite sprite)
        {
            Image.sprite = sprite;

            //이미지 사이즈 마추기
            mRectTransform.sizeDelta = new Vector2(sprite.rect.width,sprite.rect.height);
        }

        /// <summary>
        /// 백그라운드 깜빡이기
        /// </summary>
        public void Blick()
        {
            Debug.Log("깜빡임");
            Background.DOKill();
            //색 초기화
            Background.color = mBackgroundColor;
            Background.DOColor(new Color(255, 0, 0), 0.1f).SetLoops(2, LoopType.Yoyo).OnComplete(() => { Background.color = mBackgroundColor; }); ;
        }

        /// <summary>
        /// 선택됐음
        /// </summary>
        public void Selected()
        {
            //빨간색으로 변환
            Background.color = mSelectedColor;
        }

        /// <summary>
        /// 선택해제
        /// </summary>
        public void Unselected()
        {
            //흰색으로 복귀 
            Background.color = mUnselectedColor;
        }

        /// <summary>
        /// 클릭 이벤트 발생시 호출
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            //카드 선택 이벤트 호출
            CardSelectEvent.Invoke(Image.sprite);

            Debug.Log("Click");
        }
    }
}
