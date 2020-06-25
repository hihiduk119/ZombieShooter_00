using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.Common
{
    /// <summary>
    /// 조이스틱을 이용한 유저 인풋
    /// </summary>
    public class JoystickInput : MonoBehaviour , IUserInput
    {
        [Header("[울티메이트 조이스틱의 이름]")]
        public string JoystickTag = "LookAhead";

        #region [IUserInput 구현]
        float _horizontal;
        float _vertical;
        public float Horizontal { get => _horizontal; }
        public float Vertical { get => _vertical; }
        #endregion

        private void FixedUpdate()
        {
            _horizontal = UltimateJoystick.GetHorizontalAxis(JoystickTag);
            _vertical = UltimateJoystick.GetVerticalAxis(JoystickTag);
        }
    }
}
