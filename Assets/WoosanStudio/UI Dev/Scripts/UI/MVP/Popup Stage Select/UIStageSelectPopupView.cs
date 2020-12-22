using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 스테이지 선택 팝업
    ///  *MVP패턴
    /// </summary>
    public class UIStageSelectPopupView : MonoBehaviour
    {
        [Header("[스크롤을 제어하는 컨트롤러]")]
        public ScrollRect ScrollRect;
        [Header("[콘텐츠 추가시 자동으로 이동 길이 계산하기 위해 사용.]")]
        public GridLayoutGroup GridLayoutGroup;
        [Header("[콘텐츠 갯수을 알기위해 사용]")]
        public Transform ContentRoot;
        [Header("[맵 갯수]")]
        public Text TextAmount;
        [Header("[현재 선택된 맵 인덱스]")]
        public int CurrentIndex = 0;
        [Header("[별점 루트]")]
        public Transform StarRoot;
        [Header("[별점]")]
        public List<Image> Stars = new List<Image>();
        [Header("[별점 획득 컬러]")]
        public Color gainColor;
        [Header("[별점 비획득 컬러]")]
        public Color noneColor;

        //캐슁용
        int contentCount = 0;
        float snapMargin = 0;

        Coroutine coroutineMoveScroll;

        //콘텐츠 하나의 사이즈
        Vector2 cellSize;
        //콘텐츠 사이의 간격
        Vector2 spacing;

        IEnumerator Start()
        {
            //0으로 초기화
            MoveScroll(0);

            yield return new WaitForSeconds(0.05f);

            //이동간격 계산
            CalculateSnapMargin();
            /// 맵 글자표시 업데이트
            //UpdateText();
        }

        /// <summary>
        /// 맵 글자표시 업데이트
        /// </summary>
        public void UpdateText(int stage,int maxStage)
        {
            TextAmount.text = stage.ToString() + " / " + maxStage;
        }

        /// <summary>
        /// 이동 간격을 계산합니다.
        ///
        /// 자식 갯수가 변하면 재업데이트 필요 합니다.
        /// </summary>
        public void CalculateSnapMargin()
        {
            //자식의 갯수 가져오기
            contentCount = GetActivedChildCount();

            //마진은 컨텐츠 전체 겟수에서 -1뺀 값이다.
            //이동을 위한 1 step 마진
            snapMargin = 1f / (contentCount - 1);

            //Debug.Log(" snapMargin = " + snapMargin + "  contentCount = " + contentCount);
        }

        /// <summary>
        /// 활성화된 자식의 갯수 가져오기
        /// ???사용 안함
        /// </summary>
        /// <returns></returns>
        public int GetActivedChildCount()
        {
            int count = 0;

            for (int i = 0; i < ContentRoot.childCount; i++)
            {
                if (ContentRoot.GetChild(i).gameObject.activeSelf)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// 실제 움직인 값을 알기 위해 사용
        /// </summary>
        /// <param name="value"></param>
        public void Moved(Vector2 value)
        {
            //Debug.Log("X = " + value.x + "  Y = " + value.y);
        }

        /// <summary>
        /// 실제 거리 만큼 이동
        /// </summary>
        /// <param name="value"></param>
        //public void MoveScroll(float value)
        //{
        //    if (coroutineMoveScroll != null) StopCoroutine(coroutineMoveScroll);
        //    coroutineMoveScroll = StartCoroutine(CorMoveScroll(value));
        //}

        /// <summary>
        /// 인덱스 만큼 이동
        /// </summary>
        /// <param name="value"></param>
        public void MoveScroll(int index)
        {
            float value = index * snapMargin;

            if (coroutineMoveScroll != null) StopCoroutine(coroutineMoveScroll);
            coroutineMoveScroll = StartCoroutine(CorMoveScroll(value));
        }

        /// <summary>
        /// 스크롤 움직이는 코루틴
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IEnumerator CorMoveScroll(float value)
        {
            WaitForEndOfFrame WFEF = new WaitForEndOfFrame();
            float deltaTime = 0;

            while (deltaTime < 1f)
            {
                deltaTime += Time.deltaTime;
                ScrollRect.horizontalNormalizedPosition = Mathf.Lerp(ScrollRect.horizontalNormalizedPosition, value, 0.5f);

                //Debug.Log("horizontalNormalizedPosition" + ScrollRect.horizontalNormalizedPosition);
                //Debug.Log(deltaTime);

                yield return WFEF;
            }
        }

        /// <summary>
        /// 왼쪽으로 스크롤뷰 이동 클릭
        /// </summary>
        public void LeftClick()
        {
            CurrentIndex--;
            if (CurrentIndex <= 0) CurrentIndex = 0;

            //Debug.Log("count = " + count + "    snapMargin = " + snapMargin + "     value=" + (count * snapMargin));
            MoveScroll(CurrentIndex);
        }

        /// <summary>
        /// 오른쪽으로 스크롤뷰 이동 클릭
        /// </summary>
        public void RightClick()
        {
            CurrentIndex++;
            if (CurrentIndex >= GetActivedChildCount() - 1) CurrentIndex = GetActivedChildCount() - 1;

            //Debug.Log("count = " + count + "    snapMargin = " + snapMargin + "     value=" + (count * snapMargin));
            MoveScroll(CurrentIndex);
        }

        /// <summary>
        /// 별점 세팅
        /// </summary>
        /// <param name="count">별 갯수 만큼 노랑 색 칠. -1은 스테이지 미완료.</param>
        public void SetStar(int count = -1)
        {
            //시작시 별 활성화
            StarRoot.gameObject.SetActive(true);

            //별 없음 -> 스테이지 미완료.
            if (count == -1)
            {
                //별 비활성화
                StarRoot.gameObject.SetActive(false);
            } else
            {
                for (int i = 0; i < 3; i++)
                {
                    if(i < count) //노랑 스타
                    {
                        Stars[i].color = gainColor;
                    } else      //회색 스타
                    {
                        Stars[i].color = noneColor;
                    }
                }
            }
        }

        #region [-TestCode]
        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        LeftClick();
        //        UpdateText();
        //    }
        //    if (Input.GetKeyDown(KeyCode.S))
        //    {
        //        RightClick();
        //        UpdateText();
        //    }
        //    if (Input.GetKeyDown(KeyCode.Alpha1))
        //    {
        //        MoveScroll(0);
        //        UpdateText();
        //    }
        //    if (Input.GetKeyDown(KeyCode.Alpha2))
        //    {
        //        MoveScroll(1);
        //        UpdateText();
        //    }
        //    if (Input.GetKeyDown(KeyCode.Alpha3))
        //    {
        //        MoveScroll(2);
        //        UpdateText();
        //    }
        //}
        #endregion 
    }
}
