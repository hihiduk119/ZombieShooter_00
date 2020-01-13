using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

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
            //런쳐에서 탄 발사시 호출 등록.
            ((IProjectileLauncherActions)_projectileLauncher).FireActionHandler += UseAmmo;
        }

        public void Attack()
        {
            _projectileLauncher.Fire();
            UseAmmo();
        }

        /// <summary>
        /// 재장전 액션
        /// </summary>
        public void Reload()
        {

        }

        /// <summary>
        /// 재장전
        /// </summary>
        public void ReloadAmmo()
        {
            IGunStat gunStat = (IGunStat)GunSettings;
            gunStat.CurrentAmmo = gunStat.MaxAmmo;
        }

        /// <summary>
        /// 탄 사용
        /// </summary>
        public void UseAmmo()
        {
            IGunStat gunStat = (IGunStat)GunSettings;
            gunStat.CurrentAmmo--;

            Debug.Log("남은 탄약 = " + gunStat.CurrentAmmo);
            if (gunStat.CurrentAmmo == 0) { Reload(); }
        }
    }
}


