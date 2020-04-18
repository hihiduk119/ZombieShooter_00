using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class WeakZombieDrivingModule : ICharacterDrivingModule
    {
        private Transform _transform;
        private Transform _destination;
        private MonsterSettings _monsterSettings;
        //private CharacterSettings _characterSettings;
        private NavMeshAgent _agent;

        private UnityEvent _reachDestinationEvent = new UnityEvent();
        public UnityEvent ReachDestinationEvent => _reachDestinationEvent;

        public float Speed { get => _agent.speed; }

        //public AiDrivingModule(NavMeshAgent agent,Transform transform,Transform destination, CharacterSettings characterSettings)
        public WeakZombieDrivingModule(NavMeshAgent agent, Transform transform, Transform destination, MonsterSettings monterSettings)
        {
            _agent = agent;
            _transform = transform;
            _destination = destination;
            _monsterSettings = monterSettings;

            //캐릭터 세팅 값을 네비메쉬 에이전트에 세팅하는 부분
            _agent.speed = monterSettings.MoveSpeed;
            _agent.stoppingDistance = monterSettings.StopingDistance;

            //_characterSettings = characterSettings;
            //_agent.speed = characterSettings.MoveSpeed;
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
