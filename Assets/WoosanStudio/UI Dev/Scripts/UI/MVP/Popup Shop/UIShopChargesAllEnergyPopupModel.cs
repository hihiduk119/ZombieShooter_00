using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 에너지 샵 모델
    /// *MVP 모델
    /// </summary>
    public class UIShopChargesAllEnergyPopupModel : MonoBehaviour
    {
        /// <summary>
        /// 에너지 충전 정보
        /// </summary>
        [System.Serializable]
        public class Data
        {
            [Header("[1차 설명]")]
            public string Description_0;
            [Header("[2차 설명]")]
            public string Description_1; 
            [Header("[구매 가격]")]
            public float Price = 1.43f;
        }

        [Header("[에너지 구매 데이터]")]
        public Data data = new Data();
    }
}
