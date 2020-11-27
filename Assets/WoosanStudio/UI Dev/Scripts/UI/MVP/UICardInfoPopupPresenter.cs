using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 현재 가지고있는 모든 카드를 가져와 세팅
    /// *MVP 모델
    /// </summary>
    public class UICardInfoPopupPresenter : MonoBehaviour
    {
        [Header("[카드를 가져올 루트]")]
        public Transform CardItemRoot;

        [Header("[카드 정보 팝업에 종속된 슬롯]")]
        public UICardSlotInfoPresenter CardSlotInfoPresenter;

        [Header("[[Auto->Awake()] 카드아이템]")]
        public List<UICardItemPresenter> CardItems = new List<UICardItemPresenter>();

        [Header("[[Auto->Awake()] 카드 정보를 가지고 있는 프리젠트]")]
        public UICardPresenter Presenter;

        [Header("[Auto->Awake()] 선택된 카드 아이템")]
        //cardItem.ReturnMeEvent.AddListener()등록애 의해 호출
        public UICardItemPresenter CardItemPresenter;

        //캐쉬용
        private UICardItemPresenter cardItem;

        private void Awake()
        {
            //모든 카드 정보를 가지고 있는 모델 가져오기
            Presenter = GameObject.FindObjectOfType<UICardPresenter>();

            //모든 카드 아이템 그릇 가져오기
            for (int i = 0; i < CardItemRoot.childCount; i++) {
                cardItem = CardItemRoot.GetChild(i).GetComponent<UICardItemPresenter>();
                
                CardItems.Add(cardItem);

                //카드 선택시 이벤트발생 등록
                cardItem.SelectEvent.AddListener(ReleaseCardItemListener);

                //최초 존제할수 있는 카드의 갯수 만큼만 활성화
                if (Presenter.Model.cardSettings.Count > i) { cardItem.gameObject.SetActive(true);}
                else { cardItem.gameObject.SetActive(false); }

                //빈 카드 제외한 활성화 카드에만 카드 데이터 넣기
                if(cardItem.gameObject.activeSelf)
                {
                    //카드 데이터 각 카드마다 넣어주기
                    cardItem.CardSetting = Presenter.Model.cardSettings[i];
                    //카드 아이템 선택시 호출되게 리스너에 등록.
                    cardItem.ReturnMeEvent.AddListener(SelectedCardItem);
                }
            }
            

            Initialize();
        }

        /// <summary>
        /// 선택된 카드 아이템을 UICardInfoPopupPresenter가 가지고 있기 위해. 
        /// </summary>
        /// <param name="cardSetting"></param>
        void SelectedCardItem(UICardItemPresenter cardItemPresenter)
        {
            //선택된 카드세팅에 저장
            this.CardItemPresenter = cardItemPresenter;
            //Debug.Log(cardSetting.Name + " 클릭됨");
        }

        private void OnDestroy()
        {
            Debug.Log("삭제 됨");
            //등록된 모든 리스너 삭제
            CardItems.ForEach(value => value.ClickEvent.RemoveAllListeners());
        }

        /// <summary>
        /// 최초 활성화시 0번 카드 선택한채로 실행
        /// </summary>
        private void OnEnable()
        {
            //강제 선택 이벤트 실행 -> 즉시 실행하니 뭔가 문제로 안됨 0.1f 딜레이 시켜 실행
            Invoke("SelectCardZero", 0.1f);
        }

        //팝업뷰 최초 오픈시 강제로 0번 카드를 선택하기 위해 사용.
        void SelectCardZero() { CardItems[0].Selected();}

        private void Initialize()
        {
            //카드 정보를 업데이트
            UpdateCardItem();
        }

        /// <summary>
        /// 모든 카드 아이템 정보 업데이트
        /// </summary>
        public void UpdateCardItem()
        {
            //Debug.Log("UpdateCardItem 카드 갯수 = " + Model.cardSettings.Count);
            for (int i = 0; i < Presenter.Model.cardSettings.Count; i++)
            {
                CardItems[i].UpdateInfo(Presenter.Model.cardSettings[i]);
            }
        }

        /// <summary>
        /// 모든 카드 아이템 릴리즈 리스너
        /// </summary>
        public void ReleaseCardItemListener(string name)
        {
            for (int i = 0; i < CardItems.Count; i++)
            {
                if(CardItems[i].gameObject.activeSelf)
                {
                    //같은 이름 제외한 모든 아이템 해제
                    if (!CardItems[i].CardSetting.Name.Equals(name))
                    {
                        CardItems[i].Release();
                    }

                    //Debug.Log("카드네임 = " + CardItems[i].CardSetting.Name + "  현재이름 = " + name);
                }
            }
        }

        #region [-TestCode]
        //void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        CardItems[0].Selected();
        //    }

        //    if (Input.GetKeyDown(KeyCode.S))
        //    {
        //    }
        //}
        #endregion

    }
}
