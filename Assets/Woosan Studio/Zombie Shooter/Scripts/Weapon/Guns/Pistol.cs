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

        void Start()
        {
            //탄이 비었을때 해당 액션 호출.
            _gunSettings.AmmoOutActionHandler += Reload;
        }

        public void Attack()
        {
            _projectileLauncher.Fire();
            _gunSettings.UseAmmo();
        }

        /// <summary>
        /// 재장전 액션
        /// </summary>
        public void Reload()
        {

        }
    }
}


