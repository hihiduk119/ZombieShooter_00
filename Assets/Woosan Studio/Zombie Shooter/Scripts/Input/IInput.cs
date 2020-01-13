using UnityEngine.Events;

public interface IInput
{
    UnityAction FireActionHandler { get; set; }
    UnityAction StopActionHandler { get; set; }
}