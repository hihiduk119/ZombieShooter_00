﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

/// <summary>
/// 레이를 사용하여 해당 레이에 타겟이 걸리면 이벤트 발생
/// *레이는 대상이 반드시 collider가 존재해야 함을 잊지 말자.
/// </summary>
public class RayCheck : MonoBehaviour
{
    [Header("[대상 타겟]")]
    public GameObject Target;

    [Header("[체크 최대 거리]")]
    public float MaxDistance = 100f;

    [Header("[해당 되는 레이어]")]
    public LayerMask UseLayerMask;

    [Header("[히트 이벤트 발생]")]
    public UnityEvent HitEvent = new UnityEvent();

    private RaycastHit hit;

    /// <summary>
    /// 레이 체크
    /// </summary>
    void Check()
    {
        if (Target == null) return;

        if (Physics.Raycast(transform.position, transform.forward, out hit, MaxDistance,UseLayerMask.value))
        {
            Debug.DrawRay(transform.position, transform.forward * MaxDistance, Color.green, 0.1f);
            //히트 이벤트 발생
            HitEvent?.Invoke();
        } else
        {
            Debug.DrawRay(transform.position, transform.forward * MaxDistance, Color.red, 0.1f);
        }
    }

    private void FixedUpdate()
    {
        Check();
    }
}
