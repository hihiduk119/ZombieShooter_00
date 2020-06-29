using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    ///쉐도우 프로젝터 연출을 함
    /// </summary>
    public class AirStrikeShadowController : MonoBehaviour ,IStart, IEnd 
    {
        [Header("[이동 간격 거리]")]
        public float Distance = 75f;
        [Header("[카메라]")]
        public Transform Camera;
        [Header("[그림자 타겟]")]
        public Transform Target;
        [Header("[더미용 시작 포지션 [위치 확인용. 없어도 그만]]")]
        public Transform StartDummy;
        [Header("[더미용 끝 포지션 [위치 확인용. 없어도 그만]]")]
        public Transform EndDummy;

        //시작 좌표
        private Vector3 StartPoint;
        //끝 좌표
        private Vector3 EndPoint;

        #region [IStart,IEnd Implement]
        [Header("[쉐도우 프로젝터 시작 이벤트]")]
        [SerializeField]
        private UnityEvent mStartEvent = new UnityEvent();
        public UnityEvent StartEvent => mStartEvent;

        [Header("[쉐도우 프로젝터 끝남 이벤트]")]
        [SerializeField]
        private UnityEvent mEndEvent = new UnityEvent();
        public UnityEvent EndEvent => mEndEvent;
        #endregion

        /// <summary>
        /// 카메라가 바로보는 방향으로 공습 이동 좌표를 만들기
        /// </summary>
        /// <param name="camera">해당 카메라</param>
        /// /// <param name="distance">시작과 끝 간의 이동거리</param>
        /// <param name="startPoint">반환되는 시작 포인트</param>
        /// <param name="endPoint">반환되는 끝 포인트</param>
        void MakeMovingCoordinate(Transform camera, float distance, ref Vector3 startPoint, ref Vector3 endPoint , ref Transform target)
        {
            //일부러 int 로 받음. 소수 허용 안하게 하기 위해.
            int y  = (int)camera.localRotation.eulerAngles.y;

            //90으로 나눠서 나머지 없는지 확인. 0,90,180,270 만 형용하기 위한 검증루틴
            int remainder = y % 90;
            if(remainder > 0) { Debug.Log("나머지 발생 카메라 회전 각도 이상"); }

            //임시로 받을 데이터공간
            Vector3 rot = target.rotation.eulerAngles;

            //타겟 회전을 위해
            rot.y = y - 180;
            target.rotation = Quaternion.Euler(rot);

            //카메라의 포지션을 시작 포인트와 엔드포인트 초기화
            startPoint = endPoint = camera.position;

            //Distance 만큼 endPoint의 거리를 벌리기
            //endPoint는 시작점
            //startPoint는 끝점임
            switch (y)
            {
                case 0:
                    endPoint.z += distance;
                    startPoint.z -= distance / 2;
                    break;
                case 90:
                    endPoint.x += distance;
                    startPoint.x -= distance / 2;
                    break;
                case 180:
                    endPoint.z -= distance;
                    startPoint.z += distance / 2;
                    break;
                case 270:
                    endPoint.x -= distance;
                    startPoint.x += distance / 2;
                    break;   
            }


            //눈으로 확인하기 위해 더미위치 조정
            if(StartDummy != null )
            {
                StartDummy.position = startPoint;
            }
            //눈으로 확인하기 위해 더미위치 조정
            if (EndDummy != null)
            {
                EndDummy.position = endPoint;
            }
        }

        /// <summary>
        /// 폭격 연출 수행
        /// </summary>
        public void Run()
        {
            //에나메이션 시작이벤트 발생
            mStartEvent.Invoke();
            //쉐도우 프로젝트 활성화
            Target.gameObject.SetActive(true);

            //움직일 거리 재계산 -> 스테이지 이동시 마다 매번 바뀌기 때문에.
            MakeMovingCoordinate(this.Camera, Distance, ref EndPoint, ref  StartPoint, ref Target);

            //타겟 위치 시작 포지션으로 조정
            Target.position = StartPoint;

            //실제 트윈으로 에니메이션 실행
            Target.DOMove(EndPoint, 1.2f).SetEase(Ease.Linear).OnComplete(() => {
                //에니메이션 끝남 이벤트 발생
                mEndEvent.Invoke();
                //ExplosionFactory.Instance.TestRun();
                Target.gameObject.SetActive(false);
            });
        }


        #region [-TestCode]
        private void Update()
        {
            //폭격 연출 호
            if (Input.GetKeyDown(KeyCode.M))
            {
                Run();
            }
        }
        #endregion
    }
}
