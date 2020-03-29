using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

[System.Serializable]
public class UpdatePositionEvent : UnityEvent<Vector3> { }

public class MoveScreenPointToRayPosition : MonoBehaviour
{
    //카메라 세팅부분
    public Camera Camera;

    private bool Activate { get; set; }

    private Vector3 _point = Vector3.zero;

    public UpdatePositionEvent UpdatePositionEvent = new UpdatePositionEvent();

    int layerMask = 0;

    //Layer

    //캐쉬
    Ray _ray;
    RaycastHit _hit;

    private void Awake()
    {
        //Props & Barrier & Character를 제외한 나머지만 체크함.
        layerMask = ((1 << LayerMask.NameToLayer("Props")) | (1 << LayerMask.NameToLayer("Barrier")) | (1 << LayerMask.NameToLayer("Character")));
        layerMask = ~layerMask;
    }

    private void OnDisable()
    {
        Activate = false;
    }

    private void OnEnable()
    {
        Activate = true;
    }

    private void Move()
    {
        transform.position = _point;
    }

    /// <summary>
    /// 레이에 맞은 부분을 가져옴
    /// </summary>
    /// <returns></returns>
    public Vector3 GetHitPoint()
    {
        return _point;
    }

    void Update()
    {
        if (!Activate) return;

        if (Input.GetMouseButton(0))
        {
            _ray = Camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit,1000f,this.layerMask))
            {
                _point = _hit.point;
                //_point.y += 0.25f;
                UpdatePositionEvent.Invoke(_point);
                Move();
            }
        }
    }
}

