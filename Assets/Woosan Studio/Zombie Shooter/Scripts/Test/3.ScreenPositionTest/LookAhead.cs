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

    public void Look(Vector3 target)
    {
        transform.LookAt(target);
    }

    private void Update()
    {
        
        Vector3 rot = transform.localRotation.eulerAngles;
        rot.x = 0;
        transform.localRotation = Quaternion.Euler(rot);   
    }
}
