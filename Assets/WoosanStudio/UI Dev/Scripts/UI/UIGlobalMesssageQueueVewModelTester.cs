using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class UIGlobalMesssageQueueVewModelTester : MonoBehaviour
{
    public void MaAction()
    {
        Debug.Log("액션이다!!!");
    }

    //메시지 보내는 조건
    //1.PresentScene 로드시
    //2.Presenter에서 완료 통지시

    #region [-TestCode]
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
        }
    }*/
    #endregion

}
