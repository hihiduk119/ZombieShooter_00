using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 플레이어의 데이터
    /// *MVC패턴
    /// </summary>
    public class UIPlayerSelectView : MonoBehaviour
    {
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
