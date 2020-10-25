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
    }
}
