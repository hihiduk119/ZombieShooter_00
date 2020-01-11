﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class Player : MonoBehaviour
    {
        //무기를 만들어주는 팩토리 패턴 적용.
        public WeaponFactory weaponFactory;

        IWeapon weapon;

        private void Start()
        {
            weapon = weaponFactory.MakeWeapon(this.transform, 0);        
        }
    }
}
