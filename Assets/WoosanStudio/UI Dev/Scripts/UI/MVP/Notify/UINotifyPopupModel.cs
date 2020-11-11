using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 알림 통지
    /// *MVP 모델
    /// </summary>
    public class UINotifyPopupModel : MonoBehaviour
    {
        [System.Serializable]
        public enum Type {
            NotEnoughCoin = 0,  //코인 부족
            NotEnoughGem,       //보석 부족
            NotEnoughEnergy,    //에너지 부족

            //중복 카드 존제
            SlotIsFull,     //모든 연구 슬롯이 꽉차서 선택 불가.
        }

        [System.Serializable]
        public class Data {
            [Header("[설명 내용]")]
            public string[] Descriptions = {
                "Not enough coin."
               ,"Not enough gem."
               ,"Not enough energy."
               ,"Reasearch slot is full."
            };
        }

        public Data data = new Data();
    }
}
