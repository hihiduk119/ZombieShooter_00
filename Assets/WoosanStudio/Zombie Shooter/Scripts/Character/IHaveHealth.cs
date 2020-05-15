using UnityEngine.Events;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public interface IHaveHealth
    {
        int Health { get; set; }
        void DamagedListener(int damage,Vector3 hit);
        DamagedEvent DamagedEvent { get; set; }
        UnityEvent ZeroHealthEvent { get; set; }
    }

    [System.Serializable]
    public class DamagedEvent : UnityEvent<int,Vector3> { }
}