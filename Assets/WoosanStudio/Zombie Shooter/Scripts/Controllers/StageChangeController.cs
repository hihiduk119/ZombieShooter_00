using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WoosanStudio.Camera;
using Cinemachine;
using DG.Tweening;

using WoosanStudio.Common;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 임시로 만든 레벨 스와퍼
    /// 나중에 진짜 레벨 스와퍼와 통일 해야함.
    /// **임시임으로 반드시 추후 통합 필요.
    /// </summary>
    public class StageChangeController : MonoBehaviour
    {
        //화면 터치시 자동으로 하면을 따라 다니면서 연출하는 컨트롤러
        public CustomCamFollow CustomCamFollow;

        //시네머신의 가상 카메라
        public CinemachineVirtualCamera VirtualCamera;

        //실제 시네머신을 동작시키는 녀석임.
        //타겟 인덱스도 가지고 있음
        public SwapTargetController swapTargetController;

        //메인카메라
        public UnityEngine.Camera MainCamera;

        //레벨 이동 완료후 포커스 맞출 위치와 회전 값을 가짐
        public FocusOffset FocusOffset;

        //카메라가 따라다니게 만드는 스크립트
        public Transform FollowCameraTarget;

        //마지막 포커스시 필드 오브 뷰 값
        public float FOV_Value = 22f;

        //메인 카메라의 트렌스폼
        private Transform Camera;

        //포커스 사용시 화면 듀레이
        float Duration = 0.5f;

        //실제 돌리 카트의 거리를 체크하는 녀석임
        public DistanceCheck distanceCheck;



        [Header("[몬스터를 해당 위치에 생성 시켜줌]")]
        [Header("==============(스왑 부분)==============")]
        public MonsterFactory MonsterFactory;




        [Header("[플레이어용 스폰 포지션 컨트롤러]")]
        //스테이지 변경시 cam위치에 맞게 스폰위치 변경을 위해 호출 필수
        public PositionController PlayerSpawnController;

        [Header("[몬스터용 스폰 포지션 컨트롤러]")]
        //스테이지 변경시 cam위치에 맞게 스폰위치 변경을 위해 호출 필수
        public PositionController MonsterSpawnController;

        [Header("[몬스터용 스폰위치 확인용 컨트롤러 = 배포시 삭제 필요]")]
        //스테이지 변경시 cam위치에 맞게 스폰위치 변경을 위해 호출 필수
        public PositionController DrawZoneController;

        [Header("[베리어용 스폰 포지션 컨트롤러]")]
        //스테이지 변경시 cam위치에 맞게 스폰위치 변경을 위해 호출 필수
        public PositionController BarrierSpawnController;




        [Header("[현재 레벨]")]
        public int CurrentLevel = 0;

        [Header("[다음 레벨]")]
        public int NextLevel = 1;
        [Header("[최대 레벨]")]
        //*스테이지 추가시 변경 해야한다.
        public int MaxLevel = 15;

        [Header("[씨네머신 컨트롤러]")]
        public SwapTargetController SwapTargetController;

        [Header("[스테이지 변환 완료]")]
        public UnityEvent ChangeCompleteEvent = new UnityEvent();


        Coroutine coroutineFocusCamera;

        private void Awake()
        {
            //도착 완료 이벤트 발생시 RunFocus실행 되게 등록.
            distanceCheck.closeEvent.AddListener(AutoFocus);

            //카메라의 트랜스폼 받아옴
            Camera = MainCamera.GetComponent<Transform>();
        }

        private void Start() { }

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
            MainCamera.DOFieldOfView(FOV_Value, Duration).OnComplete(() => ChangeCompleteEvent.Invoke());
        }

        /// <summary>
        /// 카메라 포커스 마추기 끝나면 호출
        /// 시퀀스의 마지막 부분
        /// </summary>
        public void EndFocusCamera()
        {
            //캠 이동이 완료된 후에 호출 되어야 한다.
            //플레이어 생성 위치 캠 위치에 맞게 위치 조정
            PlayerSpawnController.Repositon();
            //몬스터 스폰 위치 캠 위치에 맞게 위치 조정
            MonsterSpawnController.Repositon();
            //베리어 캠 위치에 맞게 위치 조정
            BarrierSpawnController.Repositon();
            //폰스터 스폰위치 확인용 존 위치 조정
            DrawZoneController.Repositon();
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


        //==============(스왑 부분)==============

        /// <summary>
        /// 자동으로 스테이지 카운팅하면서 스테이지 스왑
        /// </summary>
        public void AutoChange()
        {
            //카메라 락 해제
            this.On();

            Debug.Log("CurrentLevel = " + CurrentLevel + "   MaxLevel = " + MaxLevel);

            //최대 레벨 초과시 0으로 초기화
            if (CurrentLevel >= MaxLevel) { CurrentLevel = 0; }
            if (NextLevel >= MaxLevel) { NextLevel = 0; }


            Debug.Log("현재 레벨 = " + CurrentLevel + "    다음 레벨 = " + NextLevel);

            //몬스터,플레이어 스폰 위치 스왑
            MonsterFactory.Level = CurrentLevel;

            //씨네머신 스왑
            SwapTargetController.Swap(CurrentLevel, NextLevel);

            //레벨 자동 증가
            CurrentLevel++;
            NextLevel++;
        }

        /// <summary>
        /// 해당 스테이지로 이동
        /// </summary>
        /// <param name="level"></param>
        public void Change(int level)
        {
            //카메라 락 해제
            this.On();

            //최대 레벨 초과시 0으로 초기화
            if (CurrentLevel >= MaxLevel)
            {
                CurrentLevel = 0;
                NextLevel = 1;
                Debug.Log("최고 스테이지를 초과하였습니다. 0번으로 이동합니다");
            } else
            {
                //현재 마지막 레벨에 대한 예외처리는 되어 있지 않다.
                CurrentLevel = level;
                NextLevel = CurrentLevel + 1;
            }

            //몬스터,플레이어 스폰 위치 스왑
            MonsterFactory.Level = CurrentLevel;

            Debug.Log("현재 레벨 = " + CurrentLevel + "    다음 레벨 = " + NextLevel);

            //씨네머신 스왑
            SwapTargetController.Swap(CurrentLevel, NextLevel);

            //레벨 자동 증가
            CurrentLevel++;
            NextLevel++;
        }

        #region [-TestCode]
        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.O))
        //    {
        //        this.On();
        //    }

        //    if (Input.GetKeyDown(KeyCode.P))
        //    {
        //        this.Off();
        //    }

        //    if (Input.GetKeyDown(KeyCode.Alpha0))
        //    {
        //        AutoChange();
        //    }
        //}
        #endregion
    }
}
