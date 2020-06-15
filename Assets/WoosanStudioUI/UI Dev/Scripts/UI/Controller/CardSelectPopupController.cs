using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드 셀렉터를 컨트롤함.
    /// </summary>
    public class CardSelectPopupController : MonoBehaviour
    {
        [Header("[콘텐츠의 활성,비활성을 담당하기에 모든 아이템들을 다 가지고 있음]")]
        public AddContent SelectedCardsRoot;
        [Header("[콘텐츠의 활성,비활성을 담당하기에 모든 아이템들을 다 가지고 있음]")]
        public AddContent SelectCardRoot;

        [Header("[선택된 카드 초기화 이미지]")]
        public Sprite DefalutSprite;

        [Header("[선택된 카드 현재 위치]")]
        public int SelectedPivotIndex = 0;

        public int MaxSelectedCard = 10;

        private bool isFull = false;
        public bool IsFull { get; set; }

        public Text SelectedCardText;

        [Header("[(자동)선택된 컨탠츠들]")]
        public List<CardUIController> SelectedCards = new List<CardUIController>();
        [Header("[(자동)선택하는 컨탠츠들]")]
        public List<CardUIController> SelectCards = new List<CardUIController>();

        IEnumerator Start()
        {
            yield return new WaitForSeconds(0.1f);

            //AddContent Initialize()시 일정 시간이 필요함 그래서 0.1초 대기
            //가져올때는 모든 비활성화된 카드 까지도 가져온다는걸 잊지 말
            SelectedCardsRoot.Contents.ForEach(value => SelectedCards.Add(value.GetComponent<CardUIController>()));

            SelectCardRoot.Contents.ForEach(value =>
            {
                CardUIController tmp = value.GetComponent<CardUIController>();
                SelectCards.Add(tmp);
                //클릭이벤트와 CardSelectClickHandler연결
                tmp.CardSelectEvent.AddListener(CardSelectClickHandler);
            });

            //초기화
            Initialize();
        }

        /// <summary>
        /// 선택 카드 이벤트 핸들러
        /// </summary>
        public void UpdateSelectedCardsHandler()
        {
            MaxSelectedCard++;
            //최대 갯수가 추가 됬기에 풀 체크 해
            isFull = false;

            //실제 활성화된 자식 숫자
            int activedChildCount = 0;

            for (int index = 0; index < SelectedCardsRoot.transform.childCount; index++)
            {
                if (SelectedCardsRoot.transform.GetChild(index).gameObject.activeSelf)
                {
                    activedChildCount++;
                }
            }
            Debug.Log("Child Count = " + activedChildCount);

            //유효성 체크 => 실제 활성화된 카드 와 숫자상의 실제 카드숫자가 맞는지 확인
            if(MaxSelectedCard != activedChildCount)
            {
                Debug.Log("실제 카드와 수치상 카드가 다릅니다 확인해주세요!!");
            }

            //텍스트 업데이트
            SetSelectedCardText(SelectedPivotIndex, MaxSelectedCard);
        }


        /// <summary>
        /// 최초 시작시 초기화 루틴
        /// **실제 구현때는 초기 세팅 값이 들어있어야 하지만 테스트를 위해 지금은 완전 초기화
        /// </summary>
        private void Initialize()
        {
            //선택된 카드 기본이미지로 초기화
            SelectedCards.ForEach(value => { value.SetSprite(DefalutSprite); });
        }

        /// <summary>
        /// 선택된 카드의 갯수 및 최대 갯수 표시
        /// </summary>
        /// <param name="currentCnt"></param>
        /// <param name="maxCount"></param>
        void SetSelectedCardText(int currentCnt , int maxCount)
        {
            SelectedCardText.text = SelectedPivotIndex + " / " + MaxSelectedCard;
        }

        /// <summary>
        /// 선택한 카드 되돌리기.
        /// </summary>
        public void Undo()
        {
            Debug.Log("Undo");

            //언두 인덱스는 선택 피봇보다 -1 이며 0보다 작을수 없다.
            int undoIndex = SelectedPivotIndex - 1;
            if (undoIndex < 0) undoIndex = 0;

            //선택되서 변경된 색을 원복 시키기
            CardUIController tmp = FindCard(SelectCards, SelectedCards[undoIndex].Image.sprite.name);
            //취소될 슬롯이 존제 할때만
            if (tmp != null) {
                tmp.Unselected();
                //자동도으로 스크롤뷰 옮겨줌
                AutoScollMover(tmp);
            }

            //기본 이미지로 교체
            SelectedCards[undoIndex].SetSprite(DefalutSprite);

            Debug.Log("image = " + SelectedCards[undoIndex].Image.sprite.name);

            //인덱스 감소
            SelectedPivotIndex--;

            //0이하는 존재 할수 없음. 0으로 만들기
            if (SelectedPivotIndex < 0) { SelectedPivotIndex = 0; }

            //텍스트 업데이트
            SetSelectedCardText(SelectedPivotIndex, MaxSelectedCard);

            //꽉차 있었다면 false 
            isFull = false;
        }

        /// <summary>
        /// 카드 셀렉터를 선택했을때 콜백 핸들
        /// </summary>
        /// <param name="sprite"></param>
        public void CardSelectClickHandler(Sprite sprite)
        {
            //중복이 있는지 확인
            CardUIController tmp = FindCard(SelectedCards, sprite.name);
            if (tmp != null)
            {
                //해당 카드 깜밖임.
                tmp.Blick();
                Debug.Log("중복된 카드가 존제한다.");

                AutoScollMover(tmp);

                return;
            }

            //카드가 꽉찾는지 확인
            if (isFull) {
                Debug.Log("카드가 꽉차서 선택 불가");
                //알림창 열기
                NotifyPopupController.Instance.Open();
                return;
            }

            //해당 이미지 변ㄱ
            SelectedCards[SelectedPivotIndex].SetSprite(sprite);

            //선택된 카드 색변경
            tmp = FindCard(SelectCards, sprite.name);
            tmp.Selected();

            //자동도으로 스크롤뷰 옮겨줌
            AutoScollMover(tmp);

            //선택된 카드 피봇 증가
            SelectedPivotIndex++;

            if(SelectedPivotIndex >= MaxSelectedCard)
            {
                isFull = true;
                SelectedPivotIndex = MaxSelectedCard;
            }

            //텍스트 업데이트
            SetSelectedCardText(SelectedPivotIndex, MaxSelectedCard);
        }


        /// <summary>
        /// 중복된 아이템 있는지 체크
        /// </summary>
        /// <param name="selectedCards"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public CardUIController FindCard(List<CardUIController> selectedCards , string name)
        {
            CardUIController tmp = null;
            selectedCards.ForEach(value => {
                if (value.Image.sprite.name.Equals(name)) { tmp = value; }
            });

            return tmp;
        }

        /// <summary>
        /// 자동으로 스크롤뷰 옮겨줌
        /// </summary>
        /// <param name="tmp"></param>
        void AutoScollMover(CardUIController tmp)
        {
            /*
            RectTransform rt = tmp.GetComponent<RectTransform>();
            RectTransform contentRoot = SelectedCardsRoot.GetComponent<RectTransform>();

            Vector3 pos;
            pos = contentRoot.position;
            Debug.Log("before  x = " + pos.x + "  y = " + pos.y + " z = " + pos.z);
            pos.x = -rt.position.x;
            contentRoot.position = pos;
            Debug.Log("after x = " + pos.x + "  y = " + pos.y + " z = " + pos.z);
            Debug.Log("anchored x = " + contentRoot.anchoredPosition.x + "   anchored y = " + contentRoot.anchoredPosition.y);
            */

            RectTransform rt = tmp.GetComponent<RectTransform>();
            RectTransform contentRoot = SelectedCardsRoot.GetComponent<RectTransform>();

            //실제 활성화된 자식 숫자
            int activedChildCount = 0;

            for(int index = 0; index < SelectedCardsRoot.transform.childCount;index++ )
            {
                if(SelectedCardsRoot.transform.GetChild(index).gameObject.activeSelf)
                {
                    activedChildCount++;
                }   
            }
            Debug.Log("Child Count = " + activedChildCount);

            //현재는 X기준만 작업됨 
            //y 기준을 추가하려면 추가 작업 필요
            GridLayoutGroup gridLayoutGroup = SelectedCardsRoot.GetComponent<GridLayoutGroup>();

            float gab = (gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x)/2;
            //Debug.Log(gab);

            int findIndex = SelectedCards.FindIndex(0, SelectedCards.Count, value => value.Image.sprite.name.Equals(tmp.Image.sprite.name));

            float finalDistance = 0;
            float minDistance = 0;
            float maxDistance = 0;

            //(현재 선택 할수있는 전체 슬롯 크기 - 화면에 보이는 최대 갯수 7개) * (슬롯 절반 크기)
            maxDistance = (MaxSelectedCard - 7) * (gab);
            minDistance = -((MaxSelectedCard - 7) * (gab));

            //홀짝에 의해 구분하려 했으나 의미 없어서 하나로 통합

            //(전체 갯수의 절반 - 현재 선택된 인덱스) * (갭*2)    
            finalDistance = ((activedChildCount / 2) - findIndex) * (gab * 2);
            Debug.Log("절반 = " + (activedChildCount/2) + " 현재 인덱스 = " + findIndex);

            //실제 나온 값이 최대 제한 값보다 크거나 작으면 제한된 값으로 실행
            if (finalDistance > maxDistance) { finalDistance = maxDistance; }
            if (finalDistance < minDistance) { finalDistance = minDistance; }

            Vector2 pos;
            //실제 앵커값에 값 적용하여 이동시킴.
            pos = contentRoot.anchoredPosition;
            pos.x = finalDistance;
            contentRoot.anchoredPosition = pos;
        }
    }
}