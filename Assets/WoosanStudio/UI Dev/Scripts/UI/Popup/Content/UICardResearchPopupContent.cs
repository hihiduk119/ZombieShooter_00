using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드연구 팝업의 컨텐츠
    /// </summary>
    public class UICardResearchPopupContent : MonoBehaviour
    {
        [Header("[카드 이미지]")]
        public Image Image;
        [Header("[카드 이름]")]
        public Text Name;
        [Header("[카드 레벨]")]
        public Text Level;
        [Header("[카드 설명]")]
        public Text Description;

        /// <summary>
        /// 정보를 업데이트 한다.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="name"></param>
        /// <param name="level"></param>
        /// <param name="description"></param>
        public void UpdateInfo(Sprite sprite, string name, int level, string description)
        {
            Image.sprite = sprite;
            Name.text = name;
            Level.text = level.ToString();
            Description.text = description;
        }
    }
}
