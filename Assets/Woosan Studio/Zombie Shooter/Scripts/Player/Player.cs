using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class Player : MonoBehaviour
    {
        //무기를 만들어주는 팩토리 패턴 적용.
        public WeaponFactory _weaponFactory;

        //캐슁용
        IWeapon _weapon;
        IInputActions _inputActions;

        IEnumerator Start()
        {
            _weaponFactory = FindObjectOfType<WeaponFactory>();

            yield return new WaitForSeconds(0.2f);

            _weapon = _weaponFactory.MakeWeapon(_inputActions, this.transform, 0,true);

            yield return new WaitForSeconds(0.1f);

        }

        public void Attack(IWeapon weapon)
        {
            weapon.Attack();
        }
    }
}
