using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter.UI
{
    /// <summary>
    /// 네임드 바 프리젠터
    /// *MVP 모델
    /// </summary>
    public class UINamedMonsterBarPresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UINamedMonsterBarView View;

        /// <summary>
        /// 활성 비활성
        /// </summary>
        public void SetActivate(bool value)
        {
            View.SetActivate(value);
        }
    }
}
