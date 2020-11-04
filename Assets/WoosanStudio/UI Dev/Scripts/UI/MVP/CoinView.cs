using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 
    /// *MVP 모델
    /// </summary>
    public class CoinView : MonoBehaviour
    {
        [Header("[코인 UI]")]
        public UnityEngine.UI.Text Text;

        /// <summary>
        /// 코인 화면 정보 업데이트
        /// </summary>
        /// <param name="value"></param>
        public void UpdateCoin(int value)
        {
            Text.text = string.Format("{0:0,0}", value);
        }
    }
}
