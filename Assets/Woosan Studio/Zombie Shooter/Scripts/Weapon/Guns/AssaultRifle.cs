using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class AssaultRifle : MonoBehaviour//, IWeapon, IProjectileLauncher
    {
        private ProjectileLauncher _projectileLauncher;
        public ProjectileLauncher ProjectileLauncher { get => _projectileLauncher; set => _projectileLauncher = value; }

        private GunSettings _gunSettings;
        public GunSettings GunSettings { get => _gunSettings; set => _gunSettings = value; }

        public UnityAction AmmoOutActionHandler { get; set; }

        void Start()
        {
            //탄이 비었을때 해당 액션 호출.
            //_gunSettings.AmmoOutActionHandler += Reload;
            AmmoOutActionHandler += Reload;
        }

        public void Attack()
        {
            _projectileLauncher.Fire();
            UseAmmo(GunSettings);
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
        public void ReloadAmmo(IGunStat gunStat)
        {
            gunStat.CurrentAmmo = gunStat.MaxAmmo;
        }

        /// <summary>
        /// 탄 사용
        /// </summary>
        public void UseAmmo(IGunStat gunStat)
        {
            gunStat.CurrentAmmo--;

            if (gunStat.CurrentAmmo == 0) { AmmoOutActionHandler.Invoke(); }
        }
    }
}
