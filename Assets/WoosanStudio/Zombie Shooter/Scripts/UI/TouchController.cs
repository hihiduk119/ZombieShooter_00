using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class TouchController : MonoBehaviour
    {
        [Header("[왼쪽 클릭 트리거]")]
        public EventTrigger left;
        //[Header("[중앙 클릭 트리거]")]
        //public EventTrigger center;
        [Header("[오른쪽 클릭 트리거]")]
        public EventTrigger right;

        private List<EventTrigger> eventTriggers;

        public enum TouchPosition
        {
            Left,
            //Center,
            Right,
        }

        /// <summary>
        /// 시작시 자식에서 EventTrigger를 찾아서 미리 정해진 트리거에 집어넣고
        /// 이벤트 생성후 콜백 세팅
        /// </summary>
        private void Awake()
        {
            eventTriggers = new List<EventTrigger>(transform.GetComponentsInChildren<EventTrigger>());
        }

        void AddNewEventType(EventTrigger eventTrigger, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback.AddListener(action);
            eventTrigger.triggers.Add(entry);
        }

        public void AddEvent(TouchPosition touch , UnityAction<BaseEventData> action)
        {
            switch (touch)
            {
                case TouchPosition.Left:
                    AddNewEventType(left, EventTriggerType.PointerDown, action);
                    //Debug.Log("Left Touch!");
                    break;
                case TouchPosition.Right:
                    AddNewEventType(right, EventTriggerType.PointerDown, action);
                    //Debug.Log("Right Touch!");
                    break;
            }
        }

        public void OnPointerDownDelegate(PointerEventData data,GameObject downObject)
        {
            Debug.Log("OnPointerDownDelegate called. = " + downObject.name);
        }

        public void Touch(GameObject touch)
        {
            
        }
    }
}
