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

    [Header("[근접했음]")]
    public bool Close = true;

    [Header("[근접시 자동 닫힘 기능]")]
    public bool AutoClose = true;

    [Header("[근접시 이벤트]")]
    public UnityEvent closeEvent = new UnityEvent();

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
    /// 매 프레임 거리 체크
    /// 가까워 졌을때 close 이벤트 발/
    /// </summary>
    void Check()
    {
        //한번 닫히면 Close = true 전까진 동작 안함
        if(!Close && AutoClose)
        {
            distance = Vector3.Distance(TargetPos, transform.position);

            //Debug.Log("Distance = " + distance);

            //최소 거리보다 작으면 이벤트 발생
            if (distance <= 1f)
            {
                Close = true;
                Debug.Log("체크포인트");
                closeEvent.Invoke();
            }

            //디버깅용 드로우 라인
            Debug.DrawLine(transform.position, TargetPos, Color.red);
        }
    }

    void Update()
    {
        Check();
    }
}
