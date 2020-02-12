using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAhead : MonoBehaviour
{
    public Transform Target;
    public float Speed = 100f;

    IEnumerator _corLookAhead;

    public void Look(Vector3 target)
    {
        transform.LookAt(target);
        //if (_corLookAhead != null) StopCoroutine(_corLookAhead);
        //StartCoroutine()
    }

    private void Update()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rot.x = 0;
        transform.localRotation = Quaternion.Euler(rot);
    }

    IEnumerator CorLookAhead()
    {
        while(true)
        {
            yield return null;
        }
    }
}
