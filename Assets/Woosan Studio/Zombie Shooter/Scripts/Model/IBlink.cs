using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public interface IBlink
    {
        void Blink();
        void Initialize();
        GameObject myGameObject { get; set; }
    }
}