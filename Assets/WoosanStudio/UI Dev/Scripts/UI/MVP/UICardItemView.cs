using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드 아이템 뷰
    /// *MPV 모델
    /// </summary>
    public class UICardItemView : MonoBehaviour
    {
        [Header("[카드 이미지]")]
        public Image Image;

        [Header("[카드 이름]")]
        public Text Name;

        [Header("[카드 레벨]")]
        public Text Level;

        [Header("[카드 레벨 락]")]
        public Transform Lock;

        [Header("[카드 언락 레벨]")]
        public Text UnlockLevel;

        /// <summary>
        /// 카드 뷰 업데이트
        /// </summary>
        /// <param name="cardSetting"></param>
        public void UpdateInfo(CardSetting cardSetting )
        {
            Image.sprite = cardSetting.Sprite;
            Image.color = cardSetting.IconColor;

            Name.text = cardSetting.Name;
            //cardSetting.UnlockLevel
        }
    }
}
