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
            Level.text = value.ToString();
        }

        /// <summary>
        /// UIPlayerPresenter 에서 UpdatePurchaseView 받음 
        /// </summary>
        /// <param name="data"></param>
        public void UpdateViewReceiver(UIPlayerPresenter.PurchaseViewData data)
        {
            //data.Print();

            //이미 구매했다면
            if (data.UseAble) {
                View.SetActive(false);
                return;
            }

            //구매 안했다면
            View.SetActive(true);
            UpdateGem(data.RequireGem);
            UpdateLevel(data.RequireLevel);
        }
    }
}
