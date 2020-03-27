using System;
using UnityEngine;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 2020.03.27
    /// 플레이어 동작 모듈은 현재 사용 하지 않음.
    /// </summary>
    public class PlayerDrivingModule : ICharacterDrivingModule
    {
        private ICharacterInput _characterInput;
        private Transform _transform;
        private CharacterSettings _characterSettings;
        private float _speed;

        //캐슁용 스피드계산용 벡private
        private Vector3 _speedVector;

        public PlayerDrivingModule(ICharacterInput characterInput, Transform transform, CharacterSettings characterSettings)
        {
            _characterInput = characterInput;
            _transform = transform;
            _characterSettings = characterSettings;
        }

        public float Speed { get => _speed; }

        public UnityEvent ReachDestinationEvent => throw new NotImplementedException();

        public void Tick() {}
    }
}