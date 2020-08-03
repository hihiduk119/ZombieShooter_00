using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityStandardAssets.Characters.ThirdPerson;
using WoosanStudio.Common;
using UnityEngine.Events;

using UnityEngine.EventSystems;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 
    /// </summary>
    public class PlayerMoveActor : MonoBehaviour , ILookAt , ILookAtHandler
    {
        //서드퍼슨 컨트롤 [ 애니메이션만 담당 , 회전도 담 당]
        public MyThirdPersonCharacter thirdPersonCharacter;

        float horizon;
        float vertical;
        Vector3 desiredVelocity;

        //캠의 방향으로 조이스틱 조정하기 위해 사용
        private Transform cam;
        private Vector3 camForward;
        //움직임 관련
        public bool aimed = false;

        //[Header("[Look At할 타겟들]")]
        private List<Transform> targets = new List<Transform>();
        [Header("[[Auto] Look At 타겟]")]
        public Transform fireTarget = null;

        [Header("[인터페이스 연결]")]
        public Transform JoystickInput;

        private IInput MoveInput;

        //리지드 바디 가져옴.
        Rigidbody myRigidbody;

        #region [ILookAt 구현]
        private UnityEvent mLookStart = new UnityEvent();
        private UnityEvent mLookRelease = new UnityEvent();
        public UnityEvent LookStart => mLookStart;
        public UnityEvent LookRelease => mLookRelease;
        #endregion

        //코루틴 람다식 형태
        IEnumerator WaitAndDo(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action();
        }

        private void Awake()
        {
            //초기에 리지드 바디 가져옴
            myRigidbody = GetComponent<Rigidbody>();

            //이유는 알수 없지만 Y축으로 천천히 가라않는 문제 발생.
            //조이스틱 인터페이스를 가져올 트랜스폼이 널이라면 
            if(JoystickInput == null)
            {
                //조이스틱 인풋 스크립트는 하나만 존제하기 때문에 해당 씬에서 가져옴
                //나중에 싱글톤으로 만들어야 할듯
                JoystickInput = Transform.FindObjectOfType<JoystickInput>().transform;
            }

            //해당 트렌스 폼에서 인풋 인터페이스 가져옴
            MoveInput = JoystickInput.GetComponent<IInput>();
        }

        private void Start()
        {
            if (UnityEngine.Camera.main != null)
            {
                cam = UnityEngine.Camera.main.transform;
            }

            //일부러 딜레이 시켜서 시작
            Invoke("DelayInitialize", 0.2f);
        }

        /// <summary>
        /// ThirdPersonCharacter와 같이 사용할 경우 바로 적용이 안되기 때문에 딜레이 시켜서 적용
        /// </summary>
        private void DelayInitialize()
        {
            //강제로 y 축 얼림.
            myRigidbody.constraints = RigidbodyConstraints.FreezePositionY
                | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }

        
        /// <summary>
        /// 실제 움직임이 아닌 서드퍼슨캐릭터의 움직임 에니메이션 제어
        /// </summary>
        void Move()
        {
            //실제 조이스틱 값 가져오는 부분
            //horizon = UltimateJoystick.GetHorizontalAxis("Move");
            //vertical = UltimateJoystick.GetVerticalAxis("Move");

            horizon = MoveInput.Horizontal;
            vertical = MoveInput.Vertical;

            //Debug.Log("h = " + vertical + " v = " + vertical);

            if (cam != null)
            {
                //카메라 기준으로 조이스틱 방향성 바꿔줌
                camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
                desiredVelocity = vertical * camForward + horizon * cam.right;
            }
            else
            {
                //카메라가 없다면 기본 방향
                desiredVelocity = vertical * Vector3.forward + horizon * Vector3.right;
            }
        }

        /// <summary>
        /// FixedUpdate 사용시 부드럽지 않아서 기본 Update사용
        /// </summary>
        private void Update()
        {
            //실제 움직임이 아닌 서드퍼슨캐릭터의 움직임 에니메이션 제어
            Move();

            //앞으로 걸을지 뒷걸을 칠지 결정
            MoveStateControl();

            //Navmesh를 같이 사용할경우 y축 다운 포스가 계속 발생
            //강제로 0으로 초기화함.
            //문제가 발생하는지는 확인해 보진 않음.
            myRigidbody.velocity = Vector3.zero;

            //Look At 타겟을 락인 & 릴리즈 코드
            #region [-TestUnit]
            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    OnLookStart();
            //}

            //if (Input.GetKeyDown(KeyCode.S))
            //{
            //    OnLookRelease();
            //}
            #endregion
        }

        #region [ILookAtHandler 구현]
        /// <summary>
        /// 타겟을 처다봄
        /// </summary>
        public void OnLookStart()
        {
            aimed = true;

            LookStart?.Invoke(); //null 이 아리면 Invoke();
        }

        /// <summary>
        /// 타겟 처다봄 릴리즈
        /// </summary>
        public void OnLookRelease()
        {
            aimed = false;

            LookRelease?.Invoke();
        }
        #endregion

        /// <summary>
        /// 앞으로 걷을지 뒷걸을 칠지 결정
        /// 회전도 결정함. 회전은 서드퍼슨캐릭터 사용
        /// </summary>
        private void MoveStateControl()
        {

            //실제 이동을 담당
            //navMeshAgent.destination = transform.position + desiredVelocity;

            if (cam != null)
            {
                //카메라 기준으로 조이스틱 방향성 바꿔줌
                camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
                desiredVelocity = vertical * camForward + horizon * cam.right;
            }
            else
            {
                //카메라가 없다면 기본 방향
                desiredVelocity = vertical * Vector3.forward + horizon * Vector3.right;
            }

            //조준 됐다면 캐릭터의 포지션을 타겟을 바라보게 함.
            //aimed 값은 PlayerActor 의 이벤트에 의해서 값 설정됨
            if (aimed)
            {
                //Vector3 look = zombies[0].position - transform.position;

                if (fireTarget == null) {
                    Debug.Log("[" +this.name+ "] fireTarget 널!!");
                    return;
                }
                

                Vector3 look = fireTarget.position - transform.position;
                look = look.normalized;

                
                //가상패드 인식이 없을때 그냥 서서 총쏘는 애니메이션
                if (horizon == 0 && vertical == 0)
                {
                    //임의로 수정한 코드이며 기존 코드는 아님
                    thirdPersonCharacter.OnlyTurn(look, false, false);
                    //Debug.Log("정지");
                    return;
                }
                else
                {//가상패드 인식이 있을때는 걸어다니며 슈팅a
                    //애니메이션 움직임만 담당 [회전 포함]
                    thirdPersonCharacter.Move(look, false, false);
                }
            }//비조준 상태시 전방 주시
            else
            {
                thirdPersonCharacter.Move(desiredVelocity, false, false);
            }
        }
    }
}
