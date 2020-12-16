using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

namespace WoosanStudio.ZombieShooter.UI.MVP.InGameCardSelect
{
    /// <summary>
    /// 플레이어 선택 아이템
    /// *MVP 모델
    /// </summary>
    public class PlayerItemView : MonoBehaviour
    {
        [Header("[카드 이미지]")]
        public Image Icon;

        [Header("[카드 이름]")]
        public Text Name;

        [Header("[왼쪽 버튼]")]
        public GameObject LeftButton;

        [Header("[오른쪽 버튼]")]
        public GameObject RightButton;
        //클릭 이벤트
        public class ClickEvent : UnityEvent<int> { }
        //버튼 클릭에 의한 이벤트 발생
        public ClickEvent LeftEvent = new ClickEvent();
        public ClickEvent RightEvent = new ClickEvent();

        //캐쉬용
        private CanvasGroup leftButtonCanvasGroup;
        private CanvasGroup rightButtonCanvasGroup;

        private void Awake()
        {
            //캐쉬 보관용
            leftButtonCanvasGroup = LeftButton.GetComponent<CanvasGroup>();
            rightButtonCanvasGroup = RightButton.GetComponent<CanvasGroup>();
        }

        /// <summary>
        /// 이미지 업데이트
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="color"></param>
        public void UpdateView(Sprite sprite, Color color ,string name)
        {
            //아이콘 설정
            Icon.sprite = sprite;
            //이미지에 따라 사이즈 재정의
            float width = sprite.rect.width * 0.2f;
            float height = sprite.rect.height * 0.2f;
            Icon.rectTransform.sizeDelta = new Vector2(width, height);
            //이미지 컬러 세팅
            Icon.color = color;
            //이름 설정
            Name.text = name;
        }

        /// <summary>
        /// 왼쪽 클릭
        /// </summary>
        public void ClickLeft()
        {
            LeftEvent.Invoke(-1);
        }

        /// <summary>
        /// 오른쪽 클릭
        /// </summary>
        public void ClickRight()
        {   
            RightEvent.Invoke(1);
        }

        /// <summary>
        /// 왼쪽 버튼 활성/비활성
        /// </summary>
        public void SetActiveLeftButton(bool value)
        {
            //트윈 비활성
            LeftButton.transform.DOKill();
            LeftButton.transform.localScale = Vector3.one;

            if (value)       //활성
            {
                //트윈 활성
                LeftButton.transform.DOScale(1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);

                //클릭 풀기
                leftButtonCanvasGroup.alpha = 1f;
                leftButtonCanvasGroup.interactable = true;
            } else          //비활성
            {
                //반투명 및 클릭 막기
                leftButtonCanvasGroup.alpha = 0.3f;
                leftButtonCanvasGroup.interactable = false;
            }
        }

        /// <summary>
        /// 오른쪽 버튼 활성/비활성
        /// </summary>
        public void SetActiveRightButton(bool value)
        {
            //트윈 비활성
            RightButton.transform.DOKill();
            RightButton.transform.localScale = Vector3.one;

            if (value)       //활성
            {
                //트윈 활성
                RightButton.transform.DOScale(1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);

                //클릭 풀기
                rightButtonCanvasGroup.alpha = 1f;
                rightButtonCanvasGroup.interactable = true;
            }
            else          //비활성
            {
                //반투명 및 클릭 막기
                rightButtonCanvasGroup.alpha = 0.3f;
                rightButtonCanvasGroup.interactable = false;
            }
        }
    }
}
