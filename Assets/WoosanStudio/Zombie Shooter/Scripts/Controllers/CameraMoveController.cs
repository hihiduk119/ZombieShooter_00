using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WoosanStudio.Camera;
using Cinemachine;
using DG.Tweening;

//using WoosanStudio.Common;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 임시로 만든 레벨 스와퍼
    /// 나중에 진짜 레벨 스와퍼와 통일 해야함.
    /// **임시임으로 반드시 추후 통합 필요.
    /// </summary>
    public class CameraMoveController : MonoBehaviour
    {
        //타겟 인덱스도 가지고 있음
        [Header("[실제 시네머신을 동작 컨트롤러")]
        public RailController railController;

        [Header("[카메라의 위치 전담 축")]
        public Transform CameraPositionAxis;

        [Header("[카메라의 회전 전담 축")]
        public Transform CameraRotationAxis;

        [Header("[메인카메라")]
        public UnityEngine.Camera MainCamera;

        [Header("[레벨 이동 완료후 포커스 맞출 위치와 회전 값을 가짐")]
        public FocusOffset FocusOffset;

        [Header("[카메라가 따라다니게 만드는 스크립트")]
        public Transform FollowCameraTarget;

        [Header("[마지막 포커스시 필드 오브 뷰 값")]
        public float FOV_Value = 65f;

        [Header("[실제 돌리 카트의 거리를 체크하는 녀석임")]
        public DistanceCheck distanceCheck;

        //메인 카메라의 트렌스폼
        private Transform Camera;

        //포커스 사용시 화면 듀레이
        private float Duration = 0.5f;

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
            //**해당 부분은 나중에 이벤트 호출로 변경 되어야 함
            //CM_카메라 비활성화. CM 카메라가 비활성화 되면 카메라 움직임 가능해짐
            StageManager.Instance.Off();

            //카메라의 포지션축을 CM웨이 포인트로 강제 이동 시킴
            //CM카메라 이동과 실제 포지션이동은 다르기 때문에 마추기 위해 사용
            //CM카메라 사용으로 강제로 틀어진 축을 재정비 시킴
            Vector3 pos = CameraRotationAxis.position;
            Quaternion rot = CameraRotationAxis.rotation;
            CameraPositionAxis.position = pos;
            CameraRotationAxis.localPosition = Vector3.zero;

            Debug.Log("실행 OFF");

            //화면 포커스 실
            FocusCamera();
        }

        /// <summary>
        /// 카메라 포커스 마추기
        /// </summary>
        private void FocusCamera()
        {
            int index = railController.Next;
            Focus focus = FocusOffset.Offsets[index];

            //해당 트윈으로 포커스 마춤
            //MainCamera.transform.DOLocalMove(focus.Position, Duration).SetRelative(false).SetEase(Ease.InOutSine);
            //MainCamera.transform.DOLocalRotate(focus.Rotation, Duration).SetRelative(false).SetEase(Ease.InOutSine);

            //해당 트윈으로 포커스 마춤
            CameraPositionAxis.transform.DOMove(railController.WaypointList[index], Duration).SetRelative(false).SetEase(Ease.InOutSine);
            MainCamera.transform.localPosition = Vector3.zero;
            CameraRotationAxis.DOLocalRotate(focus.Rotation, Duration).SetRelative(false).SetEase(Ease.InOutSine);

            //실제 레일의 포지션
            //railController.WaypointList[index];

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

        //==============(스왑 부분)==============

        /// <summary>
        /// 자동으로 스테이지 카운팅하면서 스테이지 스왑
        /// </summary>
        public void AutoChange()
        {
            //카메라 락 해제
            //**해당 부분은 나중에 이벤트 호출로 변경 되어야 함
            //CM_카메라 활성화. CM 카메라가 활성화되면 카메라 피봇이 강제 조정됨.
            StageManager.Instance.On();

            Debug.Log("CurrentLevel = " + CurrentLevel + "   MaxLevel = " + MaxLevel);

            //최대 레벨 초과시 0으로 초기화
            if (CurrentLevel >= MaxLevel) { CurrentLevel = 0; }
            if (NextLevel >= MaxLevel) { NextLevel = 0; }


            Debug.Log("현재 레벨 = " + CurrentLevel + "    다음 레벨 = " + NextLevel);

            //몬스터,플레이어 스폰 위치 스왑
            MonsterFactory.Level = CurrentLevel;

            //씨네머신 스왑
            railController.Swap(CurrentLevel, NextLevel);

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
            //**해당 부분은 나중에 이벤트 호출로 변경 되어야 함
            StageManager.Instance.On();

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
            railController.Swap(CurrentLevel, NextLevel);

            //레벨 자동 증가
            CurrentLevel++;
            NextLevel++;
        }

        #region [-TestCode]
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                AutoChange();
            }
        }
        #endregion
    }
}
