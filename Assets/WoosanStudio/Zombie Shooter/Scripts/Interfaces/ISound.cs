using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface ISound
    {
        UnityEvent PlayEvent { get; }
    }
}
