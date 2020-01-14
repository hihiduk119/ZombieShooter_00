using UnityEngine.Events;

public interface IInputActions
{
    UnityAction FireActionHandler { get; set; }
    UnityAction StopActionHandler { get; set; }
}