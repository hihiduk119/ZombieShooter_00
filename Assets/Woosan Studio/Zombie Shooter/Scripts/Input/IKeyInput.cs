using UnityEngine.Events;

public interface IKeyInput
{
    UnityAction FireActionHandler { get; set; }
    UnityAction StopActionHandler { get; set; }
}