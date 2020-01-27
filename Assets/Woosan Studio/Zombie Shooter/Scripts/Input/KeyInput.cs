using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class KeyInput : MonoBehaviour, IInputEvents
    {
        private UnityEvent _startEvent = new UnityEvent();
        private UnityEvent _endEvent = new UnityEvent();
        public UnityEvent StartEvent { get { return _startEvent; } set { _startEvent = value; } }
        public UnityEvent EndEvent { get { return _endEvent; } set { _endEvent = value; } }

        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                StartEvent.Invoke();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                EndEvent.Invoke();
            }
        }
    }
}
