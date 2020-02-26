using System;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
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

        public void Tick() {}
    }
}