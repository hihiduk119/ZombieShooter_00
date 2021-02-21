using UnityEngine.Events;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public interface IHaveHealth
    {
        int Health { get; set; }
        int MaxHealth { get; set; }
        void DamagedListener(int damage,Vector3 hit,string keyValue);
        void ResetHealth(int health);
        void SetHealth(int health);

        DamagedEvent DamagedEvent { get; set; }
        UnityEvent ZeroHealthEvent { get; set; }
    }

    [System.Serializable]
    public class DamagedEvent : UnityEvent<int,Vector3,string> { }
}