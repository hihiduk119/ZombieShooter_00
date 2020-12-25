using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter.UI
{
    /// <summary>
    /// Map UI 에서 콘텐츠 추가 삭제 용
    /// </summary>
    public class MapContent : MonoBehaviour
    {
        [Header("[사용할 아이템 갯수]")]
        public int Count = 1;

        [Header("[아이템 셀 사이즈]")]
        public Vector2 CellSize;

        [Header("[스페이스 사이즈]")]
        public Vector2 Spacing;

        //캐쉬
        private RectTransform rectTransform;

        /// <summary>
        /// 갯수만큼 맵 세팅
        /// </summary>
        /// <param name="count"></param>
        public void SetMap(int count)
        {
            Count = count;

            rectTransform = this.GetComponent<RectTransform>();

            //하위 자식 가져오기
            for (int i = 0; i < transform.childCount; i++)
            {
                //사용할 아이템 갯수 만큼만 활성화
                if (i < Count)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }

            //높이 구하기
            float y = CellSize.y + (Spacing.y * 2);
            //가로 구하기
            float x = (CellSize.x * Count) + (Spacing.x * Count + Spacing.x * 2);
            //사이즈 재적용
            rectTransform.sizeDelta = new Vector2(x, y);
        }
    }
}
