using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 998.OnlyProjetile 씬에 사격 컨트롤을 위해 테스트용 컨트롤러
    /// </summary>
    public class FireController : MonoBehaviour, IStart, IEnd
    {
        UnityEvent startEvent = new UnityEvent();
        UnityEvent endEvent = new UnityEvent();

        public UnityEvent StartEvent => startEvent;
        public UnityEvent EndEvent => endEvent;

        #region [-TestCode]
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                startEvent.Invoke();
                //Debug.Log("S Invoke");
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                endEvent.Invoke();
                //Debug.Log("E Invoke");
            }
        }
        #endregion

    }
}
