using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.Camera
{
    /// <summary>
    /// 캠이 따라 다니는 해당 타겟을 조정하는 컨트롤러.
    /// </summary>
    public class FollowCameraTarget : MonoBehaviour
    {
        [Header("[따라다닐 타겟]")]
        public GameObject player;
        [Header("[조이스틱 타겟]")]
        public GameObject joystickTarget;
        [Header("[조이스틱 타겟을 따라다나는 타겟]")]
        public GameObject aheadTarget;
        [Header("[캐릭터를 따라다나는 타겟]")]
        public GameObject followTarget;
        //[Header("[FollowTarget 거리제한용 기준 타겟]")]
        //public GameObject distanceLockCenterTarget;
        //[Header("[FollowTarget 거리제한용 더미 타겟]")]
        //public GameObject distanceLockDummyTarget;

        [Header("[따라다님 리커버 강도 [높을수록 빠르게 따라다님]]")]
        public float recoverStrong = 7.5f;
        [Header("[전방 과 캐릭터 거리 [높을수록 거리 벌림]]")]
        public float distance = 5.5f;

        [Header("[캠의 방향으로 조이스틱 조정하기 위해 사용]")]
        public Transform cam;

        //캐슁을 위해 
        private Vector3 camForward;
        private float h;
        private float v;
        private Vector3 pos, desiredVelocity;
        private SkinnedMeshRenderer skinnedMeshRenderer;
        //더미 좌표에 플레이어 좌료를 집어 넣기용. 범위를 벗어났을때 한번씩만 가능a
        private bool setAbleDummy = true;


        private void Awake()
        {
            //메모리 절약용 렌더 캐슁
            skinnedMeshRenderer = player.GetComponentInChildren<SkinnedMeshRenderer>();
        }

        void FixedUpdate()
        {
            if(skinnedMeshRenderer != null)
            {
                //해당 오브젝트가 비활성화 상태라면 움직이지 않음 [비용절약용 코드]
                if (!skinnedMeshRenderer.isVisible) return;
            }

            //전방을 확인하기 위해 조이스틱 값을 가져옴
            h = UltimateJoystick.GetHorizontalAxis("LookAhead");
            v = UltimateJoystick.GetVerticalAxis("LookAhead");

            //Debug.Log("h = " + h + "  | v = " + v);

            //카메라와 조이스틱 포지션간의 왜곡을 보정하기 위해 방향 다시 계산.
            camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            desiredVelocity = v * camForward + h * cam.right;

            //플레이어의 좌표와 왜곡 보정계산된 방향 가속도를 현재 좌표에 적용.
            pos = player.transform.position;
            pos.z += desiredVelocity.z * distance;
            pos.x += desiredVelocity.x * distance;

            //러프를 걸 타겟
            joystickTarget.transform.position = pos;

            //실제 러프를 거는 followedCam.    
            aheadTarget.transform.position = Vector3.Lerp(aheadTarget.transform.position, joystickTarget.transform.position, Time.deltaTime * recoverStrong);

            //distanceLockCenterTarget 과 distanceLockDummyTarget 부분은 추후에 수정해야 할듯.
            //
            //Debug.Log(Mathf.Abs(distanceLockCenterTarget.transform.position.z - player.transform.position.z));
            //플레이어가 기준 좌표 보다 멀어지면 더미 타겟에 플레이어 현재 좌표를 집어넣고 followTarget 을 바꾼다
            //if (Mathf.Abs(distanceLockCenterTarget.transform.position.z - player.transform.position.z)  > 3) {
            //    //Debug.Log(Vector3.Distance(distanceLockCenterTarget.transform.position, player.transform.position) + "커");
            //    //더미 좌표에 플레이어 좌료를 집어 넣는다 [한번만 가능]
            //    if(setAbleDummy) {
            //        setAbleDummy = false;
            //        distanceLockDummyTarget.transform.position = player.transform.position;
            //    }

            //    //followTarget 부분
            //    followTarget.transform.localPosition = Vector3.Lerp(followTarget.transform.localPosition, distanceLockDummyTarget.transform.localPosition, Time.deltaTime * recoverStrong);
            //} else {
            //    setAbleDummy = true;
                //Debug.Log(Vector3.Distance(distanceLockCenterTarget.transform.position, player.transform.position) + "작아");
                //followTarget 부분
                followTarget.transform.position = Vector3.Lerp(followTarget.transform.position, player.transform.position, Time.deltaTime * recoverStrong);
            //}

            //Debug.Log(Vector3.Dot(followTarget.transform.position, player.transform.localPosition));
        }
    }
}
