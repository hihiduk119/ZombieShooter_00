using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class ScreenInput : MonoBehaviour, IScreenInput
    {
        private UnityEvent pointerDown = new UnityEvent();
        public UnityEvent PointerDown => pointerDown;
        private UnityEvent pointerUp = new UnityEvent();
        public UnityEvent PointerUp => pointerUp;

        //private Touch touch;

        private Event clickEvent;

        public enum State
        {
            Down,
            Up,
        }
        //스크린 이벤트 상태
        private State state = State.Up;

        void OnGUI()
        {
            clickEvent = Event.current;

            if (clickEvent.type == EventType.MouseDown)
            {
                Debug.Log("Mouse Down.");
            }

            if (clickEvent.type == EventType.MouseUp)
            {
                Debug.Log("Mouse Up.");
            }
        }

        /*void Update()
        {
            //터치는 폰에서 사용
            //*테스트 후에 넣어야 함
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began)
                {
                    Debug.Log("[Began]");
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    Debug.Log("[Ended]");
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    Debug.Log("[Moved]");
                }

                if (touch.phase == TouchPhase.Canceled)
                {
                    Debug.Log("[Canceled]");
                }

                if (touch.phase == TouchPhase.Stationary)
                {
                    Debug.Log("[Stationary]");
                }
            }
        }*/
    }
}
