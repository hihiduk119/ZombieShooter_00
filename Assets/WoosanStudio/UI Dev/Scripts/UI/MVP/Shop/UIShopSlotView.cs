using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 샵 슬롯 뷰
    /// *MVP 모델
    /// </summary>
    public class UIShopSlotView : MonoBehaviour
    {
        [Header("[이미지]")]
        public Image Image;
        [Header("[획득 재화 이미지]")]
        public Image GainValueImage;
        [Header("[획득 재화]")]
        public Text GainValue;
        [Header("[설명]")]
        public Text Description;
        [Header("[할인 전 가격]")]
        public Text OldPrice;
        [Header("[가격]")]
        public Text Price;


        /// <summary>
        /// 정보 업데이트
        /// </summary>
        /// <param name="data"></param>
        public void UpdateInfo(Sprite image,Sprite gainValueImage,string gainValue,string description,string oldPrice,string price)
        {
            Image.sprite = image;
            GainValueImage.sprite = gainValueImage;
            GainValue.text = gainValue;
            Description.text = description;
            OldPrice.text = oldPrice;
            Price.text = price;
        }
    }
}
