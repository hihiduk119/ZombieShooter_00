using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class Pistol : MonoBehaviour , IWeapon , IProjectileLauncher
    {
        private ProjectileLauncher projectileLauncher;
        public ProjectileLauncher ProjectileLauncher { get => projectileLauncher; set => projectileLauncher = value; }

        private GunSettings gunSettings;
        public GunSettings GunSettings { get => gunSettings; set => gunSettings = value; }

        public void Attack()
        {
            projectileLauncher.Fire();
        }

        public void Stop()
        {

        }
    }
}


