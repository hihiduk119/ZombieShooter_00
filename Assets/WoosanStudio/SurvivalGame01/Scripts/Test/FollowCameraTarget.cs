using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Woosan.SurvivalGame01
{
    /// <summary>
    /// 캠이 따라 다니는 해당 타겟을 조정하는 컨트롤러.
    /// </summary>
    public class FollowCameraTarget : MonoBehaviour
    {
        [Header("[따라다닐 타겟]")]
        public GameObject player;
        [Header("[실제 캠이 따라다닐 타겟]")]
        public GameObject followedCam;

        [Header("[따라다님 리커버 강도 [높을수록 빠르게 따라다님]]")]
        public float recoverStrong = 7.5f;
        [Header("[전방 과 캐릭터 거리 [높을수록 거리 벌림]]")]
        public float distance = 5.5f;
        [Header("[전방보며 활성화]")]
        public bool lookAhead = true;
        [Header("[전방을 내다보며 따라다닐 타겟]")]
        public GameObject lookAheadTarget;
        [Header("[캠의 방향으로 조이스틱 조정하기 위해 사용]")]
        public Transform cam;

        //캐슁을 위해 
        private Vector3 camForward;
        private float h;
        private float v;
        private Vector3 pos, desiredVelocity;
        private SkinnedMeshRenderer skinnedMeshRenderer;

        private void Awake()
        {
            //메모리 절약용 렌더 캐슁
            skinnedMeshRenderer = player.GetComponentInChildren<SkinnedMeshRenderer>();
        }

        void FixedUpdate()
        {
            //해당 오브젝트가 비활성화 상태라면 움직이지 않음 [비용절약용 코드]
            if (!skinnedMeshRenderer.isVisible) return;

            if (!lookAhead)//기본 따라다니기
            {
             //실제 러프를 거는 부분followedCam.  
                followedCam.transform.localPosition = Vector3.Lerp(followedCam.transform.localPosition, player.transform.localPosition, Time.deltaTime * recoverStrong);
            }
            else //전방을 내다 보며 따라다니기
            {
                //전방을 확인하기 위해 조이스틱 값을 가져옴
                h = UltimateJoystick.GetHorizontalAxis("Move");
                v = UltimateJoystick.GetVerticalAxis("Move");

                //카메라와 조이스틱 포지션간의 왜곡을 보정하기 위해 방향 다시 계산.
                camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
                desiredVelocity = v * camForward + h * cam.right;

                //플레이어의 좌표와 왜곡 보정계산된 방향 가속도를 현재 좌표에 적용.
                pos = player.transform.localPosition;
                pos.z += desiredVelocity.z * distance;
                pos.x += desiredVelocity.x * distance;

                //러프를 걸 타겟
                lookAheadTarget.transform.localPosition = pos;

                //실제 러프를 거는 followedCam.    
                followedCam.transform.localPosition = Vector3.Lerp(followedCam.transform.localPosition, lookAheadTarget.transform.localPosition, Time.deltaTime * recoverStrong);
                //Debug.Log("h = [" + h + "] v = [" + v + "]" + "  pos = " + pos);
                //Debug.Log("this = " + transform.localPosition + " lookAheadTarget = " + lookAheadTarget.transform.localPosition);
            }
        }
    }
}
