using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 에어 스트라이크를 연출 전체를 컨트롤 함.
    /// </summary>
    public class AirStrikeController : MonoBehaviour
    {
        [Header("[카메라]")]
        public Transform Camera;
        [Header("[그림자 타겟]")]
        public Transform Target;

        //시작 좌표
        private Vector3 StartPoint;
        //끝 좌표
        private Vector3 EndPoint;

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
            rot.y = y - 90;
            target.rotation = Quaternion.Euler(rot);

            //카메라의 포지션을 시작 포인트와 엔드포인트 초기화
            startPoint = endPoint = camera.position;

            //Distance 만큼 endPoint의 거리를 벌리기 
            switch (y)
            {
                case 90:
                    endPoint.z += distance;
                    break;
                case 180:
                    endPoint.x += distance;
                    break;
                case 270:
                    endPoint.z -= distance;
                    break;
                case 0:
                    endPoint.x -= distance;
                    break;
            }
        }

        /// <summary>
        /// 실제 연출 수행
        /// </summary>
        public void Run()
        {
            Target.gameObject.SetActive(true);
            //Target.position = StartPoint.position;
            MakeMovingCoordinate(this.Camera, 150,ref StartPoint,ref EndPoint,ref Target);

            //타겟 위치 시작 포지션으로 조정
            Target.position = StartPoint;

            //실제 트윈으로 에니메이션 실행
            Target.DOMove(EndPoint, 1.5f).SetEase(Ease.Linear).OnComplete(() => {
                //Debug.Log("OnComplete");
                ExplosionFactory.Instance.TestRun();
                Target.gameObject.SetActive(false);
            });
        }


        #region [-TestCode]
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Run();
            }
        }
        #endregion
    }
}
