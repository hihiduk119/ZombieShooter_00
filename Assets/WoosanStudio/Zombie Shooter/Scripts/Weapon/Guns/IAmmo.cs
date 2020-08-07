using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface IAmmo
    {
        UnityEvent EmptyEvent { get; }
    }
}