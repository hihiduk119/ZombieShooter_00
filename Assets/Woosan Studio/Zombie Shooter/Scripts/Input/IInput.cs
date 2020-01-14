using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface IInputActions
    {
        UnityAction LeftMouseButtonDownHandler { get; set; }
        UnityAction RightMouseButtonDownHandler { get; set; }
    }
}