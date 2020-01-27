using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace WoosanStudio.ZombieShooter
{
    public interface IReloadEvent
    {
        ReloadEvent ReloadEvent { get; set; }

    }

    public class ReloadEvent : UnityEvent<float> {}
}