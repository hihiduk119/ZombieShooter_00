using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 라운드 선택 뷰
    /// </summary>
    public class UIRoundSelectView : MonoBehaviour
    {
        [Header("[라운드 다운 이벤트]")]
        public UnityEvent RoundDownEvent = new UnityEvent();

        [Header("[라운드 업 이벤트]")]
        public UnityEvent RoundUpEvent = new UnityEvent();

        [Header("[라운드 강 다운 이벤트]")]
        public UnityEvent RoundStrongDownEvent = new UnityEvent();

        [Header("[라운드 강 업 이벤트]")]
        public UnityEvent RoundStrongUpEvent = new UnityEvent();

        [Header("[라운드 카운트]")]
        public Text Round;

        public void LeftClick()
        {
            RoundDownEvent.Invoke();
        }

        public void RightClick()
        {
            RoundUpEvent.Invoke();
        }

        public void DoubleLeftClick()
        {
            RoundStrongDownEvent.Invoke();
        }

        public void DoubleRightClick()
        {
            RoundStrongUpEvent.Invoke();
        }

        /// <summary>
        /// 라운드 카운트
        /// </summary>
        public void SetRound(string text)
        {
            Round.text = text;
        }
    }
}
