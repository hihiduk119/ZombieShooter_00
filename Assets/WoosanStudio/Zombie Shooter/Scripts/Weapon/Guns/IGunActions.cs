using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace WoosanStudio.ZombieShooter
{
    public interface IReload
    {
        UnityEvent StartReloadEvent { get; }
        UnityEvent EndReloadEvent { get; }
    }
}