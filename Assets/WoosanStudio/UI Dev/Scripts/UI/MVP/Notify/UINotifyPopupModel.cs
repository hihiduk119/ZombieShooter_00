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
            SlotIsMax,     //모든 연구 슬롯이 꽉차서 선택 불가.
        }

        /// <summary>
        /// 추가 시마다 인스펙터에서 추가해 줘야함.
        /// *
        /// </summary>
        [System.Serializable]
        public class Data {
            [Header("[설명 내용]")]
            public string[] Descriptions = {
                "Not enough coin."
               ,"Not enough gem."
               ,"Not enough energy."
               ,"The slot is maximum."
            };
        }

        public Data data = new Data();
    }
}
