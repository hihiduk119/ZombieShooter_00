using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    //[System.Serializable]
    //public enum Direction : int
    //{
    //    Left = 0,
    //    Right,
    //}

    [System.Serializable]
    public class SwipeEvent : UnityEvent<Direction> { }

    public class SwipeController : MonoBehaviour, IDragHandler, IEndDragHandler , IBeginDragHandler
    {
        public Transform testTarget;

        public SwipeEvent swipeEvent = new SwipeEvent();

        //cashe
        private Vector3 _begin;
        private Vector3 _end;
        private Direction _direction = Direction.Left;

        //스와이프인지 단순 드래그 인지 확인용.
        private bool isSwipe = false;
        private Coroutine dragTimer;
        private float dragMaxTime = 0.33f;
        private WaitForEndOfFrame WFE = new WaitForEndOfFrame();
        private float deltaTime = 0;

        /// <summary>
        /// 마우스 터치 시작
        /// </summary>
        /// <param name="eventData"></param>
        public void OnBeginDrag(PointerEventData eventData)
        {
            _begin = testTarget.position = eventData.position;

            if (dragTimer != null) StopCoroutine(dragTimer);
            dragTimer = StartCoroutine(DragTimer(dragMaxTime));
        }

        /// <summary>
        /// 마우스 드래그 상태
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrag(PointerEventData eventData)
        {
            testTarget.position = eventData.position;
        }

        /// <summary>
        /// 마우스 터치 끝
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
            _end = testTarget.position = eventData.position;

            //방향 계산
            _direction = FindDirection(_begin, _end);

            //스와이프 방향 이벤트 발생
            if (isSwipe)
            {
                swipeEvent.Invoke(_direction);
                Debug.Log("스와이프");
            } else
            {
                Debug.Log("일반 드래그");
            }
            
        }

        /// <summary>
        /// swipe인지 아닌지 체크
        /// 
        /// </summary>
        /// <param name="maxTime"></param>
        /// <returns></returns>
        IEnumerator DragTimer(float maxTime)
        {
            WFE = new WaitForEndOfFrame();
            deltaTime = 0;
            isSwipe = true;

            while (maxTime > deltaTime)
            {
                deltaTime += Time.deltaTime;
                yield return WFE;
            }

            isSwipe = false;
        }


        //마우스 시작과 끝의 x 좌표를 비교하여 Left 와 Right두개로 분류
        private Direction FindDirection(Vector3 begin,Vector3 end)
        {
            Direction direction = Direction.Left;
            float difference = end.x - begin.x;

            if (difference > 0)
                direction = Direction.Right;
            else
                direction = Direction.Left;

            Debug.Log("방향 = " + direction.ToString());

            return direction;
        }
    }
}
