using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
using System;

namespace WoosanStudio.ZombieShooter
{
    [DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
    [SaveDuringPlay]
    public class PositionController : MonoBehaviour
    {
        //시작하면 바로 메인카메라 가져옴
        [Header("[메인 카메라]")]//
        public UnityEngine.Camera mainCamera;

        [Header("[바이케이트 루트]")]//내 자신
        public Transform Target;

        [Header("[카메라간 거리]")]
        public float Distance;

        private void Awake()
        {
            //mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UnityEngine.Camera>();
            #region [-TestCode]
            MakeMovingCoordinate(mainCamera.transform, this.Distance, ref Target);
            #endregion
        }

        /// <summary>
        /// 카메라가 바로보는 방향으로 베리어 위치 이동
        /// </summary>
        /// <param name="camera">해당 카메라</param>
        /// /// <param name="distance">시작과 끝 간의 이동거리</param>
        /// <param name="coordination">반환되는 끝 포인트</param>
        void MakeMovingCoordinate(Transform camera, float distance, ref Transform target)
        {
            //일부러 int 로 받음. 소수 허용 안하게 하기 위해.
            int y = (int)camera.localRotation.eulerAngles.y;
            Vector3 coordination;

            //90으로 나눠서 나머지 없는지 확인. 0,90,180,270 만 형용하기 위한 검증루틴
            int remainder = y % 90;
            if (remainder > 0) { Debug.Log("나머지 발생 카메라 회전 각도 이상"); }

            //임시로 받을 데이터공간
            Vector3 rot = target.rotation.eulerAngles;

            //타겟 회전을 위해
            rot.y = y ;
            target.rotation = Quaternion.Euler(rot);

            //카메라의 포지션을 시작 포인트와 엔드포인트 초기화
            coordination = camera.position;

            //0으로 초기
            coordination.y = 0;

            //Distance 만큼 endPoint의 거리를 벌리기 
            switch (y)
            {
                case 90:
                    coordination.z += distance;
                    break;
                case 180:
                    coordination.x += distance;
                    break;
                case 270:
                    coordination.z -= distance;
                    break;
                case 0:
                    coordination.x -= distance;
                    break;
            }

            //실제 타겟에 적용.
            target.position = coordination;
        }

        #region [-TestCode]
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                MakeMovingCoordinate(mainCamera.transform, this.Distance,ref Target);
            }
        }
        #endregion
    }
}
