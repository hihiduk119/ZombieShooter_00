using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter.UI.MVP.InGameCardSelect
{
    /// <summary>
    /// 카드 선택 아이템
    /// *MVP 모델
    /// </summary>
    public class ItemView : MonoBehaviour
    {
        [Header("[선택됨")]
        public GameObject Selected;

        [Header("[안 선택됨]")]
        public GameObject UnSelected;

        [Header("[사용 가능]")]
        public GameObject Able;

        [Header("[추가]")]
        public GameObject Add;

        [Header("[비었음]")]
        public GameObject Empty;

        [Header("[카드 이미지]")]
        public Image Icon;

        [Header("[카드 레벨]")]
        public Text Level;

        [Header("[추가 가격]")]
        public Text Price;

        /// <summary>
        /// 아이템 상태
        /// </summary>
        public enum ItemState
        {
            Able,       //선택 가능 상태
            Add,        //추가 가능 상태
            Empty,      //비어있는 상태
        }

        /// <summary>
        /// 화면 업데이트
        /// </summary>
        /// <param name="sprite">카드 이미지</param>
        /// <param name="level">카드 레벨</param>
        /// <param name="price">추가 가격</param>
        public void UpdateView(Sprite sprite,string level,string price)
        {
            Icon.sprite = sprite;
            Level.text = level;
            Price.text = price;
        }

        /// <summary>
        /// 선택 비선택 상태 변경
        /// </summary>
        /// <param name="value"></param>
        public void SetSelective(bool select)
        {
            //일단 모두 비활성화
            Selected.SetActive(false); UnSelected.SetActive(false);
            //선택
            if (select) { Selected.SetActive(true); } else { UnSelected.SetActive(true); }
        }

        /// <summary>
        /// 아이템 상태 설정
        /// </summary>
        /// <param name="state"></param>
        public void SetItemState(ItemState state)
        {
            //모든 아이템 셋 비활성
            Able.SetActive(false); Add.SetActive(false); Empty.SetActive(false);
            //해당 셋 활성화
            switch (state)
            {
                case ItemState.Able: Able.SetActive(true); break;
                case ItemState.Add: Add.SetActive(true); break;
                case ItemState.Empty: Empty.SetActive(true); break;
                default: break;
            }
        }
    }
}
