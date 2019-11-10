using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimatorEventListener : MonoBehaviour
{
    public UnityEvent reloadEndEvent = new UnityEvent();

    public void ReloadEnd() 
    {
        reloadEndEvent.Invoke();
    }
}
