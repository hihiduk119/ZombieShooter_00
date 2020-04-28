using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode01 : MonoBehaviour
{
    public Transform target;
    public Transform myParent;

    void Swap()
    {
        target.parent = myParent;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Swap();
        }
    }
}
