using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface ILookAt
    {
        UnityEvent LookStart { get; }
        UnityEvent LookRelease { get; }
    }
}
