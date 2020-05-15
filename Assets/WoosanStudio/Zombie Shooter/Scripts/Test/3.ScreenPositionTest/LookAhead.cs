using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAhead : MonoBehaviour
{
    //[HideInInspector] public Transform target;

    //private void Start()
    //{
    //    target = FindObjectOfType<MoveScreenPointToRayPosition>().transform;
    //}

    public bool smooth = false;
    private float speed = 5f;
    //캐쉬
    Vector3 rot;
    Quaternion targetRotation;
    Coroutine _smoothLook;
    WaitForEndOfFrame WFEF = new WaitForEndOfFrame();

    /// <summary>
    /// 타겟 바로 바라봄
    /// </summary>
    /// <param name="target"></param>
    public void Look(Vector3 target)
    {
        transform.LookAt(target);
    }

    /// <summary>
    /// 러프가 있어서 부드럽게 바라봄
    /// </summary>
    /// <param name="target"></param>
    public void SmoothLook(Vector3 target)
    {
        if (_smoothLook != null) StopCoroutine(_smoothLook);

        _smoothLook = StartCoroutine(CoroutineSmoothLook(target));
    }

    IEnumerator CoroutineSmoothLook(Vector3 target)
    {
        while(true)
        {
            targetRotation = Quaternion.LookRotation(target - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);

            yield return WFEF;
        }
    }
}
