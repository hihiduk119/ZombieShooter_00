using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 라운드 선택 뷰
    /// </summary>
    public class UIRoundSelectView : MonoBehaviour
    {
        public UnityEvent RoundDownEvent = new UnityEvent();
        public UnityEvent RoundUpEvent = new UnityEvent();
        public UnityEvent RoundStrongDownEvent = new UnityEvent();
        public UnityEvent RoundStrongUpEvent = new UnityEvent();

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
    }
}
