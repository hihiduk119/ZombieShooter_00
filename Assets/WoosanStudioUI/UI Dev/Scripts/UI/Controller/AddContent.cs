using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 실제 컨텐츠를 늘리는 계념이 아니라 비활성화 된 컨텐츠를 활성화 시키는 개념
    /// </summary>
    public class AddContent : MonoBehaviour
    {
        //콘텐츠 프리팹
        //public GameObject Prefab;

        [Header("[해당 스크롤 랙 셋업]")]
        public ScrollRect scrollRect;

        [Header("[최초 활성화 컨탠츠 갯수 결정]")]
        //프리팹에서 처음 활성화 시킬 갯수임
        //첫 갯수는 최대로 설정하고 활성화를 줄이는 방향으로 해야함.
        public int CurrentContentCount = 20;

        [Header("[(자동)현재 링크된 모든 컨탠츠들]")]
        public List<Transform> Contents = new List<Transform>();

        [Header("[컨텐츠 추가 이벤트 발생]")]
        public UnityEvent AddEvent = new UnityEvent();

        [SerializeField]
        [Header("[단일 행 사용]")]
        private bool isSingleRow = false;
        public bool IsSingleRow { get; set; }

        //수평 정렬
        private bool mHorizontal;
        //수직 정렬
        private bool mVertical;

        private RectTransform mRectTransform;
        private GridLayoutGroup mGridLayout;

        private int mConstraintCount;

        //실제 존제하는 모든 컨텐츠 갯mMaxContentCount
        private int mMaxContentCount;

        //추가할 콘텐츠 갯수
        //private int addCount = 4;
        private GameObject mClone;
        private int mActivedContentCount = 0;

        //최초 기본 랙트랜스폼 사이즈
        Vector2 defalutSizeDelta;

        private void Start()
        {
            Initialize();
        }

        /// <summary>
        /// 최초 시작시 초기화 루틴
        /// </summary>
        private void Initialize()
        {
            //최초 스크롤 랙에서 수평 수직 체크 가져
            mHorizontal = scrollRect.horizontal;
            mVertical = scrollRect.vertical;

            Debug.Log("Horizontal = " + mHorizontal + "   Vertical = " + mVertical);

            //수직 및 수평 체크 확인
            if (!IsResizeAble())
            {
                Debug.Log("수직 수평 체크 문제 있음");
            }

            Debug.Log("Initialize");
            //
            mRectTransform = GetComponent<RectTransform>();
            mGridLayout = GetComponent<GridLayoutGroup>();

            //처음 컨텐츠 최대 갯수
            mMaxContentCount = transform.childCount;

            //최초 값 알아서 저장
            defalutSizeDelta = mRectTransform.sizeDelta;

            Transform clone;

            //컨텐츠 모두 가져오기
            for (int index = 0; index < transform.childCount; index++)
            {
                clone = transform.GetChild(index);
                Contents.Add(clone);
                clone.gameObject.SetActive(false);
            }


            int activeCount = CurrentContentCount;
            //최초 활성화시 활성 갯수만큼 활성화 시키지만 최대 갯수가 모자라면 최대 갯수 만큼만 활성화
            if (CurrentContentCount > mMaxContentCount)
            {
                activeCount = mMaxContentCount;
            }

            //컨텐츠 갯수 만큼만 활성화 시킴
            for(int index = 0; index < activeCount; index++)
            {
                Contents[index].gameObject.SetActive(true);
            }

            //랙 트랜스폼 값 재계산.
            ReSizeRectTransform(activeCount, scrollRect);
        }


        /// <summary>
        /// 수평 수직 이상 없는지 검
        /// </summary>
        /// <returns></returns>
        private bool IsResizeAble()
        {
            if (mHorizontal == true && mVertical == true)
            {
                Debug.Log("수평, 수직 두개 모두 true는 지원하지 않습니다.");
                return false;
            }

            if (!mHorizontal && !mVertical)
            {
                Debug.Log("수평, 수직 두개 모두 false는 지원하지 않습니다.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 실제 랙 트랜스폼 사이즈를 재 계산한다.
        /// </summary>
        /// <param name="contentCount"></param>
        /// <param name="scrollRect"></param>
        private void ReSizeRectTransform(int contentCount, ScrollRect scrollRect)
        {
            Debug.Log("ReSizeRectTransform");
            //실제 트랜스 폼의 길이 적용해서 적용.
            float gab;
            Vector2 sizeDelta = new Vector2(0, 0);

            if (isSingleRow)//단일 행이라면 1곱하여 처음수 그대로 적용
            {
                mConstraintCount = 1;
            }
            else//행 계산시 나누기 할 행당 최대 갯수
            {
                mConstraintCount = mGridLayout.constraintCount;
            }
                

            sizeDelta = mRectTransform.sizeDelta;


            //나마지가 존제하고 1이하면
            int odd = contentCount % mConstraintCount;

            //수직 계산 
            if (mVertical)
            {
                //Debug.Log(" 수직계산 현재 갯수 = "+ contentCount + "    행 갯수 = " + mConstraintCount + "    나머지 = " + odd);
                if (odd == 0) //나머지가 0일때
                {
                    gab = (mGridLayout.cellSize.y + mGridLayout.spacing.y) * (contentCount / mConstraintCount);
                }
                else //나머지가 1이상일때
                {
                    gab = (mGridLayout.cellSize.y + mGridLayout.spacing.y) * ((contentCount / mConstraintCount) + 1);
                }

                Debug.Log("최종 간격 = " + gab + "  cell.y = " + mGridLayout.cellSize.y + "  space.y = " + mGridLayout.spacing.y
                    + "   contentCount = " + contentCount + "  mConstraintCount = " + mConstraintCount);

                //최초 기본 y 보다 클때만 적용 => 작을때 적용하면 그리드 간격이 안맞아짐
                if (defalutSizeDelta.y < gab)
                {
                    sizeDelta.y = gab;
                }
            }

            //수평 계산
            if (mHorizontal)
            {
                Debug.Log(" 수평계산 현재 갯수 = " + contentCount + "    행 갯수 = " + mConstraintCount + "    나머지 = " + odd);
                if (odd == 0) //나머지가 0일때
                {
                    gab = (mGridLayout.cellSize.x + mGridLayout.spacing.x) * (contentCount / mConstraintCount);
                }
                else //나머지가 1이상일때
                {
                    gab = (mGridLayout.cellSize.x + mGridLayout.spacing.x) * ((contentCount / mConstraintCount) + 1);
                }

                Debug.Log("gab = " + gab + "  cell.x = " + mGridLayout.cellSize.x + "  space.x = " + mGridLayout.spacing.x
                    + "   contentCount = " + contentCount + "  mConstraintCount = " + mConstraintCount);

                //Debug.Log("defalutSizeDelta.x  = " + defalutSizeDelta.x + "  gab = " + gab);
                //최종값에 spacing.x 마진을 붙여 끝부분이 딱맞아 떨어지는걸 방지한다.
                gab += mGridLayout.spacing.x;

                //최초 기본 x 보다 클때만 적용 => 작을때 적용하면 그리드 간격이 안맞아짐
                if (defalutSizeDelta.x < gab)
                {
                    sizeDelta.x = gab;
                }
            }

            Debug.Log("x= " + sizeDelta.x + "  y =" + sizeDelta.y);

            //실제 트랜스폼 적용
            mRectTransform.sizeDelta = sizeDelta;
        }


        /// <summary>
        /// 해당 겟수만큼 콘텐츠 활성화
        /// </summary>
        public void Resize(int addCount,ScrollRect scrollRect)
        {
            int MaxCount = addCount + CurrentContentCount;


            Debug.Log("addCount = " + addCount + "   CurrentContentCount = " + CurrentContentCount);
                
            if (MaxCount > mMaxContentCount)
            {
                Debug.Log("MaxCount = " + MaxCount + "  mMaxContentCount = " + mMaxContentCount);
                Debug.Log("최대 컨텐츠를 초과 했습니다.");
                return;
            }

            //추기된 컨텐츠 만큼 활성화    
            for (int index = CurrentContentCount; index < MaxCount; index++)
            {
                Contents[index].gameObject.SetActive(true);
            }
            //현재 컨텐츠 갯수 재설정
            CurrentContentCount += addCount;

            //랙 트랜스폼 재계산
            ReSizeRectTransform(CurrentContentCount, scrollRect);

            //실제 이벤트 발생
            AddEvent.Invoke();
        }


        #region [-TestCode]
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                //콘텐츠 추가
                Resize(1, scrollRect);
            }
        }
        #endregion
    }
}
