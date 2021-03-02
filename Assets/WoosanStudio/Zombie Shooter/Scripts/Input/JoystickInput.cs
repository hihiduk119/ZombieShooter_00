using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.Common
{
    /// <summary>
    /// 조이스틱을 이용한 유저 인풋
    /// </summary>
    public class JoystickInput : MonoBehaviour , IInput
    {
        //싱글톤 패턴
        static public JoystickInput Instance;

        [Header("[울티메이트 조이스틱의 이름]")]
        public string JoystickTag = "Move";

        #region [IUserInput 구현]
        float _horizontal;
        float _vertical;
        public float Horizontal { get => _horizontal; }
        public float Vertical { get => _vertical; }
        #endregion


        public UltimateJoystick UltimateJoystick;
        //스크린 인풋 감지용
        private WoosanStudio.ZombieShooter.IScreenInput screenInput;

        public enum State
        {
            Down,
            Up,
        }
        //스크린 이벤트 상태
        private State state = State.Up;

        //조이스틱 눌림 문제로 추가된 이벤트 처리
        private Event clickEvent;

        void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// 조이스틱 값 가져오기
        /// </summary>
        void GetJoystickInput()
        {
            switch (state)
            {
                case State.Down:
                    _horizontal = UltimateJoystick.GetHorizontalAxis(JoystickTag);
                    _vertical = UltimateJoystick.GetVerticalAxis(JoystickTag);
                    break;
                case State.Up:
                    _horizontal = 0;
                    _vertical = 0;
                    break;
            }
        }

        private void Update()
        {
            //조이스틱 값 가져오기
            GetJoystickInput();
        }

        void OnGUI()
        {
            clickEvent = Event.current;

            //버튼 다운
            if (clickEvent.type == EventType.MouseDown)
            {
                state = State.Down;
            }

            //버튼 업
            if (clickEvent.type == EventType.MouseUp)
            {
                state = State.Up;
            }
        }
    }
}
