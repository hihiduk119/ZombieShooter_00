using UnityEngine;
using UnityEngine.AI;

namespace WoosanStudio.ZombieShooter
{
    public class AiInput : ICharacterInput
    {
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }

        public void ReadInput()
        {
            
        }
    }
}