using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 실제 타겟과의 거리 알려주며 가까워지면 이벤트 발생.
/// </summary>
public class DistanceCheck : MonoBehaviour
{
    public Vector3 TargetPos;

    public float distance = 0;

    public bool Close = true;

    public UnityEvent closeEvent = new UnityEvent();

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
    /// 매 프레임 거리 체크
    /// 가까워 졌을때 close 이벤트 발/
    /// </summary>
    void Check()
    {
        if(!Close)
        {
            distance = Vector3.Distance(TargetPos, transform.position);

            //Debug.Log("Distance = " + distance);

            if (distance <= 1f)
            {
                Close = true;
                Debug.Log("체크포인트");
                closeEvent.Invoke();
            }

            Debug.DrawLine(transform.position, TargetPos, Color.red);
        }
    }

    void Update()
    {
        Check();
    }
}
