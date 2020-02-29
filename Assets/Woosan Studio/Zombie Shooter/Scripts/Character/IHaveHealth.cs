using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface IHaveHealth
    {
        int Health { get; set; }
        void Damaged(int damage);
        DamagedEvent DamagedEvent { get; set; }
    }

    [System.Serializable]
    public class DamagedEvent : UnityEvent<int> { }
}