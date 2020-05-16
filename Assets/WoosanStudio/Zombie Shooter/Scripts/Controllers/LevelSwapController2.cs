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

        //포커스 사용시 화면 듀레이
        float Duration = 0.5f;

        //실제 돌리 카트가 거리를 체크하는 넘
        public DistanceCheck distanceCheck;

        Coroutine coroutineFocusCamera;

        private void Awake()
        {
            //도착 완료 이벤트 발생시 RunFocus실행 되게 등록.
            distanceCheck.closeEvent.AddListener(AutoFocus);
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
