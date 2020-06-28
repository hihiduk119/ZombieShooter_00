﻿using UnityEngine.Events;

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

        UnityEvent ReachDestinationEvent { get; }
    }
}