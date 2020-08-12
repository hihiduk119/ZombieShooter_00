using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// UI 타겟으로 월드 오브젝트를 이동시켜줌
    /// </summary>
    public class MoveToUITarget : MonoBehaviour
    {
        [Header("[기준 카메라 (Auto->Awake())]")]
        public UnityEngine.Camera camera;

        [Header("[이동 시키려는 UI 타겟]")]
        public RectTransform TargetUI;

        [Header("[Ray에 맞은 포지션 타겟 -> [눈으로 보기위한 테스트용]")]
        public GameObject RayHitTarget;

        [Header("[UI 타겟으로 이동 완료 이벤트]")]
        public UnityEvent MoveCompleteEvent = new UnityEvent();

        private RaycastHit hit;
        private Ray ray;
        private Coroutine moveCoroutine;

        private void Awake()
        {
            //메인 카메라를 잡음
            camera = UnityEngine.Camera.main;
        }

        /// <summary>
        /// UI 화면좌표계를 ScreenPoint 좌표로 바꾸기위한 코드 1
        /// </summary>
        /// <param name="uiElement"></param>
        /// <returns></returns>
        public Rect GetScreenCoordinatesOfCorners(RectTransform uiElement)
        {
            var worldCorners = new Vector3[4];
            uiElement.GetWorldCorners(worldCorners);
            var result = new Rect(
                          worldCorners[0].x,
                          worldCorners[0].y,
                          worldCorners[2].x - worldCorners[0].x,
                          worldCorners[2].y - worldCorners[0].y);
            return result;
        }

        /// <summary>
        /// UI 화면좌표계를 ScreenPoint 좌표로 바꾸기위한 코드 2
        /// </summary>
        /// <param name="uiElement"></param>
        /// <returns></returns>
        public Vector2 GetPixelPositionOfRect(RectTransform uiElement)
        {
            Rect screenRect = GetScreenCoordinatesOfCorners(uiElement);

            return new Vector2(screenRect.center.x, screenRect.center.y);
        }

        /// <summary>
        /// 타겟으로 이동
        /// </summary>
        public void Move()
        {
            if (moveCoroutine != null) StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(MoveCoroutine());
        }

        /// <summary>
        /// 실제 타겟으로 이동 코루틴
        /// </summary>
        /// <returns></returns>
        IEnumerator MoveCoroutine()
        {
            while(true)
            {
                yield return new WaitForEndOfFrame();

                //UI 좌표계를 Screen 좌표계로 변환
                Vector2 pos = GetPixelPositionOfRect(TargetUI);

                //Debug.Log(pos.ToString());

                //화면의 고정 포인트 지점으로 레이 쏨
                ray = camera.ScreenPointToRay(new Vector3(pos.x, pos.y,0 ));

                if (Physics.Raycast(ray, out hit))
                {
                    //Ray Hit 포인트로 타겟 이동
                    RayHitTarget.transform.position = hit.point;

                    //2f 이내로 접근시 이동 완료 이벤트 호출
                    if(Vector3.Distance(RayHitTarget.transform.position, transform.position) <= 2f)
                    {
                        MoveCompleteEvent?.Invoke();
                    }

                    //러프 이동
                    transform.position = Vector3.Lerp(transform.position, hit.point, 0.1f);
                }

                //마우스 따라 다니는 테스트 코드
                //ray = camera.ScreenPointToRay(Input.mousePosition);
                //Debug.Log("마우스 포지션 =" + Input.mousePosition.ToString());
                //if (Physics.Raycast(ray, out hit))
                //{
                //    transform.position = hit.point;
                //    Debug.Log(hit.point.ToString());
                //}
            }
        }
    }
}
