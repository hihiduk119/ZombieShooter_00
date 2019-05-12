using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTestController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform cam;

    //조이스틱값 캐슁
    private float horizon;
    private float vertical;
    private Vector3 camForward;
    private Vector3 desiredVelocity;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        //실제 조이스틱 값 가져오는 부분
        horizon = UltimateJoystick.GetHorizontalAxis("Move");
        vertical = UltimateJoystick.GetVerticalAxis("Move");

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
        //이동을 담당
        navMeshAgent.destination = transform.position + desiredVelocity;
    }
}
