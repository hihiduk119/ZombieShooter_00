using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class KeyInput : MonoBehaviour, IInputActions
    {        
        public UnityAction LeftMouseButtonDownHandler { get; set; }
        public UnityAction RightMouseButtonDownHandler { get; set; }

        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                LeftMouseButtonDownHandler.Invoke();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                RightMouseButtonDownHandler.Invoke();
            }
        }
    }
}
