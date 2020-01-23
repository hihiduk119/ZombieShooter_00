using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class KeyInput : MonoBehaviour, IInputActions
    {        
        public UnityAction StartHandler { get; set; }
        public UnityAction EndHandler { get; set; }

        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                StartHandler.Invoke();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                EndHandler.Invoke();
            }
        }
    }
}
