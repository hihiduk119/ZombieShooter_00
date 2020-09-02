using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 실제 타겟과의 거리 알려주며 가까워지면 이벤트 발생.
/// </summary>
public class DistanceCheck : MonoBehaviour
{
    [Header("[대상 타겟이 위치]")]
    public Vector3 TargetPos;

    [Header("[타겟을 사용할지 말지 결정]")]
    public bool UseTarget = false;

    [Header("[대상 타겟]")]
    public GameObject Target;

    [Header("[근접했음]")]
    public bool Close = false;

    [Header("[근접시 자동 닫힘 기능]")]
    public bool AutoClose = true;

    [Header("[근접시 이벤트]")]
    public UnityEvent CloseEvent = new UnityEvent();

    [Header("[최소 근접 거리]")]
    public float MixDistance = 1f;

    private float distance = 0;

    /// <summary>
    /// 목표를 재설정 하고 close 구분자 초기화
    /// </summary>
    /// <param name="newPos"></param>
    public void Reset(Vector3 newPos)
    {
        Close = false;

        TargetPos = newPos;
    }

    /// <summary>
    /// 목표를 재설정 하고 close 구분자 초기화
    /// </summary>
    /// <param name="newPos"></param>
    public void Reset(GameObject target)
    {
        Close = false;

        Target = target;
    }

    /// <summary>
    /// 매 프레임 거리 체크
    /// 가까워 졌을때 close 이벤트 발/
    /// </summary>
    void Check()
    {
        //한번 닫히면 Close = true 전까진 동작 안함
        if(!Close && AutoClose)
        {
            //TargetPos 사용시
            if (!UseTarget)
            {
                distance = Vector3.Distance(TargetPos, transform.position);

                //최소 거리보다 작으면 이벤트 발생
                if (distance <= MixDistance)
                {
                    Close = true;
                    //Debug.Log("체크포인트");
                    CloseEvent.Invoke();
                }

                //디버깅용 드로우 라인
                //Debug.DrawLine(transform.position, TargetPos, Color.red);
            } else // Target 사용시
            {
                distance = Vector3.Distance(Target.transform.position, transform.position);

                //최소 거리보다 작으면 이벤트 발생
                if (distance <= MixDistance)
                {
                    Close = true;
                    //Debug.Log("체크포인트");
                    CloseEvent.Invoke();
                }

                //디버깅용 드로우 라인
                //Debug.DrawLine(transform.position, Target.transform.position, Color.red);
            }

            //Debug.Log("Distance = " + distance);
        }
    }

    void Update()
    {
        Check();
    }
}
