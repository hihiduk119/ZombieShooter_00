using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.Common
{
    public class Accelerometer : MonoBehaviour, IUserInput
    {
        #region [IUserInput 구현]
        float _horizontal;
        float _vertical;
        public float Horizontal { get => _horizontal; }
        public float Vertical { get => _vertical; }
        #endregion

        private void FixedUpdate()
        {
            _horizontal = Input.acceleration.x * 5f;
        }

        //This is a legacy function, check out the UI section for other ways to create your UI
        //void OnGUI()
        //{
        //    //Output the rotation rate, attitude and the enabled state of the gyroscope as a Label
        //    GUI.Label(new Rect(500, 300, 200, 40), "acceleration " + Input.acceleration);

        //    GUI.Label(new Rect(500, 350, 200, 40), "x = " + Input.acceleration.x);
        //}
    }
}
