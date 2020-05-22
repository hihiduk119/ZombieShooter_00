using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Cinemachine;

using WoosanStudio.Common;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 화면 연출을 위해 씨네머신을 스테이지에 따라 움직이는 컨트롤러
    /// </summary>
    public class SwapTargetController : MonoBehaviour
    {
        [Header("[돌리카트 타겟]")]
        public Transform DollyCart;
        [Header("[스왑 하려는 타겟의 루트]")]
        public Transform LookAtTargetRoot;

        [Header("[이동 타겟 리스트 자동으로 Rail과 마추기]")]
        public bool AbleAutoLookTarget = true;

        //WaypointList 와 LookAtTargets는 대칭이기에 갯수가 같아야 한다.
        [Header("[이동 타겟 리스트")]
        public List<Transform> LookAtTargets = new List<Transform>();
        [Header("[가상 카메라]")]
        public CinemachineVirtualCamera VirtualCamera;
        [Header("[기본 옵셋]")]
        private Vector3 DefaultFollowOffset = new Vector3(0, 30, -10);
        [Header("[카메라 옵셋 데이터]")]
        public FollowOffset FollowOffset;



        [Header("[거리를 찾아주고 이벤트 발생시켜줌]")]
        public DistanceCheck DistanceCheck;
        [Header("[씨네머신 패스]")]
        public CinemachinePath cinemachinePath;
        [Header("[실제 움직이는 돌리 카트]")]
        public CinemachineDollyCart cart;
        [Header("[씨네 머신에서 가져온 웨이포인트 => 자동 세팅]")]
        public List<Vector3> WaypointList = new List<Vector3>();
        [Header("[이동할 타겟의 인덱스]")]
        public int TargetIndex = 1;
        [Header("[로컬 좌표계 변환을 위해 필료]")]
        public Transform Path;
        [Header("[다음 위치를 표기하기 위한 더미]")]
        public GameObject Dummy;

        //캐쉬
        private CinemachineTransposer cinemachineTransposer;
        private int level = 0;

        private int _previous = 0;
        private int _next = 0;

        public int Previous { get => _previous; set => _previous = value; }
        public int Next { get => _next; set => _next = value; }

        private void Awake()
        {
            cinemachineTransposer = VirtualCamera.GetCinemachineComponent<CinemachineTransposer>();


            //close 이벤트 발생시 Swap (목표 타겟 변경) 실행
            //DistanceCheck.closeEvent.AddListener(AutoSwap);
            DistanceCheck.closeEvent.AddListener(AutoFocus);

            for (int index = 0; index < cinemachinePath.m_Waypoints.Length; index++)
            {
                //시네머씬에서 이동 포인트 가져옴
                //이때 로컬 좌표로 가져오기 때문에 Path를 통해서 글로벌 좌표로 변환.
                WaypointList.Add(Path.TransformPoint(cinemachinePath.m_Waypoints[index].position));
            }

            //실제 움직이는 카트
            cart.m_Position = 0;
            //거리 체커의 목표 변경
            DistanceCheck.Reset(WaypointList[TargetIndex]);
            //더미 타겟 이동
            Dummy.transform.position = WaypointList[TargetIndex];

            //돌리 카트 비활성화
            cart.enabled = false;
        }

        private void Start()
        {
            //모든 타겟들 가져오기
            Transforms.FindAll(ref LookAtTargets, LookAtTargetRoot);

            //모든 타겟과 레일위이 타겟들과 좌표 일치 시키기
            if (AbleAutoLookTarget)
            {
                for (int index = 0; index < LookAtTargets.Count; index++) { LookAtTargets[index].position = WaypointList[index]; }
            }
        }

        /// <summary>
        /// 해당 인덱스의 타겟으로 해당 카메라 포커스 스왑
        /// </summary>
        /// <param name="index"></param>
        private void Swap(Transform target)
        {
            VirtualCamera.Follow = target;
            VirtualCamera.LookAt = target;
        }

        /// <summary>
        /// 저장된 레벨로 이동
        /// </summary>
        private void Swap()
        {
            ChangeLevel(this.level);
        }

        /// <summary>
        /// 레벨 변경시 해당 넘버의 타겟으로 포커스 변경
        /// </summary>
        public void ChangeLevel(int index)
        {
            Swap(LookAtTargets[index]);

            SetOffset(FollowOffset.Offsets[index]);

            //돌리 카트 비활성화
            //cart.enabled = false;
        }

        /// <summary>
        /// 레벨 변경시 미리 레벨만 받아 놓은 부분
        /// </summary>
        /// <param name="index"></param>
        public void ChangeLevel(Slider slider)
        {
            level = (int)slider.value;
            Debug.Log("ChangeLevel = " + level);
            ChangeLevel(level);
        }

        /// <summary>
        /// 돌리 카트 포커스로 변경
        /// </summary>
        public void RideUpDollyCart()
        {
            Swap(DollyCart);
            //기본 옵셋
            //SetOffset(DefaultFollowOffset);
        }

        /// <summary>
        /// 카메라 포커스의 옵셋 강제 조정
        /// </summary>
        /// <param name="index"></param>
        public void SetOffset(Vector3 offset)
        {
            cinemachineTransposer.m_FollowOffset = offset;
        }

        #region [-TestCode]
        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.P))
        //    {
        //        Swap(3);
        //    }

        //    if (Input.GetKeyDown(KeyCode.N))
        //    {
        //        Swap(4);
        //    }
        //}
        #endregion




        /// <summary>
        /// 이동이 완료되면 해당 화면 고정
        /// </summary>
        void AutoFocus()
        {
            ChangeLevel(TargetIndex);
        }

        /// <summary>
        /// 해당 레벨로 화면 이동
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="next"></param>
        public void Swap(int previous, int next)
        {
            //일단 임시
            this.Previous = previous;
            this.Next = next;

            //돌리 카트 활성화
            cart.enabled = true;
            //인덱스 세팅
            TargetIndex = next;
            //실제 움직이는 카트
            cart.m_Position = previous;
            //거리 체커의 목표 변경
            DistanceCheck.Reset(WaypointList[next]);
            //더미 타겟 이동  
            Dummy.transform.position = WaypointList[next];

            //화면 옵셋 변경
            SetOffset(FollowOffset.Offsets[next]);

            //카트에 태워서 이동
            RideUpDollyCart();
        }

        /// <summary>
        /// 이전 슬라이더값 저장
        /// 및 실제 이동
        /// </summary>
        /// <param name="slider"></param>
        public void SetPreviosValue(Slider slider)
        {
            this.Previous = (int)slider.value;

            //최대 레벨 초과시 
            if (this.Previous >= WaypointList.Count - 1)
            {
                this.Next = 0;
                this.Previous = WaypointList.Count - 1;
            }
            else
            {
                this.Next = this.Previous + 1;
            }

            Debug.Log("Previous = " + this.Previous + "  this.Next = " + this.Next + " MaxCount = " + WaypointList.Count);


            SwapBtn();
        }

        /// <summary>
        /// 이전 슬라이더값 저장
        /// 및 실제 이동
        /// </summary>
        public void Swap(int index)
        {
            this.Previous = index;

            //최대 레벨 초과시 
            if (this.Previous >= WaypointList.Count - 1)
            {
                this.Next = 0;
                this.Previous = WaypointList.Count - 1;
            }
            else
            {
                this.Next = this.Previous + 1;
            }

            SwapBtn();
        }

        /// <summary>
        /// 다음으로 이동할 슬라이더 값 저장
        /// </summary>
        /// <param name="slider"></param>
        public void SetNextValue(Slider slider)
        {
            //this.Next = (int)slider.value;
        }

        /// <summary>
        /// 실제 스왑 시킴
        /// </summary>
        public void SwapBtn()
        {
            Swap(this.Previous, this.Next);
        }
    }
}
