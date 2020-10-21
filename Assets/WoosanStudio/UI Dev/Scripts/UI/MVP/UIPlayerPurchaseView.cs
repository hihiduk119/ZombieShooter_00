using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 플레이어 구매 뷰
    /// *MPV 모델
    /// </summary>
    public class UIPlayerPurchaseView : MonoBehaviour
    {
        [Header("[활성 뷰]")]
        public GameObject View;

        [Header("[필요 젬 수량]")]
        public Text Gem;

        [Header("[필요 레벨]")]
        public Text Level;

        /// <summary>
        /// 뷰 활성
        /// </summary>
        public void Active()
        {
            View.SetActive(true);
            
            
        }

        /// <summary>
        /// 보석 업데이트
        /// </summary>
        /// <param name="value"></param>
        public void UpdateGem(int value)
        {
            Gem.text = string.Format("{0:0,0}", value);
        }

        /// <summary>
        /// 레벨 업데이트
        /// </summary>
        /// <param name="value"></param>
        public void UpdateLevel(int value)
        {
            Level.text = string.Format("{0:0,0}", value);
        }

        /// <summary>
        /// 뷰 비활성
        /// </summary>
        public void Deactive()
        {
            View.SetActive(false);
        }
    }
}
