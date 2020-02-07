using System;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class PlayerDrivingModule : ICharacterDrivingModule
    {
        private ICharacterInput _characterInput;
        private Transform _transform;
        private CharacterSettings _characterSettings;

        public PlayerDrivingModule(ICharacterInput characterInput, Transform transform, CharacterSettings characterSettings)
        {
            _characterInput = characterInput;
            _transform = transform;
            _characterSettings = characterSettings;
        }

        public void Tick()
        {
            Vector3 nextPos = _transform.position;
            nextPos.z += _characterInput.Vertical;
            nextPos.x += _characterInput.Horizontal;
            _transform.position = Vector3.Lerp(_transform.position, nextPos, Time.deltaTime * _characterSettings.MoveSpeed);

            float heading = Mathf.Atan2(_characterInput.Horizontal, _characterInput.Vertical);
            _transform.rotation = Quaternion.Euler(0f, heading * Mathf.Rad2Deg, 0f);
        }
    }
}