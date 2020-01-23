using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface IInputActions
    {
        UnityAction StartHandler { get; set; }
        UnityAction EndHandler { get; set; }
    }
}