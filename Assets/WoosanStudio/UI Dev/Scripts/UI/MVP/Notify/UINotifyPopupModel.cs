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
            NotEnoughEnergy,    //*에너지 부족 -> 시스템 변경으로 사용 안함
            SlotIsMax,          //슬롯 추가 불가.슬롯이 최대 상태임.
            SlotIsFull,         //모든 연구 슬롯이 꽉차서 연구 불가
        }

        /// <summary>
        /// 추가 시마다 인스펙터에서 추가해 줘야함.
        /// *
        /// </summary>
        [System.Serializable]
        public class Data {
            [Header("[설명 내용]")]
            [SerializeField][TextArea(3, 8)]
            public string[] Descriptions = {
                "Not enough coin."
               ,"Not enough gem."
               ,"Not enough energy."
               ,"The research slot is maximum."
               ,"The research slot is full.\nAdd slots or wait for the research to finish."
            };
        }

        public Data data = new Data();
    }
}
