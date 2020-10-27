using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드 정보 뷰
    /// *MPV 모델
    /// </summary>
    public class UICardInfoView : MonoBehaviour
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

        [Header("[카드 활성 가능 버튼들]")]
        public List<GameObject> Buttons = new List<GameObject>();

        [System.Serializable]
        public enum CardInfoButtonState
        {
            Upgrade = 0,
            Cancel,
            None,
        }

        [System.Serializable]
        public class InfoData
        {
            public Sprite Sprite;

            public string Name;

            public string Description;

            public int Level;

            public string RemainTime;

            public float progressValue;

            public CardInfoButtonState buttonState = CardInfoButtonState.None;
        }

        /// <summary>
        /// InfoData는 나중에 변경 될수 있기에 일단 대신 InfoData
        /// </summary>
        public void UpdateInfo(InfoData infoData)
        {
            //모든 뷰어에 데이터 입력
            Image.sprite = infoData.Sprite;
            Name.text = infoData.Name;
            Description.text = infoData.Description;
            Level.text = infoData.Level.ToString();
            RemainTime.text = infoData.RemainTime;
            Progress.fillAmount = infoData.progressValue;

            //일단 모든 버튼 비활성화
            Buttons.ForEach(value => value.SetActive(false));
            switch (infoData.buttonState)
            {
                case CardInfoButtonState.Upgrade:
                    //0번 업글레이드 버튼 활성화
                    Buttons[0].SetActive(true);
                    break;
                case CardInfoButtonState.Cancel:
                    //1번 업그레이드 취소 버튼 활성화
                    Buttons[1].SetActive(true);
                    break;
                case CardInfoButtonState.None:
                    break;
            }
        }

        /// <summary>
        /// InfoData는 나중에 변경 될수 있기에 일단 대신 InfoData
        /// </summary>
        public void UpdateInfoListener(CardSetting cardSetting)
        {
            //모든 뷰어에 데이터 입력
            Image.sprite = cardSetting.Sprite;
            Image.color = cardSetting.IconColor;
            //이미지에 따라 사이즈 재정의
            float width = Image.sprite.rect.width/2.5f;
            float height = Image.sprite.rect.height/2.5f;
            Image.rectTransform.sizeDelta = new Vector2(width, height);

            Name.text = cardSetting.Name;

            Description.text = cardSetting.AllDescription();
            //1 더하는 이유는 레벨이 0부터 시작이라서
            Level.text = (cardSetting.Level + 1).ToString();

            RemainTime.text = cardSetting.GetRemainUpgradeTimeByString();
            //*일단 대기-> 타임 셋을 제대로 정리하고 가야 함.안그러면 문제 생김
            //Progress.fillAmount = infoData.progressValue;

            //버튼 상태 *여기도 수정 해야함
            CardInfoButtonState buttonState = CardInfoButtonState.Cancel;

            //일단 모든 버튼 비활성화
            Buttons.ForEach(value => value.SetActive(false));
            switch (buttonState)
            {
                case CardInfoButtonState.Upgrade:
                    //0번 업글레이드 버튼 활성화
                    Buttons[0].SetActive(true);
                    break;
                case CardInfoButtonState.Cancel:
                    //1번 업그레이드 취소 버튼 활성화
                    Buttons[1].SetActive(true);
                    break;
                case CardInfoButtonState.None:
                    break;
            }
        }
    }
}
