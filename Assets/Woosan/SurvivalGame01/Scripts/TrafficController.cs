using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UnityEngine.Events;

namespace Woosan.SurvivalGame01
{
    /// <summary>
    /// 맵에서 차량을 통제하기 위한 시스템
    /// </summary>
    public class TrafficController : MonoBehaviour
    {
        static public TrafficController Instance; 
        //public UnityAction highWayAction;

        //차량운행 간격 시간
        public float waitTime = 4f;
        //고정 시간이 아닌 랜덤시간 생성
        public bool isRand = false;
        //랜던 시간 최소, 최대
        public int waitRandomTimeMin = 4;
        public int waitRandomTimeMax = 12;

        //도로 정보
        public Road road;
        //자동차 pool
        public List<Vehicle> vehiclesOnHighWay;
        //차량을 순차적으로 보내기 위해a
        int carCnt = 0;

        private void Awake()
        {
            Instance = this;

            //주행 완료 이벤트 등록
            //highWayAction += HighWayMoveEndCallback;
            //시작시 모두 비활성 화
            vehiclesOnHighWay.ForEach(value => value.gameObject.SetActive(false));
        }

        private IEnumerator Start()
        {
            WaitForSeconds wait;
            if (isRand) {//랜덤하게 차량 운행 [일반도 로]
                wait = new WaitForSeconds(Random.Range(waitRandomTimeMin, waitRandomTimeMax));
            } else {//고정 차량 운행 [고속도로]
                wait = new WaitForSeconds(waitTime);
            }


            while(true) {
                yield return wait;
                vehiclesOnHighWay[carCnt].gameObject.SetActive(true);
                vehiclesOnHighWay[carCnt].Move(road.from.position, road.to.position);
                carCnt++;
                if (carCnt >= vehiclesOnHighWay.Count) { carCnt = 0; }
            }
        }

        //이벤트 콜백 용이나 사용 안함
        //public void HighWayMoveEndCallback() 
        //{

        //}
    }
}
