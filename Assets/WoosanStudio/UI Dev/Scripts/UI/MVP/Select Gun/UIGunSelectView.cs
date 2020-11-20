using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 건 선택
    /// *MVP패턴
    /// </summary>
    public class UIGunSelectView : MonoBehaviour
    {
        [Header("[MVP Presenter]")]
        public UIGunSelectPresenter Presenter;

        /// <summary>
        /// 왼쪽 이동 클릭
        /// </summary>
        public void LeftClick()
        {
            Presenter.Change(-1);
        }

        /// <summary>
        /// 오른쪽 이동 클릭
        /// </summary>
        public void RightClick()
        {
            Presenter.Change(1);
        }
    }
}
