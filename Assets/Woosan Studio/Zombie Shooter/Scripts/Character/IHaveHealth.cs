using UnityEngine.Events;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public interface IHaveHealth
    {
        int Health { get; set; }
        void Damaged(int damage,Vector3 hit);
        DamagedEvent DamagedEvent { get; set; }
    }

    [System.Serializable]
    public class DamagedEvent : UnityEvent<int,Vector3> { }
}