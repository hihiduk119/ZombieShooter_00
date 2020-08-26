using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WoosanStudio.ZombieShooter;

public class CameraShakeProjectile : MonoBehaviour , ICameraShaker {

    //public bool cameraShakeBool = true;
    public Animator CamerShakeAnimator;

    public void ShakeCamera()
    {
        CamerShakeAnimator.SetTrigger("CameraShakeTrigger");
    }

    public void Shake()
    {
        //일단 못하게 막음
        ShakeCamera();
    }

    #region [-TestCode]
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.G))
    //    {
    //        Shake();
    //    }
    //}
    #endregion

}
