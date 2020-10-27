using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 현재 가지고있는 모든 카드를 가져와 세팅
    /// *MPV 모델
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

                //존제할수 있는 카드의 갯수 만큼만 활성화
                if (Model.cardSettings.Count > i) { cardItem.gameObject.SetActive(true);  }
                else { cardItem.gameObject.SetActive(false); }
            }

            Initialize();
        }

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
            Debug.Log("UpdateCardItem 카드 갯수 = " + Model.cardSettings.Count);
            for (int i = 0; i < Model.cardSettings.Count; i++)
            {
                CardItems[i].UpdateInfo(Model.cardSettings[i]);
            }
        }
    }
}
