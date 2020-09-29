using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface ISpawnHandler
    {
        UnityEvent SpawnEvent { get; }
    }
}