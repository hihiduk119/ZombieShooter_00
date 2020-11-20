using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 플레이어의 데이터
    /// *MVP패턴
    /// </summary>
    public class UIPlayerSelectView : MonoBehaviour
    {
        [Header("[MVP Presenter]")]
        public UIPlayerSelectPresenter Presenter;

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
