using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 모든 연구 슬롯
    /// MVP 모델
    /// </summary>
    public class UICardUpgradeSlotPopupModel : MonoBehaviour
    {
        [System.Serializable]
        public class Data
        {
            //연구 슬롯 언락시 구매 가격 리스트
            public List<int> Prices = new List<int>();
        }

        [Header("[업그레이드 슬롯 업글 가격]")]
        public Data data = new Data();
    }
}
