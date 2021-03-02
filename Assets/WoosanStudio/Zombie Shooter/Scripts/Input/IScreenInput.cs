using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface IScreenInput
    {
        UnityEvent PointerDown { get; }
        UnityEvent PointerUp { get; }
    }
}
