using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;


namespace WoosanStudio.ZombieShooter
{
    public class CameraShaker : MonoBehaviour, ICameraShaker
    {
        public Animator CamerShakeAnimator;

        public void Shake()
        {
            CamerShakeAnimator.SetTrigger("CameraShakeTrigger");
        }
    }
}
