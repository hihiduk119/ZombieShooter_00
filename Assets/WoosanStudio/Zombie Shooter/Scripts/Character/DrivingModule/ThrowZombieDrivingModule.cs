using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class ThrowZombieDrivingModule : ICharacterDrivingModule
    {
        private Transform _transform;
        private Transform _destination;
        private MonsterSettings _monsterSettings;
        //private CharacterSettings _characterSettings;
        private NavMeshAgent _agent;

        private UnityEvent _reachDestinationEvent = new UnityEvent();
        public UnityEvent ReachDestinationEvent => _reachDestinationEvent;

        public float Speed { get => _agent.speed; }

        private DrivingState _state = DrivingState.Move;
        public DrivingState State { get => _state; }

        //캐쉬용
        Vector3 lookRot;

        //public AiDrivingModule(NavMeshAgent agent,Transform transform,Transform destination, CharacterSettings characterSettings)
        public ThrowZombieDrivingModule(NavMeshAgent agent, Transform transform, Transform destination, MonsterSettings monterSettings)
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

        /// <summary>
        /// NavMeshAgent.remainingDistance 오동작으로 재 작성.
        /// 목표와 나의 거리 가져옴
        /// </summary>
        /// <returns></returns>
        float GetRemainingDistance()
        {
            if (_destination == null) return -1f;

            return Vector3.Distance(_transform.position, _destination.position);
        }

        public void Tick()
        {
            //Debug.Log( "["+_transform.name+"]   remain = " +_agent.remainingDistance + "   stop = " + _agent.stoppingDistance);

            _agent.destination = _destination.position;
            //움직이고 있는 상태
            _state = DrivingState.Move;

            if (GetRemainingDistance() <= _agent.stoppingDistance)
            {
                //서있는 상태
                _state = DrivingState.Stand;

                if (GetRemainingDistance() != 0)
                {
                    Debug.Log("목표 = " + _destination.name);

                    Debug.Log( "["+_transform.name+"]   remain = " + GetRemainingDistance() + "   stop = " + _agent.stoppingDistance);
                    //공격 가능 거리에 도착 했다는 이벤트 발생.
                    //ZombieFSM의 리스너에 등록된 AttackStart 를 true 로 변경.  => 공격 시작
                    ReachDestinationEvent.Invoke();

                    //계속 오브젝트 쳐다보기
                    //이때 y축이 바뀌더라도 무시 하고 회전
                    lookRot = new Vector3(_destination.position.x, _transform.position.y, _destination.position.z);
                    _transform.LookAt(lookRot);
                }
            }
        }
    }
}
