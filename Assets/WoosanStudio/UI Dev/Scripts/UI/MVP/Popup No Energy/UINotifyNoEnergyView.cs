using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 에너지 부족 뷰
    /// *MVP 모델
    /// </summary>
    public class UINotifyNoEnergyView : MonoBehaviour
    {
        [Header("[설명 2]")]
        public UnityEngine.UI.Text Description;

        [Header("[노 클릭 이벤트]")]
        public UnityEvent NoEvent = new UnityEvent();
        [Header("[예스 클릭 이벤트]")]
        public UnityEvent YesEvent = new UnityEvent();

        private void Awake()
        {
            //설명 넣기
            Description.text = "Start at < color =#ff9911ff>" + GlobalDataController.NoEnergyStartHealthPointRate.ToString() + "%</color>HP.";
        }

        /// <summary>
        /// 예스 이벤트 처리 부분
        /// </summary>
        public void ClickYes()
        {
            this.YesEvent.Invoke();
        }

        /// <summary>
        /// 노 이벤트 처리 부분
        /// </summary>
        public void ClickNo()
        {
            this.NoEvent.Invoke();
        }
    }
}
