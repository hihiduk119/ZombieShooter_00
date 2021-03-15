using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter.UI
{
    /// <summary>
    /// 백 뷰
    /// *MVP 모델
    /// </summary>
    public class UIBagView : MonoBehaviour
    {
        [Header("[획득 경험치]")]
        public Text Exp;

        [Header("[획득 코인]")]
        public Text Coin;

        [Header("[획득 젬]")]
        public Text Gem;

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="coin"></param>
        /// <param name="gem"></param>
        public void UpdateInfo(string exp, string coin, string gem)
        {
            Exp.text = "+" + exp;
            Coin.text = "+" + coin;
            Gem.text = "+" + gem;
        }
    }
}
