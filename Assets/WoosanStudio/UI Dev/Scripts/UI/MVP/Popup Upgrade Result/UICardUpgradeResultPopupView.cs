using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드 연구 완료 팝업
    ///  *MVP패턴
    /// </summary>
    public class UICardUpgradeResultPopupView : MonoBehaviour
    {
        [Header("[카드 이미지]")]
        public Image Image;
        [Header("[카드 이름]")]
        public Text Name;
        [Header("[업그레이드 결과 내용]")]
        public Text Result;
        [Header("[레벨]")]
        public Text Level;
        

        /// <summary>
        /// 결과 정보 업데이트
        /// </summary>
        /// <param name="value"></param>
        public void UpdateResult(Sprite sprite,Color spriteColor,string name, string value, string level,Color levelColor)
        {
            Image.sprite = sprite;
            Image.color = spriteColor;
            //이미지에 따라 사이즈 재정의
            float width = Image.sprite.rect.width / 2.5f;
            float height = Image.sprite.rect.height / 2.5f;
            Image.rectTransform.sizeDelta = new Vector2(width, height);

            Name.text = name;
            Result.text = value;
            Level.text = level;
            Level.color = levelColor;
        }
    }
}
