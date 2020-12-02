using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드연구 팝업의 컨텐츠
    /// </summary>
    public class UICardSlotInfoViewOnAllUpgradePopup : MonoBehaviour
    {
        [Header("[카드 이미지]")]
        public Image Image;

        [Header("[카드 이름]")]
        public Text Name;

        [Header("[카드 설명]")]
        public Text Description;

        [Header("[카드 레벨]")]
        public Text Level;

        [Header("[카드 연구 남은 시간]")]
        public Text RemainTime;

        [Header("[카드 연구 프로그레스]")]
        public Image Progress;

        [Header("[슬롯 상태]")]
        public UICardSlotInfoPresenterOnAllUpgradePopup.SlotState State = UICardSlotInfoPresenterOnAllUpgradePopup.SlotState.Lock;

        [Header("[슬롯 루트]")]
        public List<GameObject> Slots = new List<GameObject>();

        /// <summary>
        /// 화면 업데이트
        /// </summary>
        /// <param name="cardSetting">카드정보</param>
        /// <param name="state">카드 슬롯 상태</param>
        /// <param name="price">슬롯 가격</param>
        public void UpdateInfo(CardSetting cardSetting,UICardSlotInfoPresenterOnAllUpgradePopup.SlotState state,int price)
        {
            //슬롯 상태 저
            this.State = state;

            //카드 데이터가 존재 할때만 각종 이미지 및 설명 세팅
            if(cardSetting != null)
            {
                //모든 뷰어에 데이터 입력
                Image.sprite = cardSetting.Sprite;
                Image.color = cardSetting.IconColor;

                //이미지에 따라 사이즈 재정의
                float width = Image.sprite.rect.width / 2.5f;
                float height = Image.sprite.rect.height / 2.5f;
                Image.rectTransform.sizeDelta = new Vector2(width, height);

                Name.text = cardSetting.Name;

                Description.text = cardSetting.AllDescription();
                //1 더하는 이유는 레벨이 0부터 시작이라서
                Level.text = (cardSetting.Level + 1).ToString();
            }
            

            //일단 모든 슬롯 비활성화
            Slots.ForEach(value => value.SetActive(false));

            switch (state)
            {
                case UICardSlotInfoPresenterOnAllUpgradePopup.SlotState.Empty://비어있음.
                    Debug.Log("[0]");
                    //Empty slot 활성화
                    Slots[0].SetActive(true);
                    break;
                case UICardSlotInfoPresenterOnAllUpgradePopup.SlotState.Lock://선택됐고 업그레이드중
                    //Lock 정보 slot 활성화
                    Slots[1].SetActive(true);
                    Debug.Log("[1]");
                    
                    break;
                case UICardSlotInfoPresenterOnAllUpgradePopup.SlotState.PurchaseAble://선택만 됐음.
                    //구매 정보 slot 활성화
                    Slots[2].SetActive(true);
                    Debug.Log("[2]");
                    
                    break;
                case UICardSlotInfoPresenterOnAllUpgradePopup.SlotState.Upgrading://모두 비홯성
                    Debug.Log("[3]");
                    //카드 정보 slot 활성화
                    Slots[3].SetActive(true);
                    break;
            }
        }

        /// <summary>
        /// 남은 연구시간 시간만 업데이트
        /// </summary>
        /// <param name="upgradeRemainTime"></param>
        public void UpdateTime(string upgradeRemainTime, float upgradeRemainValue)
        {
            RemainTime.text = upgradeRemainTime;
            Progress.fillAmount = upgradeRemainValue;
        }
    }
}
