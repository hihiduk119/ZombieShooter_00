using UnityEngine;

using DG.Tweening;
//using UnityEngine.Events;

namespace Woosan.SurvivalGame01
{
    /// <summary>
    /// 도로 위의 차량
    /// </summary>
    public class Vehicle : MonoBehaviour
    {
        //UnityEvent moveEnd;

        public float duration = 5f;

        private void Start()
        {
            //moveEnd = new UnityEvent();
            //교통 컨트롤러에 이동 완료 이벤트 넣 음
            //moveEnd.AddListener(TrafficController.Instance.highWayAction);
        }

        /// <summary>
        /// from 에서 to까지 이동.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public void Move( Vector3 from,Vector3 to )
        {
            //시작 위치로 이동
            transform.position = from;
            //트윈 시작
            transform.DOMove(to, duration).SetEase(Ease.Linear).OnComplete(() => {
                //moveEnd.Invoke();
                this.gameObject.SetActive(false);
            });
        }
    }
}
