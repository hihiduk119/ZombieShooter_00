using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter.UI.MVP.InGameCardSelect
{
    /// <summary>
    /// 카드 선택 아이템
    /// *MVP 모델
    /// </summary>
    public class ItemView : MonoBehaviour
    {
        [Header("[사용 가능]")]
        public GameObject ObjSelect;

        [Header("[선택됐을때 이펙트]")]
        public GameObject FocusEffect;

        [Header("[추가]")]
        public GameObject ObjAdd;

        [Header("[비었음]")]
        public GameObject ObjEmpty;

        [Header("[카드 이미지]")]
        public Image Icon;

        [Header("[카드 레벨 슬라이더]")]
        public Image Slider;

        [Header("[카드 퍼센트")]
        public Text Percent;

        //[Header("[카드 레벨]")]
        //public Text Level;

        [Header("[추가 가격]")]
        public Text Price;

        //버튼 클릭에 의한 이벤트 발생
        public UnityEvent SelectEvent = new UnityEvent();
        public UnityEvent AddEvent = new UnityEvent();

        

        /// <summary>
        /// 화면 업데이트
        /// </summary>
        /// <param name="sprite">카드 이미지</param>
        /// <param name="level">카드 레벨</param>
        /// <param name="price">추가 가격</param>
        public void UpdateView(Sprite sprite,Color color,string level,string price ,string percent, float fillAmount)
        {
            //아이콘 설정
            Icon.sprite = sprite;
            //이미지에 따라 사이즈 재정의
            float width = sprite.rect.width;
            float height = sprite.rect.height;
            Icon.rectTransform.sizeDelta = new Vector2(width, height);
            //이미지 컬러 세팅
            Icon.color = color;
            ////레벨 세팅
            //Level.text = level;
            //가격 세팅
            Price.text = price;

            //카드 레벨 슬라이더 세팅
            Slider.fillAmount = fillAmount;

            //카드 퍼센트 세팅
            Percent.text = percent;
        }

        /// <summary>
        /// 선택 비선택 상태 변경
        /// *선택된 상황에서만 작동됨
        /// </summary>
        /// <param name="value"></param>
        public void SetFocus(bool select)
        {
            //선택 이펙트 활성화
            FocusEffect.SetActive(select);
        }

        /// <summary>
        /// 아이템 상태 설정
        /// </summary>
        /// <param name="state"></param>
        public void SetItemState(ItemPresenter.ItemState state)
        {
            //모든 아이템 셋 비활성
            ObjSelect.SetActive(false); ObjAdd.SetActive(false); ObjEmpty.SetActive(false);
            //해당 셋 활성화
            switch (state)
            {
                case ItemPresenter.ItemState.SelectAble: ObjSelect.SetActive(true); break;
                case ItemPresenter.ItemState.Add: ObjAdd.SetActive(true); break;
                case ItemPresenter.ItemState.Empty: ObjEmpty.SetActive(true); break;
                default: break;
            }
        }

        /// <summary>
        /// 클릭
        /// </summary>
        public void Select()
        {
            SelectEvent.Invoke();
        }

        /// <summary>
        /// 추가 클릭
        /// </summary>
        public void Add()
        {
            AddEvent.Invoke();
        }
    }
}
