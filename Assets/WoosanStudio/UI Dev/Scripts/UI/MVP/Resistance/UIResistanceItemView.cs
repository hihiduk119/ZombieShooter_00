using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter.UI
{
    /// <summary>
    /// 저항 아이템 뷰
    /// *MVP 모델
    /// </summary>
    public class UIResistanceItemView : MonoBehaviour
    {
        [Header("[아이템 이름]")]
        public Text Name;

        [Header("[아이템 이미지]")]
        public Image Icon;

        [Header("[아이템 설명]")]
        public Text Description;

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="icon"></param>
        public void UpdateInfo(string name,string description,Sprite icon)
        {
            Name.text = name;
            Description.text = description;
            Icon.sprite = icon;
        }
    }
}
