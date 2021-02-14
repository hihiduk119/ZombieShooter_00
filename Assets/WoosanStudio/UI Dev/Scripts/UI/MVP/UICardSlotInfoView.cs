using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드 슬롯 정보 뷰
    /// *MVP 모델
    /// </summary>
    public class UICardSlotInfoView : MonoBehaviour
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
        public List<GameObject> Slots = new List<GameObject>();

        [Header("[카드 활성 가능 버튼들]")]
        public List<GameObject> Buttons = new List<GameObject>();

        /// <summary>
        /// InfoData는 나중에 변경 될수 있기에 일단 대신 InfoData
        /// </summary>
        public void UpdateInfo(CardSetting cardSetting , UICardSlotInfoPresenter.State state)
        {
            //모든 뷰어에 데이터 입력
            Image.sprite = cardSetting.Sprite;
            Image.color = cardSetting.IconColor;
            //이미지에 따라 사이즈 재정의
            float width = Image.sprite.rect.width * 1.75f;
            float height = Image.sprite.rect.height * 1.75f;
            Image.rectTransform.sizeDelta = new Vector2(width, height);

            Name.text = cardSetting.Name;

            Description.text = cardSetting.AllDescription();
            //1 더하는 이유는 레벨이 0부터 시작이라서
            Level.text = (cardSetting.Level + 1).ToString();

            //RemainTime.text = cardSetting.GetRemainUpgradeTimeByString();
            //*일단 대기-> 타임 셋을 제대로 정리하고 가야 함.안그러면 문제 생김
            //Progress.fillAmount = infoData.progressValue;

            //일단 모든 슬롯 비활성화
            Slots.ForEach(value => value.SetActive(false));
            //일단 모든 버튼 비활성화
            Buttons.ForEach(value => value.SetActive(false));

            //Debug.Log("state = " + state.ToString());

            switch (state)
            {
                case UICardSlotInfoPresenter.State.Empty://비어있음.
                    //Debug.Log("[0]");
                    //Empty slot 활성화
                    Slots[0].SetActive(true);
                    break;
                case UICardSlotInfoPresenter.State.Select://선택됐고 업그레이드중
                    //카드 정보 slot 활성화
                    Slots[1].SetActive(true);
                    //Debug.Log("[1]");
                    //0번 업글레이드 버튼 활성화
                    Buttons[0].SetActive(true);
                    break;
                case UICardSlotInfoPresenter.State.SelectAndUpgrading://선택만 됐음.
                    //카드 정보 slot 활성화
                    Slots[1].SetActive(true);
                    //Debug.Log("[2]");
                    //1번 업그레이드 취소 버튼 활성화
                    Buttons[1].SetActive(true);
                    break;
                case UICardSlotInfoPresenter.State.Lock://모두 비홯성
                    //Debug.Log("[3]");
                    //카드 정보 slot 활성화
                    Slots[1].SetActive(true);
                    break;
            }
        }

        /// <summary>
        /// 남은 연구시간 시간만 업데이트
        /// </summary>
        /// <param name="upgradeRemainTime"></param>
        public void UpdateTime(string upgradeRemainTime,float upgradeRemainValue)
        {
            RemainTime.text = upgradeRemainTime;
            Progress.fillAmount = upgradeRemainValue;
        }
    }
}
