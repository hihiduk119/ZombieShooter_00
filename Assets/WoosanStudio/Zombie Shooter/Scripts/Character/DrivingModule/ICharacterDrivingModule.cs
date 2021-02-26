using UnityEngine.Events;
using UnityEngine;

namespace WoosanStudio.ZombieShooter.Character
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
        void Stop();
        void Run();
        float Speed { get; }
        Transform Destination { get; set; }

        UnityEvent ReachDestinationEvent { get; }
    }
}