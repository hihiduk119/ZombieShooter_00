using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCamFollow : MonoBehaviour
{
    //기본 Cam Follow 의 target오브젝트에 믹스 시킬 오브젝트
    public Transform mixTarget;
    public Transform target;
    protected Vector3 gab;

    [Header("[X축 을 고정]")]
    public bool fixX = false;
    [Header("[Y축 을 고정]")]
    public bool fixY = false;
    [Header("[Z축 을 고정]")]
    public bool fixZ = false;

    //캐쉬 변수
    Vector3 pos;

    public void Start()
    {
        gab = target.position - transform.position;
    }

    public void FixedUpdate()
    {
        pos = target.position - gab;
        if (fixX) { pos.x = transform.position.x; }
        if (fixY) { pos.y = transform.position.y; }
        if (fixZ) { pos.z = transform.position.z; }

        pos.z = mixTarget.position.z;

        transform.position = pos;
    }
}
