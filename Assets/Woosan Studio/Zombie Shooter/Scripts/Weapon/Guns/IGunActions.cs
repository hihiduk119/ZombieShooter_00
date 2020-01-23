using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface IGunActions
    {
        //UnityAction FireAction { get; set; }
        UnityAction TriggerAction { get; set; }
        UnityAction ReloadAction { get; set; }
    }
}