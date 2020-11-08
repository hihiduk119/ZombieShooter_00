using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 구매 최종 통지
    /// *MVP 모델
    /// </summary>
    public class UINotifyPurchasePopupView : MonoBehaviour
    {
        [Header("[대표 이미지]")]
        public Image MainImage;
        [Header("[획득 이미지]")]
        public Image GainImage;
        [Header("[획득 값]")]
        public Text GainValue;
        [Header("[가격]")]
        public Text Price;


        /// <summary>
        /// 화면 업데이트
        /// </summary>
        /// <param name="mainImage"></param>
        /// <param name="gainImage"></param>
        /// <param name="gainValue"></param>
        /// <param name="price"></param>
        public void UpdateView(Sprite mainImage,Sprite gainImage,string gainValue,string price )
        {
            MainImage.sprite = mainImage;

            //이미지에 따라 사이즈 재정의
            float width = mainImage.rect.width * 2f;
            float height = mainImage.rect.height * 2f;
            MainImage.rectTransform.sizeDelta = new Vector2(width, height);

            GainImage.sprite = gainImage;
            GainValue.text = gainValue;
            Price.text = price;
        }
    }
}
