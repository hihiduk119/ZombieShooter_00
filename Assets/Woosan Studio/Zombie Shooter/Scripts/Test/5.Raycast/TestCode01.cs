using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 강제로 플레이어 총기의 parent를 상위로 올림.
/// 일종의 꼼수 코드
/// </summary>
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
