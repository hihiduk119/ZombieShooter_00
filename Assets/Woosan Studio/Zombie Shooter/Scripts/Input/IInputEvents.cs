using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface IInputEvents
    {
        UnityEvent StartEvent { get; set; }
        UnityEvent EndEvent { get; set; }
    }
}