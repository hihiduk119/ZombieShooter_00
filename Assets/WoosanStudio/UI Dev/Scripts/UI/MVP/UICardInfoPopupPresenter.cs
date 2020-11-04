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

        [Header("[[Auto->Awake()] 카드아이템]")]
        public List<UICardItemView> CardItems = new List<UICardItemView>();

        [Header("[[Auto->Awake()] 카드 정보를 가지고 있는 모델]")]
        public UICardModel Model;


        //캐쉬용
        private UICardItemView cardItem;

        private void Awake()
        {
            //모든 카드 정보를 가지고 있는 모델 가져오기
            Model = GameObject.FindObjectOfType<UICardModel>();

            //모든 카드 아이템 그릇 가져오기
            for (int i = 0; i < CardItemRoot.childCount; i++) {
                cardItem = CardItemRoot.GetChild(i).GetComponent<UICardItemView>();
                CardItems.Add(cardItem);

                //카드 선택시 이벤트발생 등록
                cardItem.SelectEvent.AddListener(ReleaseCardItemListener);

                //존제할수 있는 카드의 갯수 만큼만 활성화
                if (Model.cardSettings.Count > i) { cardItem.gameObject.SetActive(true);}
                else { cardItem.gameObject.SetActive(false); }
            }

            Initialize();
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
            for (int i = 0; i < Model.cardSettings.Count; i++)
            {
                CardItems[i].UpdateInfo(Model.cardSettings[i]);
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
