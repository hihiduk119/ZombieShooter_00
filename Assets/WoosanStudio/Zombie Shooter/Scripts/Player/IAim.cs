using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface IAim
    {
        UnityEvent AimEvent { get; }
        UnityEvent ReleaseEvent { get; }
    }
}