using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

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
            string coin;
            if (value < 10) { coin = value.ToString(); }
            else { coin = string.Format("{0:0,0}", value); }

            Text.text = coin;

            //트윈 연출
            Text.transform.DOScale(1.5f, 0.1f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
    }
}
