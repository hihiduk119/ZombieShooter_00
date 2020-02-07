using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace WoosanStudio.ZombieShooter
{
    public class AiDrivingModule : ICharacterDrivingModule
    {
        private Transform _transform;
        private Transform _destination;
        private CharacterSettings _characterSettings;
        private NavMeshAgent _agent;

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
            _agent.destination = _destination.position;
        }
    }
}
