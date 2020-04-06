using UnityEngine;
using UnityEngine.Events;

public interface ILookPoint
{
    Vector3 Point { get; set; }
    UpdatePositionEvent UpdatePositionEvent { get; set; }
}