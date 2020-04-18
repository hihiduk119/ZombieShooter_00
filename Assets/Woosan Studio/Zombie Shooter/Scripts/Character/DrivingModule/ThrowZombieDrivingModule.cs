using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class ThrowZombieDrivingModule : ICharacterDrivingModule
    {
        public float Speed => throw new System.NotImplementedException();

        private UnityEvent _reachDestinationEvent = new UnityEvent();
        public UnityEvent ReachDestinationEvent => _reachDestinationEvent;

        public ThrowZombieDrivingModule()
        {

        }

        public void Tick()
        {
            
        }
    }
}
