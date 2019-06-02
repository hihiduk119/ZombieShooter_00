using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//화면에서 목적 Pivot 에 따라 다니게 하기위해 만든 녀석
public class CamFollow : MonoBehaviour
{
    public Transform target;
    Vector3 gab;

    public bool fixX = false;
    public bool fixY = false;
    public bool fixZ = false;

    Vector3 pos;

    private void Start()
    {
        gab = target.position - transform.position;
    }

    void FixedUpdate()
    {
        pos = target.position - gab;
        if (fixX) { pos.x = transform.localPosition.x; }
        if (fixY) { pos.y = transform.localPosition.y; }
        if (fixZ) { pos.z = transform.localPosition.z; }
        transform.localPosition = pos;    
    }
}
