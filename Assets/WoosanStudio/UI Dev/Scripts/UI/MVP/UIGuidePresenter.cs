using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter.UI
{
    /// <summary>
    /// 가이드 프리젠터
    /// *MVP 모델
    /// </summary>
    public class UIGuidePresenter : MonoBehaviour
    {
        [Header("[복제하려는 아이템]")]
        public GameObject[] PrefabItems;

        //[Header("[사용 하는 아이템]")]
        //public GameObject[] PrefabItems;

        [Header("[MVP View]")]
        public UIGuideView View;


        /// <summary>
        /// 활성 비활성
        /// </summary>
        public void SetActivate(bool value)
        {
            View.SetActivate(value);
        }
    }
}
