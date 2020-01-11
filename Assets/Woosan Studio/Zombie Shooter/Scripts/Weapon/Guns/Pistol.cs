using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class Pistol : MonoBehaviour , IWeapon , IProjectileLauncher
    {
        private ProjectileLauncher _projectileLauncher;
        public ProjectileLauncher ProjectileLauncher { get => _projectileLauncher; set => _projectileLauncher = value; }

        private GunSettings _gunSettings;
        public GunSettings GunSettings { get => _gunSettings; set => _gunSettings = value; }

        void Awake()
        {
            _gunSettings.EmptyAmmoActionHandler += Reload;
        }

        public void Attack()
        {
            _projectileLauncher.Fire();
            _gunSettings.UseAmmo();
        }

        public void Stop()
        {

        }

        public void Reload()
        {

        }
    }
}


