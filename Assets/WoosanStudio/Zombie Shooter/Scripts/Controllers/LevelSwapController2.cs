using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WoosanStudio.Camera;
using Cinemachine;
using DG.Tweening;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 임시로 만든 레벨 스와퍼
    /// 나중에 진짜 레벨 스와퍼와 통일 해야함.
    /// **임시임으로 반드시 추후 통합 필요.
    /// </summary>
    public class LevelSwapController2 : MonoBehaviour
    {
        //화면 터치시 자동으로 하면을 따라 다니면서 연출하는 컨트롤러
        public CustomCamFollow CustomCamFollow;

        //시네머신의 가상 카메라
        public CinemachineVirtualCamera VirtualCamera;

        //실제 시네머신을 동작시키는 녀석임.
        public SwapTargetController swapTargetController;

        //메인카메라
        public UnityEngine.Camera MainCamera;

        //레벨 이동 완료후 포커스 맞출 위치와 회전 값을 가짐
        public FocusOffset FocusOffset;

        //카메라가 따라다니게 만드는 스크립트
        public Transform FollowCameraTarget;

        //메인 카메라의 트렌스폼
        private Transform Camera;

        //실제 폭발 반경을 위해 카메라와의 거리
        //**ExplosionFactory.distance 와 값을 마춰라
        public float distance = 80f;

        //포커스 사용시 화면 듀레이
        float Duration = 0.5f;

        //실제 돌리 카트가 거리를 체크하는 넘
        public DistanceCheck distanceCheck;

        Coroutine coroutineFocusCamera;

        private void Awake()
        {
            //도착 완료 이벤트 발생시 RunFocus실행 되게 등록.
            distanceCheck.closeEvent.AddListener(AutoFocus);

            //카메라의 트랜스폼 받아옴
            Camera = MainCamera.GetComponent<Transform>();
        }

        private void Start() {}

        /// <summary>
        /// 해당 레벨 도착 알림이 오면 실행.
        /// </summary>
        public void AutoFocus()
        {
            //자동 포커스 코루틴 실행
            if (coroutineFocusCamera != null) StopCoroutine(coroutineFocusCamera);
            coroutineFocusCamera = StartCoroutine(CoroutineFocusCamera(3f));
        }

        IEnumerator CoroutineFocusCamera(float delay)
        {
            Debug.Log("포커스 카메라 실행");
            yield return new WaitForSeconds(delay);

            //시네머신 해제
            Off();
            //화면 포커스 실
            FocusCamera();
        }

        /// <summary>
        /// 카메라 포커스 마추기
        /// </summary>
        private void FocusCamera()
        {
            int index = swapTargetController.Previous;
            Focus focus = FocusOffset.Offsets[index];

            //해당 트윈으로 포커스 마춤
            MainCamera.transform.DOLocalMove(focus.Position, Duration).SetRelative(false).SetEase(Ease.InOutSine);
            MainCamera.transform.DOLocalRotate(focus.Rotation, Duration).SetRelative(false).SetEase(Ease.InOutSine);
            //FOV 설정.
            MainCamera.DOFieldOfView(22f, Duration);
        }

        /// <summary>
        /// 카메라가 따라다닐 위치 재계산
        /// ExplosionFactory.cs => CalculateExplosionPosition와 유사
        /// </summary>
        /// <param name="followTarget"></param>
        /// <param name="camera"></param>
        /// <param name="height"></param>
        void CalculateFollowCameraTarget(Transform followTarget, Transform camera, float height = 0)
        {
            //일단 카메라 포지션 기준
            followTarget.position = camera.position;
            //보정 값
            Vector3 pos = followTarget.position;
            //높이가 있다면 높이 적용 => 고가 도로 맵에
            pos.y = height;

            //카메라 각도에 따라 폭발 루트를 회전시키기 위해 미리 받아둠.
            //Vector3 rot = followTarget.localRotation.eulerAngles;

            //회전 각에 따라 Distance도 다르게 적용.
            //회전 각에 따라 영역의 가로 새로도 변경
            switch ((int)Camera.localRotation.eulerAngles.y)
            {
                case 90: pos.z += distance; break;
                case 180: pos.x += distance; break;
                case 270: pos.z -= distance; break;
                case 0: pos.x -= distance; break;
            }

            //폭발루트에 변경된 좌표와 회전값 넣음
            followTarget.position = pos;
            //카메라 회전 y축 각도 그대로 적용
            //rot.y = Camera.localRotation.eulerAngles.y;
            //followTarget.localRotation = Quaternion.Euler(rot);
        }

        /// <summary>
        /// 캠 이동전 호출
        /// 씨네머신 활성화
        /// </summary>
        public void On() 
        {
            Debug.Log("On");
            CustomCamFollow.enabled = false;
            VirtualCamera.enabled = true;
        }

        /// <summary>
        /// 캠 이동 완료 후 호출
        /// 씨네머신 해제
        /// </summary>
        public void Off()
        {
            Debug.Log("Off");
            CustomCamFollow.enabled = true;
            VirtualCamera.enabled = false;

            //카메라가 따라다닐 위치 재계산
            //이거 안써도 동작함 ...알수 없음.
            //CalculateFollowCameraTarget(FollowCameraTarget, this.Camera);
        }

        #region [-TestCode]
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                this.On();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                this.Off();
            }

            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                this.FocusCamera();
            }
        }
        #endregion
    }
}
