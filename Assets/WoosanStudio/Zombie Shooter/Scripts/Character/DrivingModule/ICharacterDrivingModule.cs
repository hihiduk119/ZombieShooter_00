using UnityEngine.Events;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public enum DrivingState
    {
        Move,
        Stand,
    }

    public interface ICharacterDrivingModule
    {
        DrivingState State { get; }
        void Tick();
        float Speed { get; }
        Transform Destination { get; set; }

        UnityEvent ReachDestinationEvent { get; }
    }
}