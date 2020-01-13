using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class Player : MonoBehaviour
    {
        //무기를 만들어주는 팩토리 패턴 적용.
        public WeaponFactory _weaponFactory;

        IWeapon _weapon;
        ICameraShaker _cameraShaker;

        public void Start()
        {
            _weaponFactory = FindObjectOfType<WeaponFactory>();
            _weapon = _weaponFactory.MakeWeapon(this.transform, 0);        
        }

        public void Attack(IWeapon weapon)
        {
            weapon.Attack();
        }
    }
}
