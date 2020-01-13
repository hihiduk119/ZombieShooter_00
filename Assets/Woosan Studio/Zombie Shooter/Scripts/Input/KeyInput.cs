using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class KeyInput : MonoBehaviour, IInput
    {
        private UnityAction _fireActionHandler;
        public UnityAction FireActionHandler { get; set; }

        private UnityAction _stopActionHandler;
        public UnityAction StopActionHandler { get; set; }

        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                FireActionHandler.Invoke();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                StopActionHandler.Invoke();
            }
        }
    }
}
