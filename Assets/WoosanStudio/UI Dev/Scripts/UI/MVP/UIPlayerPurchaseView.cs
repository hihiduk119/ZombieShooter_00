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

        [Header("[MVP Presenter]")]
        public UIPlayerPurchasePresenter Presenter;

        /// <summary>
        /// 뷰 활성
        /// </summary>
        public void Active()
        {
            View.SetActive(true);
            Gem.text = Presenter.Model.RequireGem.ToString();
            Level.text = Presenter.Model.RequireLevel.ToString();
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
