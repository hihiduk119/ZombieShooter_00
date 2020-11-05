using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 샵 슬롯 모델
    /// 구매 정보를 가지고 있음
    /// *MVP 모델
    /// </summary>
    public class UIShopSlotModel : MonoBehaviour
    {

        /// <summary>
        /// 획득 재화 타입
        /// </summary>
        [System.Serializable]
        public enum Type
        {
            Goin,   //코인
            Gem,    //젬
        }

        [System.Serializable]
        public class Data
        {
            [Header("[인덱스]")]
            public int Index;
            [Header("[획득 타입]")]
            public Type type;
            [Header("[아이콘 이미지]]")]
            public Sprite Image;
            [Header("[설명]]")]
            public string Description;
            [Header("[얻는 재화]")]
            public int GainValue;
            [Header("[얻는 재화 이미지]")]
            public Sprite GainValueImage;
            [Header("[할인 전 가격]")]
            public float OldPrice;
            [Header("[가격]")]
            public float Price;   
        }

        [Header("[샵 구매 데이터]")]
        public Data data = new Data();
    }
}
