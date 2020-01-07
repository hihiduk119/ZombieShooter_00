using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class Pistol : IWeapon
    {
        private ProjectileLauncher _projectileLauncher;

        public void Attack()
        {
            _projectileLauncher.Fire();
        }

        public void Stop()
        {

        }
    }
}


