using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 화면에서 목적 Pivot 에 따라 다니게 하기위해 만든 녀석
/// </summary>
public class CamFollow : MonoBehaviour
{
    public Transform target;
    protected Vector3 gab;

    [Header ("[X축 을 고정]")]
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
        //왜 localPosition 을 넣었는지 몰르나 일단 position 으로 변경a
        /*pos = target.position - gab;
        if (fixX) { pos.x = transform.localPosition.x; }
        if (fixY) { pos.y = transform.localPosition.y; }
        if (fixZ) { pos.z = transform.localPosition.z; }

        transform.localPosition = pos;*/

        pos = target.position - gab;
        if (fixX) { pos.x = transform.position.x; }
        if (fixY) { pos.y = transform.position.y; }
        if (fixZ) { pos.z = transform.position.z; }

        transform.position = pos;
    }
}
