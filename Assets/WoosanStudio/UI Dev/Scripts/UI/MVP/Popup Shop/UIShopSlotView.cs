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
        public void UpdateInfo(Sprite sprite, Sprite gainValueImage,int gainValue,string description,string oldPrice,string price,bool bOldPrice,bool bAds)
        {
            Image.sprite = sprite;
            //이미지에 따라 사이즈 재정의
            float width = sprite.rect.width * 1f;
            float height = sprite.rect.height * 1f;
            Image.rectTransform.sizeDelta = new Vector2(width, height);

            GainValueImage.sprite = gainValueImage;
            GainValue.text = string.Format("{0:0,0}",gainValue);
            Description.text = description;
            OldPrice.text = oldPrice;

            Price.text = price;
            //세일가격 사용 안함
            if (!bOldPrice){
                OldPrice.transform.parent.gameObject.SetActive(false);
            }

            //광고 사용시
            //이부분 구현 해야함
            if(bAds) { }

        }
    }
}
