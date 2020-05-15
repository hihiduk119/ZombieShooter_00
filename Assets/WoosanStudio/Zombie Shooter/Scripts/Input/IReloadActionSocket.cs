using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface IReloadAction
    {
        UnityAction<float> ReloadAction { get; set; }
    }
}