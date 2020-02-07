using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class ControllerInput : ICharacterInput
    {
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }

        public void ReadInput()
        {
            Horizontal = UltimateJoystick.GetHorizontalAxis("Move");
            Vertical = UltimateJoystick.GetVerticalAxis("Move");
        }
    }
}