using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface IReloadEvent
    {
        ReloadEvent ReloadEvent { get; set; }
        void ConnectReloadEvent(IReloadEventSocket reloadEventSocket);
    }

    public class ReloadEvent : UnityEvent<float> { }
}