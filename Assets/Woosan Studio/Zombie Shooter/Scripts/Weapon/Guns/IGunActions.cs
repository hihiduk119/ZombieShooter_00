using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface IGunActions
    {
        UnityAction FireAction { get; set; }
        UnityAction ReloadAction { get; set; }
    }
}