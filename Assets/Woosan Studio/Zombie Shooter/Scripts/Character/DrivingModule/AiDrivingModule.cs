using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class AiDrivingModule : ICharacterDrivingModule
    {
        private Transform _transform;
        private Transform _destination;
        private CharacterSettings _characterSettings;
        private NavMeshAgent _agent;

        private UnityEvent _reachDestinationEvent = new UnityEvent();
        public UnityEvent ReachDestinationEvent => _reachDestinationEvent;

        public float Speed { get => _agent.speed; }

        public AiDrivingModule(NavMeshAgent agent,Transform transform,Transform destination, CharacterSettings characterSettings)
        {
            _agent = agent;
            _transform = transform;
            _destination = destination;
            _characterSettings = characterSettings;
            _agent.stoppingDistance = characterSettings.StopingDistance;
        }

        public void Tick()
        {
            //Debug.Log("remain = " +_agent.remainingDistance + "   stop = " + _agent.stoppingDistance);

            _agent.destination = _destination.position;

            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                if(_agent.remainingDistance != 0)
                {
                    ReachDestinationEvent.Invoke();
                }
            }
        }
    }
}
