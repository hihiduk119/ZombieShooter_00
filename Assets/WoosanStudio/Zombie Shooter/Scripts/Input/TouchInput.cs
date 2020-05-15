using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
namespace WoosanStudio.ZombieShooter
{
    public class TouchInput : MonoBehaviour , IInputEvents
    {
        [Header("[화면 터치 다운 발생시]")]
        public UnityEvent touchDownEvent = new UnityEvent();
        [Header("[화면 터치 업 발생시]")]
        public UnityEvent touchUpEvent = new UnityEvent();

        public UnityEvent StartEvent { get => touchDownEvent; set => touchDownEvent = value; }
        public UnityEvent EndEvent { get => touchUpEvent; set => touchUpEvent = value; }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Down");
                touchDownEvent.Invoke();
            }

            if (Input.GetMouseButtonUp(0))
            {
                //Debug.Log("Up   ");
                touchUpEvent.Invoke();
            }
        }
    }
}
