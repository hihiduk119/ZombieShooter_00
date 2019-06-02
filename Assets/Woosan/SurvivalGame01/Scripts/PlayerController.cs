using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    //네비메쉬 [실제 이동 담당]
    //NavMeshAgent navMeshAgent;
    //서드퍼슨 컨트롤 [ 애니메이션만 담당 , 회전도 담 당]
    //ThirdPersonCharacter thirdPersonCharacter;
    //조이스틱값 캐슁
    private float horizon;
    private float vertical;
    private Vector3 desiredVelocity;
    //캠의 방향으로 조이스틱 조정하기 위해 사용
    public Transform cam;
    private Vector3 camForward;
    //최적화 캐슁
    private SkinnedMeshRenderer myRenderer;

    //테스트용 움직임 랙이 생기는것 때문에 시간재기
    float deltaTime = 0;
    float preDeltaTime = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //캐슁용 [최적화]
        //thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        //navMeshAgent = GetComponent<NavMeshAgent>();
        myRenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();

        //ThirdPersonCharacter 에서 회전을 담당 하기에 NavMeshAgent 의 회전은 막음
        //navMeshAgent.updateRotation = false;
        //navMeshAgent.updatePosition = false;
    }

    /*private void Update()
    {
        //카메라가 보고있으면 동작[최적화]
        if (myRenderer.isVisible)
        {
            //움직임 관련
            Move();
        }
    }*/

    private void FixedUpdate()
    {
        //카메라가 보고있으면 동작[최적화]
        if (myRenderer.isVisible)
        {
            //움직임 관련
            //Move();
        }
    }

    /// <summary>
    /// 캐릭터의 움직임 컨트롤
    /// </summary>
    private void Move()
    {
        //실제 조이스틱 값 가져오는 부분
        horizon = UltimateJoystick.GetHorizontalAxis("Move");
        vertical = UltimateJoystick.GetVerticalAxis("Move");

        preDeltaTime = deltaTime;
        deltaTime += Time.deltaTime;

        //Debug.Log("현재["+deltaTime + "]   간격[" + (deltaTime - preDeltaTime) + "]   =====> h = " + vertical + " v = " + vertical);

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
        //이동을 담당 (이동을 하면 안된다. 에이전트들이 많아지면 조금씩멈춤 현상 발생 )
        //navMeshAgent.destination = transform.position + desiredVelocity;
        //애니메이션 움직임 담당 [회전 포함]
        //실제 이동 없이 회전 만 시키기 위해서 MoveSpeedMultiplier 값을 0으로 바꿈
        //thirdPersonCharacter.Move(desiredVelocity, false, false);
    }
}
