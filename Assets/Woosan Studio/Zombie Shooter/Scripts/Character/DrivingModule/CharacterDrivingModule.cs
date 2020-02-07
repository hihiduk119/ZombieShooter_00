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

        public void Tick()
        {
            Vector3 nextPos = _transform.position;
            nextPos.z += _characterInput.Vertical;
            nextPos.x += _characterInput.Horizontal;

            //스피드 계산용
            _speedVector.z = _characterInput.Vertical;
            _speedVector.x = _characterInput.Horizontal;
            _speed = _speedVector.magnitude;
            //Debug.Log(_speedVector.magnitude + "   z = " + _characterInput.Vertical + "  x = " + _characterInput.Horizontal);

            _transform.position = Vector3.Lerp(_transform.position, nextPos, Time.deltaTime * _characterSettings.MoveSpeed);

            float heading = Mathf.Atan2(_characterInput.Horizontal, _characterInput.Vertical);
            _transform.rotation = Quaternion.Euler(0f, heading * Mathf.Rad2Deg, 0f);
        }
    }
}