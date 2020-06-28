using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface ILookAtHandler
    {
        void OnLookStart();
        void OnLookRelease();
    }
}
