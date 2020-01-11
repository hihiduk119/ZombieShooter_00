using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class Player : MonoBehaviour
    {
        public WeaponFactory weaponFactory;

        IWeapon weapon;

        private void Start()
        {
            weapon = weaponFactory.MakeWeapon(this.transform, 0);
        }
    }
}
